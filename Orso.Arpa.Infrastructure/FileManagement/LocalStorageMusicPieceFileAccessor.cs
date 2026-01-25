using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.MusicLibraryDomain;

namespace Orso.Arpa.Infrastructure.FileManagement
{
    /// <summary>
    /// Local file system implementation of IMusicPieceFileAccessor for environments without Azure Blob Storage.
    /// Stores sheet music files in a configurable local directory.
    /// </summary>
    public class LocalStorageMusicPieceFileAccessor : IMusicPieceFileAccessor
    {
        private readonly string _storagePath;
        private const string DefaultStoragePath = "/data/sheet-music";

        /// <summary>
        /// Maximum allowed file size for sheet music files (50 MB).
        /// </summary>
        private const long MaxFileSizeBytes = 50 * 1024 * 1024;

        /// <summary>
        /// Allowed file extensions for sheet music files.
        /// </summary>
        private static readonly string[] AllowedExtensions =
            [".pdf", ".jpg", ".jpeg", ".png", ".webp", ".gif", ".xml", ".musicxml", ".mxl"];

        /// <summary>
        /// Magic bytes for file type validation.
        /// </summary>
        private static readonly Dictionary<string, byte[][]> FileMagicBytes = new()
        {
            { ".pdf", [[0x25, 0x50, 0x44, 0x46]] }, // %PDF
            { ".jpg", [[0xFF, 0xD8, 0xFF]] },
            { ".jpeg", [[0xFF, 0xD8, 0xFF]] },
            { ".png", [[0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A]] },
            { ".gif", [[0x47, 0x49, 0x46, 0x38, 0x37, 0x61], [0x47, 0x49, 0x46, 0x38, 0x39, 0x61]] },
            { ".webp", [[0x52, 0x49, 0x46, 0x46]] }, // RIFF header
            { ".xml", [[0x3C, 0x3F, 0x78, 0x6D, 0x6C]] }, // <?xml
            { ".musicxml", [[0x3C, 0x3F, 0x78, 0x6D, 0x6C]] }, // <?xml
            { ".mxl", [[0x50, 0x4B, 0x03, 0x04]] } // PK (ZIP archive)
        };

        public LocalStorageMusicPieceFileAccessor(IConfiguration configuration)
        {
            _storagePath = configuration.GetValue<string>("LocalStorageConfiguration:SheetMusicPath")
                ?? DefaultStoragePath;

            // Ensure directory exists
            if (!Directory.Exists(_storagePath))
            {
                Directory.CreateDirectory(_storagePath);
            }
        }

        /// <summary>
        /// Sanitizes the filename to prevent path traversal attacks.
        /// </summary>
        private static string SanitizeFileName(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException("Filename cannot be null or empty", nameof(fileName));
            }

            if (fileName.Contains("..") || Path.IsPathRooted(fileName))
            {
                throw new ArgumentException("Invalid filename: path traversal not allowed", nameof(fileName));
            }

            var sanitized = Path.GetFileName(fileName);

            if (string.IsNullOrWhiteSpace(sanitized))
            {
                throw new ArgumentException("Invalid filename", nameof(fileName));
            }

            return sanitized;
        }

        /// <summary>
        /// Validates file size, extension, and content (magic bytes).
        /// </summary>
        private static async Task ValidateFileAsync(IFormFile file)
        {
            if (file.Length > MaxFileSizeBytes)
            {
                throw new ArgumentException(
                    $"File size ({file.Length / 1024 / 1024:F1} MB) exceeds maximum allowed size ({MaxFileSizeBytes / 1024 / 1024} MB)",
                    nameof(file));
            }

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!AllowedExtensions.Contains(extension))
            {
                throw new ArgumentException(
                    $"File type '{extension}' is not allowed. Allowed types: {string.Join(", ", AllowedExtensions)}",
                    nameof(file));
            }

            if (FileMagicBytes.TryGetValue(extension, out var validSignatures))
            {
                var headerBytes = new byte[8];
                using var stream = file.OpenReadStream();
                var bytesRead = await stream.ReadAsync(headerBytes.AsMemory(0, 8));

                if (bytesRead < 4)
                {
                    throw new ArgumentException("File is too small to be valid", nameof(file));
                }

                var isValidSignature = validSignatures.Any(signature =>
                    headerBytes.Take(signature.Length).SequenceEqual(signature));

                if (!isValidSignature)
                {
                    throw new ArgumentException(
                        $"File content does not match expected format for '{extension}'",
                        nameof(file));
                }
            }
        }

        public async Task<IFileResult> SaveAsync(IFormFile file, string fileName)
        {
            await ValidateFileAsync(file);

            var actualFileName = SanitizeFileName(fileName ?? file.FileName);
            var filePath = Path.Combine(_storagePath, actualFileName);
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!new FileExtensionContentTypeProvider().TryGetContentType(actualFileName, out var mimeType))
            {
                mimeType = "application/octet-stream";
            }

            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var content = await File.ReadAllBytesAsync(filePath);
            var fileInfo = new FileInfo(filePath);

            return new FileResult
            {
                Content = content,
                Name = actualFileName,
                ContentType = mimeType,
                Extension = extension,
                LastModified = fileInfo.LastWriteTimeUtc
            };
        }

        public async Task<IFileResult> GetAsync(string fileName)
        {
            var sanitizedFileName = SanitizeFileName(fileName);
            var filePath = Path.Combine(_storagePath, sanitizedFileName);

            if (!File.Exists(filePath))
            {
                return null;
            }

            var content = await File.ReadAllBytesAsync(filePath);
            var fileInfo = new FileInfo(filePath);
            var extension = Path.GetExtension(sanitizedFileName).ToLowerInvariant();

            if (!new FileExtensionContentTypeProvider().TryGetContentType(sanitizedFileName, out var mimeType))
            {
                mimeType = "application/octet-stream";
            }

            return new FileResult
            {
                Content = content,
                Name = sanitizedFileName,
                ContentType = mimeType,
                Extension = extension,
                LastModified = fileInfo.LastWriteTimeUtc
            };
        }

        public Task DeleteAsync(string fileName)
        {
            var sanitizedFileName = SanitizeFileName(fileName);
            var filePath = Path.Combine(_storagePath, sanitizedFileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Not supported for local storage. Returns null.
        /// </summary>
        public Task<BlobClient> GetAsBlobAsync(string fileName)
        {
            return Task.FromResult<BlobClient>(null);
        }

        /// <summary>
        /// Gets the full file path for a given file name.
        /// </summary>
        public string GetFilePath(string fileName)
        {
            var sanitizedFileName = SanitizeFileName(fileName);
            return Path.Combine(_storagePath, sanitizedFileName);
        }
    }
}

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
using Orso.Arpa.Misc;

namespace Orso.Arpa.Infrastructure.FileManagement
{
    /// <summary>
    /// Local file system implementation of IFileAccessor for environments without Azure Blob Storage.
    /// Stores profile pictures in a configurable local directory.
    /// </summary>
    public class LocalStorageProfilePictureAccessor : IFileAccessor
    {
        private readonly string _storagePath;
        private const string DefaultStoragePath = "/data/profile-pictures";

        /// <summary>
        /// Maximum allowed file size for profile pictures (5 MB).
        /// </summary>
        private const long MaxFileSizeBytes = 5 * 1024 * 1024;

        /// <summary>
        /// Allowed file extensions for profile pictures.
        /// </summary>
        private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png", ".webp", ".gif"];

        /// <summary>
        /// Magic bytes for image file type validation.
        /// </summary>
        private static readonly Dictionary<string, byte[][]> ImageMagicBytes = new()
        {
            { ".jpg", [[0xFF, 0xD8, 0xFF]] },
            { ".jpeg", [[0xFF, 0xD8, 0xFF]] },
            { ".png", [[0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A]] },
            { ".gif", [[0x47, 0x49, 0x46, 0x38, 0x37, 0x61], [0x47, 0x49, 0x46, 0x38, 0x39, 0x61]] },
            { ".webp", [[0x52, 0x49, 0x46, 0x46]] }  // RIFF header (WEBP follows at bytes 8-11)
        };

        public LocalStorageProfilePictureAccessor(IConfiguration configuration)
        {
            _storagePath = configuration.GetValue<string>("LocalStorageConfiguration:ProfilePicturesPath")
                ?? DefaultStoragePath;

            // Ensure directory exists
            if (!Directory.Exists(_storagePath))
            {
                Directory.CreateDirectory(_storagePath);
            }
        }

        /// <summary>
        /// Sanitizes the filename to prevent path traversal attacks.
        /// Removes any directory components and validates the filename.
        /// </summary>
        private static string SanitizeFileName(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException("Filename cannot be null or empty", nameof(fileName));
            }

            // Check for path traversal attempts
            if (fileName.Contains("..") || Path.IsPathRooted(fileName))
            {
                throw new ArgumentException("Invalid filename: path traversal not allowed", nameof(fileName));
            }

            // Extract only the filename, removing any directory components
            var sanitized = Path.GetFileName(fileName);

            if (string.IsNullOrWhiteSpace(sanitized))
            {
                throw new ArgumentException("Invalid filename", nameof(fileName));
            }

            return sanitized;
        }

        /// <summary>
        /// Validates file size, extension, and content (magic bytes) to ensure it's a valid image.
        /// </summary>
        private static async Task ValidateImageFileAsync(IFormFile file)
        {
            // Validate file size
            if (file.Length > MaxFileSizeBytes)
            {
                throw new ArgumentException(
                    $"File size ({file.Length / 1024 / 1024:F1} MB) exceeds maximum allowed size ({MaxFileSizeBytes / 1024 / 1024} MB)",
                    nameof(file));
            }

            // Validate extension
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!AllowedExtensions.Contains(extension))
            {
                throw new ArgumentException(
                    $"File type '{extension}' is not allowed. Allowed types: {string.Join(", ", AllowedExtensions)}",
                    nameof(file));
            }

            // Validate magic bytes (file content signature)
            if (ImageMagicBytes.TryGetValue(extension, out var validSignatures))
            {
                var headerBytes = new byte[8];
                using var stream = file.OpenReadStream();
                var bytesRead = await stream.ReadAsync(headerBytes.AsMemory(0, 8));

                if (bytesRead < 4)
                {
                    throw new ArgumentException("File is too small to be a valid image", nameof(file));
                }

                var isValidSignature = validSignatures.Any(signature =>
                    headerBytes.Take(signature.Length).SequenceEqual(signature));

                if (!isValidSignature)
                {
                    throw new ArgumentException(
                        $"File content does not match expected image format for '{extension}'",
                        nameof(file));
                }
            }
        }

        public async Task<IFileResult> SaveAsync(IFormFile file, string fileName = null)
        {
            // Validate file before saving
            await ValidateImageFileAsync(file);

            var actualFileName = SanitizeFileName(fileName ?? file.FileName);
            var filePath = Path.Combine(_storagePath, actualFileName);
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!new FileExtensionContentTypeProvider().TryGetContentType(actualFileName, out var mimeType))
            {
                mimeType = "image/webp";
            }

            // Save file to disk
            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Read file content for response
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
                mimeType = "image/webp";
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
        /// Azure Blob-specific functionality is not available in local storage mode.
        /// </summary>
        public Task<BlobClient> GetAsBlobAsync(string fileName)
        {
            // This method is Azure-specific and not supported for local storage.
            // The ArpaProfilePictureProvider needs to handle this case.
            return Task.FromResult<BlobClient>(null);
        }

        /// <summary>
        /// Gets the full file path for a given file name.
        /// Used by LocalProfilePictureProvider for ImageSharp integration.
        /// </summary>
        public string GetFilePath(string fileName)
        {
            var sanitizedFileName = SanitizeFileName(fileName);
            return Path.Combine(_storagePath, sanitizedFileName);
        }
    }
}

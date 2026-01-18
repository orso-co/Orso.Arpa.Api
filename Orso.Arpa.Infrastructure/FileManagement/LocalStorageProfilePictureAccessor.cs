using System;
using System.IO;
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

        public async Task<IFileResult> SaveAsync(IFormFile file, string fileName = null)
        {
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

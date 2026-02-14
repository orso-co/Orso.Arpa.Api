using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Orso.Arpa.Domain.EmailCampaignDomain.Interfaces;

namespace Orso.Arpa.Infrastructure.FileManagement
{
    public class LocalStorageEmailTemplateImageAccessor : IEmailTemplateImageAccessor
    {
        private readonly string _storagePath;
        private const string DefaultStoragePath = "/data/email-template-images";
        private const long MaxFileSizeBytes = 10 * 1024 * 1024;

        private static readonly string[] AllowedExtensions =
        [
            ".jpg", ".jpeg", ".png", ".gif", ".webp", ".svg"
        ];

        public LocalStorageEmailTemplateImageAccessor(IConfiguration configuration)
        {
            _storagePath = configuration.GetValue<string>("LocalStorageConfiguration:EmailTemplateImagesPath")
                ?? DefaultStoragePath;

            if (!Directory.Exists(_storagePath))
            {
                Directory.CreateDirectory(_storagePath);
            }
        }

        public async Task<string> SaveAsync(string fileName, string contentType, byte[] data)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException("Filename cannot be null or empty", nameof(fileName));
            }

            if (fileName.Contains("..") || Path.IsPathRooted(fileName))
            {
                throw new ArgumentException("Invalid filename: path traversal not allowed", nameof(fileName));
            }

            var sanitizedFileName = Path.GetFileName(fileName);
            var extension = Path.GetExtension(sanitizedFileName).ToLowerInvariant();

            if (!AllowedExtensions.Contains(extension))
            {
                throw new ArgumentException($"File type '{extension}' is not allowed. Allowed: {string.Join(", ", AllowedExtensions)}");
            }

            if (data.Length > MaxFileSizeBytes)
            {
                throw new ArgumentException($"File size ({data.Length / 1024 / 1024:F1} MB) exceeds maximum allowed size ({MaxFileSizeBytes / 1024 / 1024} MB)");
            }

            var uniqueId = Guid.NewGuid().ToString("N")[..8];
            var baseName = Path.GetFileNameWithoutExtension(sanitizedFileName);
            var uniqueFileName = $"{baseName}_{uniqueId}{extension}";

            var filePath = Path.Combine(_storagePath, uniqueFileName);
            await File.WriteAllBytesAsync(filePath, data);

            return uniqueFileName;
        }

        public async Task<EmailTemplateImageResult> GetAsync(string storagePath)
        {
            if (string.IsNullOrWhiteSpace(storagePath) || storagePath.Contains(".."))
            {
                return null;
            }

            var fileName = Path.GetFileName(storagePath);
            var filePath = Path.Combine(_storagePath, fileName);

            if (!File.Exists(filePath))
            {
                return null;
            }

            var content = await File.ReadAllBytesAsync(filePath);

            if (!new FileExtensionContentTypeProvider().TryGetContentType(fileName, out var mimeType))
            {
                mimeType = "application/octet-stream";
            }

            return new EmailTemplateImageResult
            {
                Content = content,
                FileName = fileName,
                ContentType = mimeType,
            };
        }
    }
}

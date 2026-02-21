using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Orso.Arpa.Domain.TicketDomain.Interfaces;

namespace Orso.Arpa.Infrastructure.FileManagement
{
    public class LocalStorageTicketFileAccessor : ITicketFileAccessor
    {
        private readonly string _storagePath;
        private const string DefaultStoragePath = "/data/ticket-attachments";
        private const long MaxFileSizeBytes = 10 * 1024 * 1024;

        private static readonly string[] AllowedExtensions =
        [
            ".jpg", ".jpeg", ".png", ".gif", ".webp", ".bmp", ".svg",
            ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx",
            ".txt", ".rtf", ".odt", ".ods", ".odp",
            ".mp3", ".wav", ".ogg", ".m4a",
            ".mp4", ".webm", ".mov",
            ".zip", ".rar", ".7z", ".tar", ".gz"
        ];

        public LocalStorageTicketFileAccessor(IConfiguration configuration)
        {
            _storagePath = configuration.GetValue<string>("LocalStorageConfiguration:TicketAttachmentsPath")
                ?? DefaultStoragePath;

            if (!Directory.Exists(_storagePath))
            {
                Directory.CreateDirectory(_storagePath);
            }
        }

        public async Task<string> SaveAsync(Guid ticketId, string fileName, string contentType, byte[] data)
        {
            var sanitizedFileName = Path.GetFileName(fileName);
            if (string.IsNullOrWhiteSpace(sanitizedFileName) || sanitizedFileName.Contains(".."))
                throw new ArgumentException("Invalid filename", nameof(fileName));

            if (data.Length > MaxFileSizeBytes)
                throw new ArgumentException($"File size exceeds maximum allowed size ({MaxFileSizeBytes / 1024 / 1024} MB)");

            var extension = Path.GetExtension(sanitizedFileName).ToLowerInvariant();
            if (!AllowedExtensions.Contains(extension))
                throw new ArgumentException($"File type '{extension}' is not allowed.");

            var ticketDir = Path.Combine(_storagePath, ticketId.ToString());
            if (!Directory.Exists(ticketDir))
                Directory.CreateDirectory(ticketDir);

            var uniqueId = Guid.NewGuid().ToString("N")[..8];
            var baseName = Path.GetFileNameWithoutExtension(sanitizedFileName);
            var uniqueFileName = $"{baseName}_{uniqueId}{extension}";
            var filePath = Path.Combine(ticketDir, uniqueFileName);

            await File.WriteAllBytesAsync(filePath, data);

            return $"{ticketId}/{uniqueFileName}";
        }

        public async Task<TicketAttachmentFileResult> GetAsync(string storagePath)
        {
            if (string.IsNullOrWhiteSpace(storagePath) || storagePath.Contains(".."))
                return null;

            var filePath = Path.Combine(_storagePath, storagePath);
            if (!File.Exists(filePath))
                return null;

            var content = await File.ReadAllBytesAsync(filePath);
            var fileInfo = new FileInfo(filePath);
            var fileName = Path.GetFileName(storagePath);

            if (!new FileExtensionContentTypeProvider().TryGetContentType(fileName, out var mimeType))
                mimeType = "application/octet-stream";

            return new TicketAttachmentFileResult
            {
                Content = content,
                FileName = fileName,
                ContentType = mimeType,
                FileSize = fileInfo.Length
            };
        }

        public Task DeleteAsync(string storagePath)
        {
            if (string.IsNullOrWhiteSpace(storagePath) || storagePath.Contains(".."))
                return Task.CompletedTask;

            var filePath = Path.Combine(_storagePath, storagePath);
            if (File.Exists(filePath))
                File.Delete(filePath);

            return Task.CompletedTask;
        }
    }
}

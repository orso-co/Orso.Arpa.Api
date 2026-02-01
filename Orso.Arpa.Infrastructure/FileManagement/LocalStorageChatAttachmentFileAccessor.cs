using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Orso.Arpa.Domain.ChatDomain.Interfaces;

namespace Orso.Arpa.Infrastructure.FileManagement
{
    /// <summary>
    /// Local file system implementation of IChatAttachmentFileAccessor.
    /// Stores chat attachments in a configurable local directory.
    /// </summary>
    public class LocalStorageChatAttachmentFileAccessor : IChatAttachmentFileAccessor
    {
        private readonly string _storagePath;
        private const string DefaultStoragePath = "/data/chat-attachments";

        /// <summary>
        /// Maximum allowed file size for chat attachments (10 MB).
        /// </summary>
        private const long MaxFileSizeBytes = 10 * 1024 * 1024;

        /// <summary>
        /// Allowed file extensions for chat attachments.
        /// </summary>
        private static readonly string[] AllowedExtensions =
        [
            // Images
            ".jpg", ".jpeg", ".png", ".gif", ".webp", ".bmp", ".svg",
            // Documents
            ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx",
            ".txt", ".rtf", ".odt", ".ods", ".odp",
            // Audio
            ".mp3", ".wav", ".ogg", ".m4a", ".flac", ".aac",
            // Video
            ".mp4", ".webm", ".mov", ".avi", ".mkv",
            // Archives
            ".zip", ".rar", ".7z", ".tar", ".gz",
            // Music notation
            ".xml", ".musicxml", ".mxl"
        ];

        public LocalStorageChatAttachmentFileAccessor(IConfiguration configuration)
        {
            _storagePath = configuration.GetValue<string>("LocalStorageConfiguration:ChatAttachmentsPath")
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
        /// Validates file size and extension.
        /// </summary>
        private static void ValidateFile(string fileName, byte[] data)
        {
            if (data.Length > MaxFileSizeBytes)
            {
                throw new ArgumentException(
                    $"File size ({data.Length / 1024 / 1024:F1} MB) exceeds maximum allowed size ({MaxFileSizeBytes / 1024 / 1024} MB)",
                    nameof(data));
            }

            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            if (!AllowedExtensions.Contains(extension))
            {
                throw new ArgumentException(
                    $"File type '{extension}' is not allowed.",
                    nameof(fileName));
            }
        }

        public async Task<string> SaveAsync(Guid roomId, string fileName, string contentType, byte[] data)
        {
            var sanitizedFileName = SanitizeFileName(fileName);
            ValidateFile(sanitizedFileName, data);

            // Create room subdirectory
            var roomDir = Path.Combine(_storagePath, roomId.ToString());
            if (!Directory.Exists(roomDir))
            {
                Directory.CreateDirectory(roomDir);
            }

            // Generate unique filename to prevent overwriting
            var uniqueId = Guid.NewGuid().ToString("N")[..8];
            var extension = Path.GetExtension(sanitizedFileName);
            var baseName = Path.GetFileNameWithoutExtension(sanitizedFileName);
            var uniqueFileName = $"{baseName}_{uniqueId}{extension}";

            var filePath = Path.Combine(roomDir, uniqueFileName);

            await File.WriteAllBytesAsync(filePath, data);

            // Return relative storage path
            return $"{roomId}/{uniqueFileName}";
        }

        public async Task<ChatAttachmentFileResult> GetAsync(string storagePath)
        {
            if (string.IsNullOrWhiteSpace(storagePath))
            {
                return null;
            }

            // Prevent path traversal
            if (storagePath.Contains(".."))
            {
                throw new ArgumentException("Invalid storage path", nameof(storagePath));
            }

            var filePath = Path.Combine(_storagePath, storagePath);

            if (!File.Exists(filePath))
            {
                return null;
            }

            var content = await File.ReadAllBytesAsync(filePath);
            var fileInfo = new FileInfo(filePath);
            var fileName = Path.GetFileName(storagePath);

            if (!new FileExtensionContentTypeProvider().TryGetContentType(fileName, out var mimeType))
            {
                mimeType = "application/octet-stream";
            }

            return new ChatAttachmentFileResult
            {
                Content = content,
                FileName = fileName,
                ContentType = mimeType,
                FileSize = fileInfo.Length,
                LastModified = fileInfo.LastWriteTimeUtc
            };
        }

        public Task DeleteAsync(string storagePath)
        {
            if (string.IsNullOrWhiteSpace(storagePath) || storagePath.Contains(".."))
            {
                return Task.CompletedTask;
            }

            var filePath = Path.Combine(_storagePath, storagePath);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            return Task.CompletedTask;
        }

        public string GetDownloadUrl(string storagePath)
        {
            // For local storage, the API will serve the file
            // This returns the relative path that the controller will use
            return storagePath;
        }
    }
}

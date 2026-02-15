using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Orso.Arpa.Domain.EmailCampaignDomain.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;

namespace Orso.Arpa.Infrastructure.FileManagement
{
    public class LocalStorageEmailTemplateImageAccessor : IEmailTemplateImageAccessor
    {
        private readonly string _storagePath;
        private readonly ILogger<LocalStorageEmailTemplateImageAccessor> _logger;
        private const string DefaultStoragePath = "/data/email-template-images";
        private const long MaxFileSizeBytes = 10 * 1024 * 1024;
        private const int MaxImageWidth = 1200;
        private const int JpegQuality = 80;

        private static readonly string[] AllowedExtensions =
        [
            ".jpg", ".jpeg", ".png", ".gif", ".webp", ".svg"
        ];

        public LocalStorageEmailTemplateImageAccessor(
            IConfiguration configuration,
            ILogger<LocalStorageEmailTemplateImageAccessor> logger)
        {
            _logger = logger;
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

            // Resize and compress raster images for email use
            if (extension is not ".svg" and not ".gif")
            {
                data = await ResizeAndCompressAsync(data, extension);
            }

            var uniqueId = Guid.NewGuid().ToString("N")[..8];
            var baseName = Path.GetFileNameWithoutExtension(sanitizedFileName);
            var uniqueFileName = $"{baseName}_{uniqueId}{extension}";

            var filePath = Path.Combine(_storagePath, uniqueFileName);
            await File.WriteAllBytesAsync(filePath, data);

            _logger.LogInformation("Saved email template image: {FileName} ({Size} KB)",
                uniqueFileName, data.Length / 1024);

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

        private async Task<byte[]> ResizeAndCompressAsync(byte[] data, string extension)
        {
            try
            {
                using var image = Image.Load(data);
                var originalSize = data.Length;

                // Only resize if wider than max width
                if (image.Width > MaxImageWidth)
                {
                    image.Mutate(x => x.Resize(new ResizeOptions
                    {
                        Size = new Size(MaxImageWidth, 0),
                        Mode = ResizeMode.Max,
                    }));
                }

                using var outputStream = new MemoryStream();

                if (extension is ".jpg" or ".jpeg")
                {
                    await image.SaveAsJpegAsync(outputStream, new JpegEncoder { Quality = JpegQuality });
                }
                else if (extension == ".webp")
                {
                    await image.SaveAsWebpAsync(outputStream, new WebpEncoder { Quality = JpegQuality });
                }
                else if (extension == ".png")
                {
                    await image.SaveAsPngAsync(outputStream, new PngEncoder
                    {
                        CompressionLevel = PngCompressionLevel.BestCompression,
                    });
                }
                else
                {
                    return data;
                }

                byte[] result = outputStream.ToArray();
                _logger.LogInformation("Image optimized: {Original} KB -> {Optimized} KB ({Width}x{Height})",
                    originalSize / 1024, result.Length / 1024, image.Width, image.Height);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to optimize image, using original");
                return data;
            }
        }
    }
}

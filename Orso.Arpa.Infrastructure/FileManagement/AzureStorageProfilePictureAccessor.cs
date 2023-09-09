using System.IO;
using System.Threading.Tasks;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Orso.Arpa.Domain.General.Interfaces;

namespace Orso.Arpa.Infrastructure.FileManagement
{
    public class AzureStorageProfilePictureAccessor : IFileAccessor
    {
        private readonly BlobServiceClient _blobServiceClient;
        private const string ContainerName = "profile-pictures";

        public AzureStorageProfilePictureAccessor(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<IFileResult> SaveAsync(IFormFile file, string fileName = null)
        {
            BlobContainerClient blobContainer = await GetProfilePictureContainerAsync();

            BlobClient blobClient = blobContainer.GetBlobClient(fileName ?? file.FileName);
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!new FileExtensionContentTypeProvider().TryGetContentType(fileName, out var mimeType))
            {
                mimeType = "image/webp";
            }

            Response<BlobContentInfo> response = await blobClient.UploadAsync(file.OpenReadStream(), new BlobHttpHeaders
            {
                ContentType = mimeType,
                ContentDisposition = file.ContentDisposition,
                ContentLanguage = extension,
                CacheControl = "public, must-revalidate, max-age=1"
            });

            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);

            return new FileResult
            {
                Content = ms.ToArray(),
                Name = fileName,
                ContentType = mimeType,
                Extension = extension,
                LastModified = response.Value.LastModified
            };
        }

        public async Task<IFileResult> GetAsync(string fileName)
        {
            BlobContainerClient blobContainer = await GetProfilePictureContainerAsync();

            BlobClient blobClient = blobContainer.GetBlobClient(fileName);
            Response<BlobDownloadInfo> downloadContent = await blobClient.DownloadAsync();
            using var ms = new MemoryStream();
            await downloadContent.Value.Content.CopyToAsync(ms);

            return new FileResult
            {
                Content = ms.ToArray(),
                Name = fileName,
                ContentType = downloadContent.Value.Details.ContentType,
                Extension = downloadContent.Value.Details.ContentLanguage,
                LastModified = downloadContent.Value.Details.LastModified
            };
        }

        public async Task<BlobClient> GetAsBlobAsync(string fileName)
        {
            BlobContainerClient blobContainer = await GetProfilePictureContainerAsync();

            return blobContainer.GetBlobClient(fileName);
        }

        public async Task DeleteAsync(string fileName)
        {
            BlobContainerClient blobContainer = await GetProfilePictureContainerAsync();

            BlobClient blobClient = blobContainer.GetBlobClient(fileName);

            _ = await blobClient.DeleteAsync();
        }

        private async Task<BlobContainerClient> GetProfilePictureContainerAsync()
        {
            BlobContainerClient client = _blobServiceClient.GetBlobContainerClient(ContainerName);
            _ = await client.CreateIfNotExistsAsync();
            return client;
        }
    }
}

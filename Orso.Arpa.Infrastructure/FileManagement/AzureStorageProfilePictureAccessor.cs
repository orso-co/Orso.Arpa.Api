using System.IO;
using System.Threading.Tasks;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Orso.Arpa.Domain.Configuration;

namespace Orso.Arpa.Infrastructure.FileManagement
{
    public class AzureStorageProfilePictureAccessor : IFileAccessor
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly AzureStorageConfiguration _azureStorageConfiguration;

        public AzureStorageProfilePictureAccessor(BlobServiceClient blobServiceClient, AzureStorageConfiguration azureStorageConfiguration)
        {
            _blobServiceClient = blobServiceClient;
            _azureStorageConfiguration = azureStorageConfiguration;
        }

        public async Task<byte[]> SaveAsync(IFormFile model, string fileName = null)
        {
            BlobContainerClient blobContainer = await GetProfilePictureContainerAsync();

            BlobClient blobClient = blobContainer.GetBlobClient(fileName ?? model.FileName);

            _ = await blobClient.UploadAsync(model.OpenReadStream());

            using var ms = new MemoryStream();
            await model.CopyToAsync(ms);
            return ms.ToArray();
        }

        public async Task<byte[]> GetAsync(string fileName)
        {
            BlobContainerClient blobContainer = await GetProfilePictureContainerAsync();

            BlobClient blobClient = blobContainer.GetBlobClient(fileName);
            Response<BlobDownloadInfo> downloadContent = await blobClient.DownloadAsync();
            using var ms = new MemoryStream();
            await downloadContent.Value.Content.CopyToAsync(ms);
            return ms.ToArray();
        }

        public async Task DeleteAsync(string fileName)
        {
            BlobContainerClient blobContainer = await GetProfilePictureContainerAsync();

            BlobClient blobClient = blobContainer.GetBlobClient(fileName);

            _ = await blobClient.DeleteAsync();
        }

        private async Task<BlobContainerClient> GetProfilePictureContainerAsync()
        {
            BlobContainerClient client = _blobServiceClient.GetBlobContainerClient(_azureStorageConfiguration.ProfilePictureContainerName);
            _ = await client.CreateIfNotExistsAsync();
            return client;
        }
    }
}

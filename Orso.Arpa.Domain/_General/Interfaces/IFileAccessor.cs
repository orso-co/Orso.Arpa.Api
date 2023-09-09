using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;

namespace Orso.Arpa.Domain.General.Interfaces
{
    public interface IFileAccessor
    {
        Task<IFileResult> SaveAsync(IFormFile file, string fileName = null);
        Task<IFileResult> GetAsync(string fileName);
        Task DeleteAsync(string fileName);
        Task<BlobClient> GetAsBlobAsync(string fileName);
    }
}

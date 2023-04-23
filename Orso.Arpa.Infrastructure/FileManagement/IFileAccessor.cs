using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Orso.Arpa.Infrastructure.FileManagement
{
    public interface IFileAccessor
    {
        Task<byte[]> SaveAsync(IFormFile model, string fileName = null);
        Task<byte[]> GetAsync(string fileName);
        Task DeleteAsync(string fileName);
    }
}

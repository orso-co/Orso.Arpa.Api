using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Orso.Arpa.Infrastructure.FileManagement
{
    public interface IFileAccessor
    {
        Task<FileResult> SaveAsync(IFormFile model, string fileName = null);
        Task<FileResult> GetAsync(string fileName);
        Task DeleteAsync(string fileName);
    }
}

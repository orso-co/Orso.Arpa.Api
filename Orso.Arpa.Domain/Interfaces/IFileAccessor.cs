using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Orso.Arpa.Domain.Interfaces
{
    public interface IFileAccessor
    {
        Task<IFileResult> SaveAsync(IFormFile file, string fileName = null);
        Task<IFileResult> GetAsync(string fileName);
        Task DeleteAsync(string fileName);
    }
}

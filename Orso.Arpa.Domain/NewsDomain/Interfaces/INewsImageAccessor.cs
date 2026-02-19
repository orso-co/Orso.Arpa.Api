using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Orso.Arpa.Domain.General.Interfaces;

namespace Orso.Arpa.Domain.NewsDomain.Interfaces;

public interface INewsImageAccessor
{
    Task<IFileResult> SaveAsync(IFormFile file, string fileName = null);
    Task<IFileResult> GetAsync(string fileName);
    Task DeleteAsync(string fileName);
}

using Microsoft.AspNetCore.Http;

namespace Orso.Arpa.Domain.General.Interfaces
{
    public interface IFileNameGenerator
    {
        string GenerateRandomFileName(IFormFile formFile);
    }
}

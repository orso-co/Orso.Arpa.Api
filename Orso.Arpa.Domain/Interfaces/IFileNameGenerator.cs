using Microsoft.AspNetCore.Http;

namespace Orso.Arpa.Domain.Interfaces
{
    public interface IFileNameGenerator
    {
        string GenerateRandomFileName(IFormFile formFile);
    }
}

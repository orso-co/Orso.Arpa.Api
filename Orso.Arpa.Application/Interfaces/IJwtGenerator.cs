using System.Threading.Tasks;
using Orso.Arpa.Domain;

namespace Orso.Arpa.Application.Interfaces
{
    public interface IJwtGenerator
    {
        Task<string> CreateTokenAsync(User user);
    }
}

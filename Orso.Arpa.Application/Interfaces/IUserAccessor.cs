using System.Threading.Tasks;
using Orso.Arpa.Domain;

namespace Orso.Arpa.Application.Interfaces
{
    public interface IUserAccessor
    {
        string GetCurrentUsername();
        Task<User> GetCurrentUserAsync();
    }
}

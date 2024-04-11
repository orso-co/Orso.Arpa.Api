using System.Threading.Tasks;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Domain.General.Interfaces
{
    public interface ICookieSignIn
    {
        Task<Task> AsyncSignInUser(User user);
        Task SignOutUser();
        Task<Task> AsyncRefreshSignIn(User user);
        Task<bool> AsyncIsCookieSignInPossible(User user, string password);
    }
}
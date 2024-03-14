using System.Threading.Tasks;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Domain.General.Interfaces
{
    public interface ICookieSignIn
    {
        Task<Task> SignInUserWithClaims(User user, string token);
        Task SignOutUser();
        Task<Task> RefreshSignIn(User user, string token);
        Task<bool> IsCookieSignInPossible(User user, string password);
    }
}
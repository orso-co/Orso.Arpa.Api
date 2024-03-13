using System.Threading.Tasks;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Domain.General.Interfaces
{
    public interface ICookieSignIn
    {
        Task SignInUserWithClaims(User user, string token);
        Task SignOutUser();
        Task RefreshSignIn(string token);
        Task<bool> IsCookieSignInPossible(User user, string password);
    }
}
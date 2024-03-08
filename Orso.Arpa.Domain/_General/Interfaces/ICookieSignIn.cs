using System.Threading.Tasks;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Domain.General.Interfaces
{
    public interface ICookieSignIn
    {
        public Task SignInUserWithClaims(User user, string token);
        public Task RefreshSignIn(string token);
        public Task<bool> IsCookieSignInPossible(User user, string password);
    }
}
using System.Threading.Tasks;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Domain.General.Interfaces
{
    public interface ICookieSignIn
    {
        Task<Task> SignInUser(User user);
        Task SignOutUser();
        Task<Task> RefreshSignIn(User user);
        Task<bool> IsCookieSignInPossible(User user, string password);
    }
}
using System.Threading.Tasks;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Domain.General.Interfaces
{
    public interface ICookieSignIn
    {
        Task<Task> SignInUserAsync(User user);
        Task SignOutUserAsync();
        Task<Task> RefreshSignInAsync(User user);
        Task<bool> IsCookieSignInPossibleAsync(User user, string password);
    }
}
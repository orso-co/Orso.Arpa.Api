using System.Threading.Tasks;
using Orso.Arpa.Application.Logic.Auth;

namespace Orso.Arpa.Application.Interfaces
{
    public interface IAuthService
    {
        Task<TokenDto> LoginAsync(Login.Dto loginDto);

        Task<TokenDto> RegisterAsync(UserRegister.Dto registerDto);

        Task ChangePasswordAsync(ChangePassword.Dto changePasswordDto);

        Task SetRoleAsync(SetRole.Dto setRoleDto);
    }
}

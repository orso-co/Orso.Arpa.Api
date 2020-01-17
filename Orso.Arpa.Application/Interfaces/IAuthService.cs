using System.Threading.Tasks;
using Orso.Arpa.Application.Dtos;

namespace Orso.Arpa.Application.Interfaces
{
    public interface IAuthService
    {
        Task<TokenDto> LoginAsync(LoginDto loginDto);
        Task<TokenDto> RegisterAsync(UserRegisterDto registerDto);
        Task ChangePasswordAsync(ChangePasswordDto changePasswordDto);
        Task SetRoleAsync(SetRoleDto setRoleDto);
    }
}

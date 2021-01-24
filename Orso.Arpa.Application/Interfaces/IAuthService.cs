using System.Threading.Tasks;
using Orso.Arpa.Application.AuthApplication;

namespace Orso.Arpa.Application.Interfaces
{
    public interface IAuthService
    {
        Task<TokenDto> LoginAsync(LoginDto loginDto);

        Task RegisterAsync(UserRegisterDto registerDto);

        Task ChangePasswordAsync(ChangePasswordDto changePasswordDto);

        Task SetRoleAsync(SetRoleDto setRoleDto);

        Task ForgotPasswordAsync(ForgotPasswordDto forgotPassswordDto);

        Task ResetPasswordAsync(ResetPasswordDto resetPasswordDto);

        Task ConfirmEmailAsync(ConfirmEmailDto confirmEmailDto);

        Task CreateNewEmailConfirmationTokenAsync(CreateEmailConfirmationTokenDto createEmailConfirmationTokenDto);

        Task<TokenDto> RefreshAccessTokenAsync(string refreshToken, string remoteIpAddress);

        Task RevokeRefreshTokenAsync(string refreshToken, string remoteIpAddress);
    }
}

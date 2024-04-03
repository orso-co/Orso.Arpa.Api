using System.Threading.Tasks;
using Orso.Arpa.Application.AuthApplication.Model;

namespace Orso.Arpa.Application.AuthApplication.Interfaces
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(LoginDto loginDto, string remoteIpAddress);

        Task RegisterAsync(UserRegisterDto registerDto);

        Task ChangePasswordAsync(ChangePasswordDto changePasswordDto);

        Task SetRoleAsync(SetRoleDto setRoleDto);

        Task ForgotPasswordAsync(ForgotPasswordDto forgotPassswordDto);

        Task ResetPasswordAsync(ResetPasswordDto resetPasswordDto);

        Task ConfirmEmailAsync(ConfirmEmailDto confirmEmailDto);

        Task CreateNewEmailConfirmationTokenAsync(CreateEmailConfirmationTokenDto createEmailConfirmationTokenDto);

        Task<bool> RefreshAccessTokenAsync(string refreshToken, string remoteIpAddress);

        Task RevokeRefreshTokenAsync(string refreshToken, string remoteIpAddress);
        Task SignOut(string refreshToken, string remoteIpAddress);
    }
}

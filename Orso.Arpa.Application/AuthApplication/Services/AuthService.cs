using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.AuthApplication.Interfaces;
using Orso.Arpa.Application.AuthApplication.Model;
using Orso.Arpa.Domain.UserDomain.Commands;
using Orso.Arpa.Domain.UserDomain.Enums;
using Orso.Arpa.Domain.UserDomain.Notifications;

namespace Orso.Arpa.Application.AuthApplication.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public AuthService(IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<bool> LoginAsync(LoginDto loginDto, string remoteIpAddress)
        {
            LoginUser.Command command = _mapper.Map<LoginUser.Command>(loginDto);
            command.RemoteIpAddress = remoteIpAddress;
            return await _mediator.Send(command);
        }

        public async Task RegisterAsync(UserRegisterDto registerDto)
        {
            RegisterUser.Command registerCommand = _mapper.Map<RegisterUser.Command>(registerDto);
            await _mediator.Send(registerCommand);

            CreateEmailConfirmationToken.Command command = _mapper.Map<CreateEmailConfirmationToken.Command>(registerDto);
            await _mediator.Send(command);

            UserRegisteredNotification userRegisteredNotification = new UserRegisteredNotification { UserName = registerDto.UserName };
            await _mediator.Publish(userRegisteredNotification);
        }

        public async Task ChangePasswordAsync(ChangePasswordDto changePasswordDto)
        {
            ChangePassword.Command command = _mapper.Map<ChangePassword.Command>(changePasswordDto);
            await _mediator.Send(command);
            var emailCommand = new SendPasswordChangedInfo.Command();
            await _mediator.Send(emailCommand);
        }

        public async Task SetRoleAsync(SetRoleDto setRoleDto)
        {
            SetRole.Command command = _mapper.Map<SetRole.Command>(setRoleDto);
            var isNewUser = await _mediator.Send(command);

            if (isNewUser)
            {
                SendActivationInfo.Command activationCommand = _mapper.Map<SendActivationInfo.Command>(setRoleDto);
                await _mediator.Send(activationCommand);
            }

            if (setRoleDto.RoleNames.Contains(RoleNames.Performer))
            {
                SendMyQRCode.Command codeCommand = _mapper.Map<SendMyQRCode.Command>(setRoleDto);
                codeCommand.SendEmail = true;
                await _mediator.Send(codeCommand);
            }
        }

        public async Task ForgotPasswordAsync(ForgotPasswordDto forgotPassswordDto)
        {
            CreateResetPasswordToken.Command command = _mapper.Map<CreateResetPasswordToken.Command>(forgotPassswordDto);
            await _mediator.Send(command);
        }

        public async Task ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            ResetPassword.Command command = _mapper.Map<ResetPassword.Command>(resetPasswordDto);
            await _mediator.Send(command);
            SendPasswordChangedInfo.Command emailCommand = _mapper.Map<SendPasswordChangedInfo.Command>(resetPasswordDto);
            await _mediator.Send(emailCommand);
        }

        public async Task ConfirmEmailAsync(ConfirmEmailDto confirmEmailDto)
        {
            ConfirmEmail.Command command = _mapper.Map<ConfirmEmail.Command>(confirmEmailDto);
            await _mediator.Send(command);

            EmailConfirmedNotification emailConfirmedNotification = new EmailConfirmedNotification { Email = confirmEmailDto.Email };
            await _mediator.Publish(emailConfirmedNotification);
        }

        public async Task CreateNewEmailConfirmationTokenAsync(CreateEmailConfirmationTokenDto createEmailConfirmationTokenDto)
        {
            CreateEmailConfirmationToken.Command command = _mapper.Map<CreateEmailConfirmationToken.Command>(createEmailConfirmationTokenDto);
            await _mediator.Send(command);
        }

        public async Task<TokenDto> RefreshAccessTokenAsync(string refreshToken, string remoteIpAddress)
        {
            var command = new RefreshAccessToken.Command { RefreshToken = refreshToken, RemoteIpAddress = remoteIpAddress };
            var token = await _mediator.Send(command);
            await RevokeRefreshTokenAsync(refreshToken, remoteIpAddress);
            return _mapper.Map<TokenDto>(token);
        }

        public async Task RevokeRefreshTokenAsync(string refreshToken, string remoteIpAddress)
        {
            var command = new RevokeRefreshToken.Command { RefreshToken = refreshToken, RemoteIpAddress = remoteIpAddress };
            await _mediator.Send(command);
        }
    }
}

using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.AuthApplication;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Domain.Logic.Auth;
using Orso.Arpa.Domain.Logic.Me;
using Orso.Arpa.Domain.Roles;

namespace Orso.Arpa.Application.Services
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

        public async Task<TokenDto> LoginAsync(LoginDto loginDto, string remoteIpAddress)
        {
            Login.Command command = _mapper.Map<Login.Command>(loginDto);
            command.RemoteIpAddress = remoteIpAddress;
            var token = await _mediator.Send(command);
            return _mapper.Map<TokenDto>(token);
        }

        public async Task RegisterAsync(UserRegisterDto registerDto)
        {
            UserRegister.Command registerCommand = _mapper.Map<UserRegister.Command>(registerDto);
            await _mediator.Send(registerCommand);
            CreateEmailConfirmationToken.Command command = _mapper.Map<CreateEmailConfirmationToken.Command>(registerDto);
            await _mediator.Send(command);
        }

        public async Task ChangePasswordAsync(ChangePasswordDto changePasswordDto)
        {
            ChangePassword.Command command = _mapper.Map<ChangePassword.Command>(changePasswordDto);
            await _mediator.Send(command);
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
                SendQRCode.Command codeCommand = _mapper.Map<SendQRCode.Command>(setRoleDto);
                await _mediator.Send(codeCommand);
            }
        }

        public async Task ForgotPasswordAsync(ForgotPasswordDto forgotPassswordDto)
        {
            ForgotPassword.Command command = _mapper.Map<ForgotPassword.Command>(forgotPassswordDto);
            await _mediator.Send(command);
        }

        public async Task ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            ResetPassword.Command command = _mapper.Map<ResetPassword.Command>(resetPasswordDto);
            await _mediator.Send(command);
        }

        public async Task ConfirmEmailAsync(ConfirmEmailDto confirmEmailDto)
        {
            ConfirmEmail.Command command = _mapper.Map<ConfirmEmail.Command>(confirmEmailDto);
            await _mediator.Send(command);
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
            var revokeCommand = new RevokeRefreshToken.Command { RefreshToken = refreshToken, RemoteIpAddress = remoteIpAddress };
            await _mediator.Send(revokeCommand);
            return _mapper.Map<TokenDto>(token);
        }

        public async Task RevokeRefreshTokenAsync(string refreshToken, string remoteIpAddress)
        {
            var command = new RevokeRefreshToken.Command { RefreshToken = refreshToken, RemoteIpAddress = remoteIpAddress };
            await _mediator.Send(command);
        }
    }
}

using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Application.AuthApplication.Interfaces;
using Orso.Arpa.Application.AuthApplication.Model;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.UserDomain.Commands;
using Orso.Arpa.Domain.UserDomain.Enums;
using Orso.Arpa.Domain.UserDomain.Model;
using Orso.Arpa.Domain.UserDomain.Notifications;
using Orso.Arpa.Domain.UserDomain.Repositories;

namespace Orso.Arpa.Application.AuthApplication.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ArpaUserManager _userManager;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IUserAccessor _userAccessor;
        private readonly IArpaContext _arpaContext;

        public AuthService(
            IMediator mediator,
            IMapper mapper,
            ArpaUserManager userManager,
            IJwtGenerator jwtGenerator,
            IUserAccessor userAccessor,
            IArpaContext arpaContext)
        {
            _mapper = mapper;
            _mediator = mediator;
            _userManager = userManager;
            _jwtGenerator = jwtGenerator;
            _userAccessor = userAccessor;
            _arpaContext = arpaContext;
        }

        public async Task<TokenDto> LoginAsync(LoginDto loginDto, string remoteIpAddress)
        {
            LoginUser.Command command = _mapper.Map<LoginUser.Command>(loginDto);
            command.RemoteIpAddress = remoteIpAddress;
            var token = await _mediator.Send(command);
            return _mapper.Map<TokenDto>(token);
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

        public async Task SendSupportRequestAsync(SupportRequestDto supportRequestDto)
        {
            SendSupportRequest.Command command = _mapper.Map<SendSupportRequest.Command>(supportRequestDto);
            await _mediator.Send(command);
        }

        public async Task<TokenDto> ImpersonateAsync(Guid personId)
        {
            User adminUser = await _userAccessor.GetCurrentUserAsync();

            User targetUser = await _userManager.Users
                .FirstOrDefaultAsync(u => u.PersonId == personId)
                ?? throw new InvalidOperationException($"No user account found for person {personId}");

            if (targetUser.Id == adminUser.Id)
            {
                throw new InvalidOperationException("Cannot impersonate yourself");
            }

            string token = await _jwtGenerator.CreateImpersonationTokenAsync(targetUser, adminUser);
            return new TokenDto { Token = token };
        }
    }
}

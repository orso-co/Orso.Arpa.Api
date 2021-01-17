using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.AuthApplication;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Domain.Logic.Auth;

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

        public async Task<TokenDto> LoginAsync(LoginDto loginDto)
        {
            Login.Command query = _mapper.Map<Login.Command>(loginDto);
            var token = await _mediator.Send(query);
            return _mapper.Map<TokenDto>(token);
        }

        public async Task<TokenDto> RegisterAsync(UserRegisterDto registerDto)
        {
            UserRegister.Command command = _mapper.Map<UserRegister.Command>(registerDto);
            var token = await _mediator.Send(command);
            return _mapper.Map<TokenDto>(token);
        }

        public async Task ChangePasswordAsync(ChangePasswordDto changePasswordDto)
        {
            ChangePassword.Command command = _mapper.Map<ChangePassword.Command>(changePasswordDto);
            await _mediator.Send(command);
        }

        public async Task SetRoleAsync(SetRoleDto setRoleDto)
        {
            SetRole.Command command = _mapper.Map<SetRole.Command>(setRoleDto);
            await _mediator.Send(command);
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
    }
}

using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.Logic.Auth;

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

        public async Task<TokenDto> LoginAsync(Logic.Auth.Login.Dto loginDto)
        {
            Domain.Logic.Auth.Login.Query query = _mapper.Map<Domain.Logic.Auth.Login.Query>(loginDto);
            var token = await _mediator.Send(query);
            return _mapper.Map<TokenDto>(token);
        }

        public async Task<TokenDto> RegisterAsync(Logic.Auth.UserRegister.Dto registerDto)
        {
            Domain.Logic.Auth.UserRegister.Command command = _mapper.Map<Domain.Logic.Auth.UserRegister.Command>(registerDto);
            var token = await _mediator.Send(command);
            return _mapper.Map<TokenDto>(token);
        }

        public async Task ChangePasswordAsync(Logic.Auth.ChangePassword.Dto changePasswordDto)
        {
            Domain.Logic.Auth.ChangePassword.Command command = _mapper.Map<Domain.Logic.Auth.ChangePassword.Command>(changePasswordDto);
            await _mediator.Send(command);
        }

        public async Task SetRoleAsync(Logic.Auth.SetRole.Dto setRoleDto)
        {
            Domain.Logic.Auth.SetRole.Command command = _mapper.Map<Domain.Logic.Auth.SetRole.Command>(setRoleDto);
            await _mediator.Send(command);
        }
    }
}

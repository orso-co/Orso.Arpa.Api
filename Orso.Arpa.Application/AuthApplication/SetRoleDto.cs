using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Domain.Logic.Auth;

namespace Orso.Arpa.Application.AuthApplication
{
    public class SetRoleDto
    {
        public string Username { get; set; }
        public string RoleName { get; set; }
    }

    public class SetRoleDtoMappingProfile : Profile
    {
        public SetRoleDtoMappingProfile()
        {
            CreateMap<SetRoleDto, SetRole.Command>();
            CreateMap<SetRoleDto, SendActivationInfo.Command>();
            CreateMap<SetRoleDto, SendQRCode.Command>();
        }
    }

    public class SetRoleDtoValidator : AbstractValidator<SetRoleDto>
    {
        public SetRoleDtoValidator()
        {
            RuleFor(c => c.Username)
                .NotEmpty()
                .Username();
        }
    }
}

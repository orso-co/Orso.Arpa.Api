using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Extensions;
using static Orso.Arpa.Domain.Logic.Auth.SetRole;

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
            CreateMap<SetRoleDto, Command>();
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

using AutoMapper;
using FluentValidation;
using static Orso.Arpa.Domain.Logic.Auth.SetRole;

namespace Orso.Arpa.Application.AuthApplication
{
    public class SetRoleDto
    {
        public string UserName { get; set; }
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
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(c => c.UserName)
                .NotEmpty();
        }
    }
}

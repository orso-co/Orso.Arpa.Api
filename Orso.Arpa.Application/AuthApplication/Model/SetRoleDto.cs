using System.Collections.Generic;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General.Extensions;
using Orso.Arpa.Domain.UserDomain.Commands;

namespace Orso.Arpa.Application.AuthApplication.Model
{
    public class SetRoleDto
    {
        public string Username { get; set; }
        public IEnumerable<string> RoleNames { get; set; }
    }

    public class SetRoleDtoMappingProfile : Profile
    {
        public SetRoleDtoMappingProfile()
        {
            CreateMap<SetRoleDto, SetRole.Command>();
            CreateMap<SetRoleDto, SendActivationInfo.Command>();
            CreateMap<SetRoleDto, SendMyQRCode.Command>();
        }
    }

    public class SetRoleDtoValidator : AbstractValidator<SetRoleDto>
    {
        public SetRoleDtoValidator()
        {
            RuleFor(c => c.Username)
                .NotEmpty()
                .Username();
            RuleFor(c => c.RoleNames)
                .NotNull();
        }
    }
}

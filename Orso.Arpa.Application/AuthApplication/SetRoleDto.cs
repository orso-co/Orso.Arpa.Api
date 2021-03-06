using System.Collections.Generic;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Application.Resources.Cultures;
using Orso.Arpa.Domain.Logic.Auth;
using Orso.Arpa.Domain.Logic.Me;

namespace Orso.Arpa.Application.AuthApplication
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
            CreateMap<SetRoleDto, SendQRCode.Command>();
        }
    }

    public class SetRoleDtoValidator : AbstractValidator<SetRoleDto>
    {
        public SetRoleDtoValidator(IStringLocalizer<ApplicationResource> localizer)
        {
            RuleFor(c => c.Username)
                .NotEmpty()
                .Username(localizer);
            RuleFor(c => c.RoleNames)
                .NotNull();
        }
    }
}

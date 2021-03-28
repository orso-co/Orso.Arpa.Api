using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Orso.Arpa.Application.Extensions;
using static Orso.Arpa.Domain.Logic.Auth.ChangePassword;

namespace Orso.Arpa.Application.AuthApplication
{
    public class ChangePasswordDto
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }

    public class ChangePasswordDtoMappingProfile : Profile
    {
        public ChangePasswordDtoMappingProfile()
        {
            CreateMap<ChangePasswordDto, Command>();
        }
    }

    public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordDtoValidator(IStringLocalizer<ApplicationResource> localizer)
        {
            RuleFor(c => c.NewPassword)
                .Password(localizer);
            RuleFor(c => c.CurrentPassword)
                .NotEmpty();
        }
    }
}

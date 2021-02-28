using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Application.Resources.Cultures;
using static Orso.Arpa.Domain.Logic.Auth.Login;

namespace Orso.Arpa.Application.AuthApplication
{
    public class LoginDto
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }

    public class LoginDtoMappingProfile : Profile
    {
        public LoginDtoMappingProfile()
        {
            CreateMap<LoginDto, Command>();
        }
    }

    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator(IStringLocalizer<ApplicationValidators> localizer)
        {
            RuleFor(q => q.UsernameOrEmail)
                .NotEmpty();
            RuleFor(q => q.Password)
                .NotEmpty();
            When(dto => dto.UsernameOrEmail != null && dto.UsernameOrEmail.Contains('@'), () =>
            {
                RuleFor(dto => dto.UsernameOrEmail).EmailAddress().MaximumLength(256);
            }).Otherwise(() =>
            {
                RuleFor(dto => dto.UsernameOrEmail).Username(localizer);
            });
        }
    }
}

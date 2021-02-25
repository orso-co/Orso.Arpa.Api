using System;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Application.Resources.Cultures;
using static Orso.Arpa.Domain.Logic.Auth.ResetPassword;

namespace Orso.Arpa.Application.AuthApplication
{
    public class ResetPasswordDto
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }

    public class ResetPasswordDtoValidator : AbstractValidator<ResetPasswordDto>
    {
        public ResetPasswordDtoValidator(IStringLocalizer<ApplicationValidators> localizer)
        {
            RuleFor(c => c.UsernameOrEmail)
                .NotEmpty();
            RuleFor(c => c.Password)
                .Password(localizer);
            RuleFor(c => c.Token)
                .NotEmpty();
            When(dto => dto.UsernameOrEmail != null && dto.UsernameOrEmail.Contains('@'), () =>
            {
                RuleFor(dto => dto.UsernameOrEmail).EmailAddress().MaximumLength(256);
            }).Otherwise(() =>
            {
                RuleFor(dto => dto.UsernameOrEmail).Username();
            });
        }
    }

    public class ResetPasswordDtoMappingProfile : Profile
    {
        public ResetPasswordDtoMappingProfile()
        {
            CreateMap<ResetPasswordDto, Command>()
                .ForMember(cmd => cmd.Token, opt => opt.MapFrom(dto => Uri.UnescapeDataString(dto.Token)));
        }
    }
}

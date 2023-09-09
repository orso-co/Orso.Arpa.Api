using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General.Extensions;
using Orso.Arpa.Domain.UserDomain.Commands;

namespace Orso.Arpa.Application.AuthApplication.Model
{
    public class ResetPasswordDto
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }

    public class ResetPasswordDtoValidator : AbstractValidator<ResetPasswordDto>
    {
        public ResetPasswordDtoValidator()
        {
            RuleFor(c => c.UsernameOrEmail)
                .NotEmpty();
            RuleFor(c => c.Password)
                .Password();
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
            CreateMap<ResetPasswordDto, ResetPassword.Command>()
                .ForMember(cmd => cmd.Token, opt => opt.MapFrom(dto => Uri.UnescapeDataString(dto.Token)));
            CreateMap<ResetPasswordDto, SendPasswordChangedInfo.Command>();
        }
    }
}

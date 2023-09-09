using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General.Extensions;
using Orso.Arpa.Domain.UserDomain.Commands;

namespace Orso.Arpa.Application.AuthApplication.Model
{
    public class ForgotPasswordDto
    {
        public string UsernameOrEmail { get; set; }
        public string ClientUri { get; set; }
    }

    public class ForgotPasswordDtoMappingProfile : Profile
    {
        public ForgotPasswordDtoMappingProfile()
        {
            CreateMap<ForgotPasswordDto, CreateResetPasswordToken.Command>();
        }
    }

    public class ForgotPasswordDtoValidator : AbstractValidator<ForgotPasswordDto>
    {
        public ForgotPasswordDtoValidator()
        {
            RuleFor(q => q.UsernameOrEmail)
                .NotEmpty();
            RuleFor(q => q.ClientUri)
                .ValidUri();
            When(dto => dto.UsernameOrEmail != null && dto.UsernameOrEmail.Contains('@'), () =>
            {
                RuleFor(dto => dto.UsernameOrEmail).EmailAddress().MaximumLength(256);
            }).Otherwise(() =>
            {
                RuleFor(dto => dto.UsernameOrEmail).Username();
            });
        }
    }
}

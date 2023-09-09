using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General.Extensions;
using Orso.Arpa.Domain.UserDomain.Commands;

namespace Orso.Arpa.Application.AuthApplication.Model
{
    public class CreateEmailConfirmationTokenDto
    {
        public string UsernameOrEmail { get; set; }
        public string ClientUri { get; set; }
    }

    public class CreateEmailConfirmationTokenDtoValidator : AbstractValidator<CreateEmailConfirmationTokenDto>
    {
        public CreateEmailConfirmationTokenDtoValidator()
        {
            RuleFor(c => c.UsernameOrEmail)
               .NotEmpty();
            RuleFor(c => c.ClientUri)
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

    public class CreateEmailConfirmationTokenDtoMappingProfile : Profile
    {
        public CreateEmailConfirmationTokenDtoMappingProfile()
        {
            CreateMap<CreateEmailConfirmationTokenDto, CreateEmailConfirmationToken.Command>();
        }
    }
}

using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Extensions;
using static Orso.Arpa.Domain.Logic.Auth.CreateEmailConfirmationToken;

namespace Orso.Arpa.Application.AuthApplication
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
            CreateMap<CreateEmailConfirmationTokenDto, Command>();
        }
    }
}

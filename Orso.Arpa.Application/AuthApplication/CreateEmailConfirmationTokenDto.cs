using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Extensions;
using static Orso.Arpa.Domain.Logic.Auth.CreateEmailConfirmationToken;

namespace Orso.Arpa.Application.AuthApplication
{
    public class CreateEmailConfirmationTokenDto
    {
        public string Email { get; set; }
        public string ClientUri { get; set; }
    }

    public class CreateEmailConfirmationTokenDtoValidator : AbstractValidator<CreateEmailConfirmationTokenDto>
    {
        public CreateEmailConfirmationTokenDtoValidator()
        {
            RuleFor(c => c.Email)
               .NotEmpty()
               .EmailAddress();
            RuleFor(c => c.ClientUri)
                .ValidUri();
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

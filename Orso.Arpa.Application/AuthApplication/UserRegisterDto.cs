using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Extensions;
using static Orso.Arpa.Domain.Logic.Auth.UserRegister;

namespace Orso.Arpa.Application.AuthApplication
{
    public class UserRegisterDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string ClientUri { get; set; }
    }

    public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterDtoValidator()
        {
            RuleFor(c => c.UserName)
                .NotEmpty();
            RuleFor(c => c.Password)
                .Password();
            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(c => c.GivenName)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(c => c.Surname)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(c => c.ClientUri)
                .ValidUri();
        }
    }

    public class UserRegisterDtoMappingProfile : Profile
    {
        public UserRegisterDtoMappingProfile()
        {
            CreateMap<UserRegisterDto, Command>();
        }
    }
}

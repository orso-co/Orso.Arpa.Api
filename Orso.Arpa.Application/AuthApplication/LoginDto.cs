using AutoMapper;
using FluentValidation;
using static Orso.Arpa.Domain.Logic.Auth.Login;

namespace Orso.Arpa.Application.AuthApplication
{
    public class LoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginDtoMappingProfile : Profile
    {
        public LoginDtoMappingProfile()
        {
            CreateMap<LoginDto, Query>();
        }
    }

    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(q => q.UserName)
                .NotEmpty();
            RuleFor(q => q.Password)
                .NotEmpty();
        }
    }
}

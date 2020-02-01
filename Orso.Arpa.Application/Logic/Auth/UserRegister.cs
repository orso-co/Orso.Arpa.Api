using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Validation;
using static Orso.Arpa.Domain.Logic.Auth.UserRegister;

namespace Orso.Arpa.Application.Logic.Auth
{
    public static class UserRegister
    {
        public class Dto
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
            public string GivenName { get; set; }
            public string Surname { get; set; }
        }

        public class Validator : AbstractValidator<Dto>
        {
            public Validator()
            {
                CascadeMode = CascadeMode.StopOnFirstFailure;
                RuleFor(c => c.UserName)
                    .NotEmpty();
                RuleFor(c => c.Password)
                    .Password();
                RuleFor(c => c.Email)
                    .NotEmpty()
                    .EmailAddress();
                RuleFor(c => c.GivenName)
                    .NotEmpty();
                RuleFor(c => c.Surname)
                    .NotEmpty();
            }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Dto, Command>();
            }
        }
    }
}

using AutoMapper;
using FluentValidation;
using static Orso.Arpa.Domain.Logic.Auth.Login;

namespace Orso.Arpa.Application.Logic.Auth
{
    public static class Login
    {
        public class Dto
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Dto, Query>();
            }
        }

        public class Validator : AbstractValidator<Dto>
        {
            public Validator()
            {
                CascadeMode = CascadeMode.StopOnFirstFailure;
                RuleFor(q => q.UserName)
                    .NotEmpty();
                RuleFor(q => q.Password)
                    .NotEmpty();
            }
        }
    }
}

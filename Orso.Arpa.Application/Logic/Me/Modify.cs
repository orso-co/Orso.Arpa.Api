using AutoMapper;
using FluentValidation;
using static Orso.Arpa.Domain.Logic.Me.Modify;

namespace Orso.Arpa.Application.Logic.Me
{
    public static class Modify
    {
        public class Dto
        {
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string GivenName { get; set; }
            public string Surname { get; set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Dto, Command>();
            }
        }

        public class Validator : AbstractValidator<Dto>
        {
            public Validator()
            {
                CascadeMode = CascadeMode.StopOnFirstFailure;
                RuleFor(c => c.Email)
                    .NotEmpty()
                    .EmailAddress();
                RuleFor(c => c.GivenName)
                    .NotEmpty();
                RuleFor(c => c.Surname)
                    .NotEmpty();
            }
        }
    }
}

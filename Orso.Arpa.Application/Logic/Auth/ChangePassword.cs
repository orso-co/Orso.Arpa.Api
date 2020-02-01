using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Validation;
using static Orso.Arpa.Domain.Logic.Auth.ChangePassword;

namespace Orso.Arpa.Application.Logic.Auth
{
    public static class ChangePassword
    {
        public class Dto
        {
            public string CurrentPassword { get; set; }
            public string NewPassword { get; set; }
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
                RuleFor(c => c.NewPassword)
                    .Password();
                RuleFor(c => c.CurrentPassword)
                    .NotEmpty();
            }
        }
    }
}

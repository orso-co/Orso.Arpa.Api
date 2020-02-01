using AutoMapper;
using FluentValidation;
using static Orso.Arpa.Domain.Logic.Auth.SetRole;

namespace Orso.Arpa.Application.Logic.Auth
{
    public static class SetRole
    {
        public class Dto
        {
            public string UserName { get; set; }
            public string RoleName { get; set; }
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
                RuleFor(c => c.UserName)
                    .NotEmpty();
            }
        }
    }
}

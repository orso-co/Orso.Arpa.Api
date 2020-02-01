using AutoMapper;
using FluentValidation;
using static Orso.Arpa.Domain.Logic.Regions.Create;

namespace Orso.Arpa.Application.Logic.Regions
{
    public static class Create
    {
        public class Dto
        {
            public string Name { get; set; }
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
                RuleFor(c => c.Name)
                    .NotEmpty();
            }
        }
    }
}

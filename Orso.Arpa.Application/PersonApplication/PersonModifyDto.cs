using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General;
using static Orso.Arpa.Domain.Logic.Persons.Modify;

namespace Orso.Arpa.Application.PersonApplication
{
    public class PersonModifyDto : IdFromRouteDto<PersonModifyBodyDto>
    {
    }

    public class PersonModifyBodyDto
    {
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string AboutMe { get; set; }
    }

    public class PersonModifyDtoMappingProfile : Profile
    {
        public PersonModifyDtoMappingProfile()
        {
            CreateMap<PersonModifyDto, Command>()
                .ForMember(dest => dest.GivenName, opt => opt.MapFrom(src => src.Body.GivenName))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Body.Surname))
                .ForMember(dest => dest.AboutMe, opt => opt.MapFrom(src => src.Body.AboutMe));
        }
    }

    public class PersonModifyDtoValidator : BaseModifyDtoValidator<PersonModifyDto, PersonModifyBodyDto>
    {
        public PersonModifyDtoValidator()
        {
            RuleFor(d => d.Body)
                .SetValidator(new PersonModifyBodyDtoValidator());
        }
    }

    public class PersonModifyBodyDtoValidator : AbstractValidator<PersonModifyBodyDto>
    {
        public PersonModifyBodyDtoValidator()
        {
            RuleFor(c => c.GivenName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(c => c.Surname)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(c => c.AboutMe)
                .NotEmpty()
                .MaximumLength(1000);
        }
    }
}

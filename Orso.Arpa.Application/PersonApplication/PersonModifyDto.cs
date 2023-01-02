using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Extensions;
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
        public Guid GenderId { get; set; }
        public Guid? ContactViaId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Birthplace { get; set; }
        public string Background { get; set; }
        public byte ExperienceLevel { get; set; }
        public string BirthName { get; set; }
        public byte Reliability { get; set; }
        public byte GeneralPreference { get; set; }
    }

    public class PersonModifyDtoMappingProfile : Profile
    {
        public PersonModifyDtoMappingProfile()
        {
            CreateMap<PersonModifyDto, Command>()
                .ForMember(dest => dest.GivenName, opt => opt.MapFrom(src => src.Body.GivenName))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Body.Surname))
                .ForMember(dest => dest.GenderId, opt => opt.MapFrom(src => src.Body.GenderId))
                .ForMember(dest => dest.AboutMe, opt => opt.MapFrom(src => src.Body.AboutMe))
                .ForMember(dest => dest.ContactViaId, opt => opt.MapFrom(src => src.Body.ContactViaId))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.Body.DateOfBirth.HasValue ? src.Body.DateOfBirth.Value.Date : (DateTime?)null))
                .ForMember(dest => dest.Birthplace, opt => opt.MapFrom(src => src.Body.Birthplace))
                .ForMember(dest => dest.Background, opt => opt.MapFrom(src => src.Body.Background))
                .ForMember(dest => dest.ExperienceLevel, opt => opt.MapFrom(src => src.Body.ExperienceLevel))
                .ForMember(dest => dest.BirthName, opt => opt.MapFrom(src => src.Body.BirthName))
                .ForMember(dest => dest.Reliability, opt => opt.MapFrom(src => src.Body.Reliability))
                .ForMember(dest => dest.GeneralPreference, opt => opt.MapFrom(src => src.Body.GeneralPreference));
        }
    }

    public class PersonModifyDtoValidator : IdFromRouteDtoValidator<PersonModifyDto, PersonModifyBodyDto>
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
                .PersonName(50);

            RuleFor(c => c.Surname)
                .NotEmpty()
                .PersonName(50);

            RuleFor(c => c.AboutMe)
                .RestrictedFreeText(1000);

            RuleFor(c => c.GenderId)
                .NotEmpty();

            RuleFor(c => c.Birthplace)
                .PlaceName(50);

            RuleFor(c => c.Background)
                .PlaceName(500);

            RuleFor(c => c.ExperienceLevel)
                .FiveStarRating();

            RuleFor(c => c.BirthName)
                .PersonName(50);

            RuleFor(c => c.Reliability)
                .FiveStarRating();

            RuleFor(c => c.GeneralPreference)
                .FiveStarRating();
        }
    }
}

using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General.Extensions;
using Orso.Arpa.Domain.PersonDomain.Commands;

namespace Orso.Arpa.Application.PersonApplication.Model
{
    public class PersonCreateDto
    {
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string AboutMe { get; set; }
        public Guid GenderId { get; set; }
        public Guid? ContactViaId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Birthplace { get; set; }
        public string PersonBackgroundTeam { get; set; }
        public byte ExperienceLevel { get; set; }
        public string BirthName { get; set; }
        public byte Reliability { get; set; }
        public byte GeneralPreference { get; set; }
    }

    public class PersonCreateDtoMappingProfile : Profile
    {
        public PersonCreateDtoMappingProfile()
        {
            CreateMap<PersonCreateDto, CreatePerson.Command>();
        }
    }

    public class PersonCreateDtoValidator : AbstractValidator<PersonCreateDto>
    {
        public PersonCreateDtoValidator()
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

            RuleFor(c => c.PersonBackgroundTeam)
                .RestrictedFreeText(1000);

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

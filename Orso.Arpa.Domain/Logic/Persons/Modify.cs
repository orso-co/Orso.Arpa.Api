using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;
using static Orso.Arpa.Domain.GenericHandlers.Modify;

namespace Orso.Arpa.Domain.Logic.Persons
{
    public static class Modify
    {
        public class Command : IModifyCommand<Person>
        {
            public Guid Id { get; set; }
            public string GivenName { get; set; }
            public string Surname { get; set; }
            public string BirthName { get; set; }
            public string AboutMe { get; set; }
            public Guid GenderId { get; set; }
            public DateTime? DateOfBirth { get; set; }
            public string Birthplace { get; set; }
            public Guid? ContactViaId { get; set; }
            public byte ExperienceLevel { get; set; }
            public byte Reliability { get; set; }
            public byte GeneralPreference { get; set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Command, Person>()
                    .ForMember(dest => dest.GivenName, opt => opt.MapFrom(src => src.GivenName))
                    .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
                    .ForMember(dest => dest.BirthName, opt => opt.MapFrom(src => src.BirthName))
                    .ForMember(dest => dest.AboutMe, opt => opt.MapFrom(src => src.AboutMe))
                    .ForMember(dest => dest.GenderId, opt => opt.MapFrom(src => src.GenderId))
                    .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
                    .ForMember(dest => dest.Birthplace, opt => opt.MapFrom(src => src.Birthplace))
                    .ForMember(dest => dest.ContactViaId, opt => opt.MapFrom(src => src.ContactViaId))
                    .ForMember(dest => dest.ExperienceLevel, opt => opt.MapFrom(src => src.ExperienceLevel))
                    .ForMember(dest => dest.Reliability, opt => opt.MapFrom(src => src.Reliability))
                    .ForMember(dest => dest.GeneralPreference, opt => opt.MapFrom(src => src.GeneralPreference))
                    .ForAllOtherMembers(opt => opt.Ignore());
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.Id)
                    .EntityExists<Command, Person>(arpaContext);
                RuleFor(c => c.GenderId)
                    .SelectValueMapping<Command, Person>(arpaContext, p => p.Gender);
                RuleFor(c => c.ContactViaId)
                    .EntityExists<Command, Person>(arpaContext)
                    .Must((command, contactViaId, _) => !command.Id.Equals(contactViaId.Value))
                    .When(command => command.ContactViaId.HasValue)
                    .WithMessage("Person cannot to be self-referenced");
            }
        }
    }
}

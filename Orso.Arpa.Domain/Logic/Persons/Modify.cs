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
            public string AboutMe { get; set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Command, Person>()
                    .ForMember(dest => dest.GivenName, opt => opt.MapFrom(src => src.GivenName))
                    .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
                    .ForMember(dest => dest.AboutMe, opt => opt.MapFrom(src => src.AboutMe))
                    .ForAllOtherMembers(opt => opt.Ignore());
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.Id)
                    .EntityExists<Command, Person>(arpaContext, nameof(Command.Id));
            }
        }
    }
}

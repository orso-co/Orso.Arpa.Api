using System;
using FluentValidation;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;
using static Orso.Arpa.Domain.GenericHandlers.Create;

namespace Orso.Arpa.Domain.Logic.Persons
{
    public static class Create
    {
        public class Command : ICreateCommand<Person>
        {
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

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.GenderId)
                    .SelectValueMapping<Command, Person>(arpaContext, p => p.Gender);

                RuleFor(c => c.ContactViaId)
                    .EntityExists<Command, Person>(arpaContext);
            }
        }
    }
}

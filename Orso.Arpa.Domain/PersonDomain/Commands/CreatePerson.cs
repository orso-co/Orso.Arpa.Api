using System;
using FluentValidation;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.PersonDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Create;

namespace Orso.Arpa.Domain.PersonDomain.Commands
{
    public static class CreatePerson
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
            public string PersonBackgroundTeam { get; set; }
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

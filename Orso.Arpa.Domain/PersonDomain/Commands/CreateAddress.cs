using System;
using FluentValidation;
using Orso.Arpa.Domain.AddressDomain.Commands;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.PersonDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Create;

namespace Orso.Arpa.Domain.PersonDomain.Commands
{
    public static class CreateAddress
    {
        public class Command : AddressCommand, ICreateCommand<PersonAddress>
        {
            public Guid PersonId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.PersonId)
                    .EntityExists<Command, Person>(arpaContext);
            }
        }
    }
}

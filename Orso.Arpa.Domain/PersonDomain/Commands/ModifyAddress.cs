using System;
using FluentValidation;
using Orso.Arpa.Domain.AddressDomain.Commands;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.PersonDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Modify;

namespace Orso.Arpa.Domain.PersonDomain.Commands
{
    public static class ModifyAddress
    {
        public class Command : AddressCommand, IModifyCommand<PersonAddress>
        {
            public Guid PersonId { get; set; }
            public Guid Id { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.Id)
                    .MustAsync(async (cmd, id, cancellation) => await arpaContext
                        .EntityExistsAsync<PersonAddress>(address => address.Id == id && address.PersonId == cmd.PersonId, cancellation))
                    .WithMessage("Address could not be found")
                    .WithErrorCode("404");

                RuleFor(c => c.PersonId)
                    .EntityExists<Command, Person>(arpaContext);
            }
        }
    }
}

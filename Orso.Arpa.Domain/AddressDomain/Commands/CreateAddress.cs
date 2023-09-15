using System;
using FluentValidation;
using Orso.Arpa.Domain.AddressDomain.Interfaces;
using Orso.Arpa.Domain.AddressDomain.Model;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.PersonDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Create;

namespace Orso.Arpa.Domain.AddressDomain.Commands
{
    public static class CreateAddress
    {
        public class Command : BaseAddressCreateCommand, ICreateCommand<Address>
        {
            public Guid PersonId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.PersonId)
                    .EntityExists<Command, Person>(arpaContext);

                RuleFor(c => c.TypeId)
                    .SelectValueMapping<Command, Address>(arpaContext, c => c.Type);
            }
        }
    }
}

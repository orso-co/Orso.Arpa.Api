using System;
using FluentValidation;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;
using static Orso.Arpa.Domain.GenericHandlers.Create;

namespace Orso.Arpa.Domain.Logic.Addresses
{
    public static class Create
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

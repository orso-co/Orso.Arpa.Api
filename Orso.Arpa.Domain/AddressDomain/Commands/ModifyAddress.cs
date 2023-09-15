using System;
using FluentValidation;
using Orso.Arpa.Domain.AddressDomain.Model;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.PersonDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Modify;

namespace Orso.Arpa.Domain.AddressDomain.Commands
{
    public static class ModifyAddress
    {
        public class Command : IModifyCommand<Address>
        {
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string Zip { get; set; }
            public string City { get; set; }
            public string UrbanDistrict { get; set; }
            public string Country { get; set; }
            public string State { get; set; }
            public string CommentInner { get; set; }
            public Guid? TypeId { get; set; }
            public Guid PersonId { get; set; }
            public Guid Id { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.Id)
                    .MustAsync(async (cmd, id, cancellation) => await arpaContext
                        .EntityExistsAsync<Address>(address => address.Id == id && address.PersonId == cmd.PersonId, cancellation))
                    .WithMessage("Address could not be found")
                    .WithErrorCode("404");

                RuleFor(c => c.PersonId)
                    .EntityExists<Command, Person>(arpaContext);

                RuleFor(c => c.TypeId)
                    .SelectValueMapping<Command, Address>(arpaContext, c => c.Type);
            }
        }
    }
}

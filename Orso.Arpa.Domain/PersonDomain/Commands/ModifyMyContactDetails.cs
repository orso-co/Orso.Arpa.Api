using System;
using FluentValidation;
using Orso.Arpa.Domain.PersonDomain.Enums;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.PersonDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Modify;

namespace Orso.Arpa.Domain.PersonDomain.Commands
{
    public static class ModifyMyContactDetails
    {
        public class Command : IModifyCommand<ContactDetail>
        {
            public ContactDetailKey Key { get; set; }
            public string Value { get; set; }
            public Guid? TypeId { get; set; }
            public string CommentInner { get; set; }
            public byte Preference { get; set; }
            public Guid Id { get; set; }
            public Guid PersonId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.Id)
                    .MustAsync(async (command, id, cancellation) => await arpaContext
                        .EntityExistsAsync<ContactDetail>(cd => cd.Id == id && cd.PersonId == command.PersonId, cancellation))
                    .WithMessage("Contact Detail could not be found")
                    .WithErrorCode("404");

                RuleFor(c => c.TypeId)
                    .SelectValueMapping<Command, ContactDetail>(arpaContext, c => c.Type);
            }
        }
    }
}

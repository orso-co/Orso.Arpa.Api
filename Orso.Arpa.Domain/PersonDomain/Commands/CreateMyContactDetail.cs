using System;
using FluentValidation;
using Orso.Arpa.Domain.PersonDomain.Enums;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.PersonDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Create;

namespace Orso.Arpa.Domain.PersonDomain.Commands
{
    public static class CreateMyContactDetail
    {
        public class Command : ICreateCommand<ContactDetail>
        {
            public Guid PersonId { get; set; }
            public ContactDetailKey Key { get; set; }
            public string Value { get; set; }
            public Guid? TypeId { get; set; }
            public string CommentInner { get; set; }
            public byte Preference { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.PersonId)
                    .EntityExists<Command, Person>(arpaContext);

                RuleFor(c => c.TypeId)
                    .SelectValueMapping<Command, ContactDetail>(arpaContext, c => c.Type);
            }
        }
    }
}

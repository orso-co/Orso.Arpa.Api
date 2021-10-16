using System;
using FluentValidation;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;
using static Orso.Arpa.Domain.GenericHandlers.Create;

namespace Orso.Arpa.Domain.Logic.MyContactDetails
{
    public static class Create
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

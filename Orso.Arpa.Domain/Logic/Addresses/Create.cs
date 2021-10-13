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
        public class Command : ICreateCommand<Address>, IAddressCreateCommand
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

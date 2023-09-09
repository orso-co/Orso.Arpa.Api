using System;
using FluentValidation;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.VenueDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Modify;

namespace Orso.Arpa.Domain.VenueDomain.Commands
{
    public static class ModifyVenue
    {
        public class Command : IModifyCommand<Venue>
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string Zip { get; set; }
            public string City { get; set; }
            public string UrbanDistrict { get; set; }
            public string Country { get; set; }
            public string State { get; set; }
            public string CommentInner { get; set; }
            public Guid Id { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.Id)
                    .EntityExists<Command, Venue>(arpaContext);
            }
        }
    }
}

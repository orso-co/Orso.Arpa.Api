using System;
using FluentValidation;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.OrganizationDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Modify;

namespace Orso.Arpa.Domain.OrganizationDomain.Commands
{
    public static class ModifyPersonOrganization
    {
        public class Command : IModifyCommand<PersonOrganization>
        {
            public Guid Id { get; set; }
            public string Function { get; set; }
            public Guid? RelationshipTypeId { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.Id)
                    .EntityExists<Command, PersonOrganization>(arpaContext);

                RuleFor(c => c.RelationshipTypeId)
                    .SelectValueMapping<Command, PersonOrganization>(arpaContext, po => po.RelationshipType)
                    .When(c => c.RelationshipTypeId.HasValue);
            }
        }
    }
}

using System;
using FluentValidation;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.OrganizationDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Modify;

namespace Orso.Arpa.Domain.OrganizationDomain.Commands
{
    public static class ModifyOrganizationRelationship
    {
        public class Command : IModifyCommand<OrganizationRelationship>
        {
            public Guid Id { get; set; }
            public Guid? RelationshipTypeId { get; set; }
            public string Description { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.Id)
                    .EntityExists<Command, OrganizationRelationship>(arpaContext);

                RuleFor(c => c.RelationshipTypeId)
                    .SelectValueMapping<Command, OrganizationRelationship>(arpaContext, r => r.RelationshipType)
                    .When(c => c.RelationshipTypeId.HasValue);
            }
        }
    }
}

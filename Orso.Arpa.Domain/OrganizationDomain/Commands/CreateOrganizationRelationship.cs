using System;
using FluentValidation;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.OrganizationDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Create;

namespace Orso.Arpa.Domain.OrganizationDomain.Commands
{
    public static class CreateOrganizationRelationship
    {
        public class Command : ICreateCommand<OrganizationRelationship>
        {
            public Guid SourceOrganizationId { get; set; }
            public Guid TargetOrganizationId { get; set; }
            public Guid? RelationshipTypeId { get; set; }
            public string Description { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.SourceOrganizationId)
                    .EntityExists<Command, Organization>(arpaContext);

                RuleFor(c => c.TargetOrganizationId)
                    .EntityExists<Command, Organization>(arpaContext)
                    .Must((command, targetId, _) => !command.SourceOrganizationId.Equals(targetId))
                    .WithMessage("An organization cannot have a relationship with itself");

                RuleFor(c => c.RelationshipTypeId)
                    .SelectValueMapping<Command, OrganizationRelationship>(arpaContext, r => r.RelationshipType)
                    .When(c => c.RelationshipTypeId.HasValue);
            }
        }
    }
}

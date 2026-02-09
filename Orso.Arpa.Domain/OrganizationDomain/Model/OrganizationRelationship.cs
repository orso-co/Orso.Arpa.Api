using System;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.OrganizationDomain.Commands;
using Orso.Arpa.Domain.SelectValueDomain.Model;

namespace Orso.Arpa.Domain.OrganizationDomain.Model
{
    public class OrganizationRelationship : BaseEntity
    {
        public OrganizationRelationship(Guid? id, CreateOrganizationRelationship.Command command) : base(id)
        {
            SourceOrganizationId = command.SourceOrganizationId;
            TargetOrganizationId = command.TargetOrganizationId;
            RelationshipTypeId = command.RelationshipTypeId;
            Description = command.Description;
            StartDate = command.StartDate;
            EndDate = command.EndDate;
        }

        public OrganizationRelationship()
        {
        }

        public void Update(ModifyOrganizationRelationship.Command command)
        {
            RelationshipTypeId = command.RelationshipTypeId;
            Description = command.Description;
            StartDate = command.StartDate;
            EndDate = command.EndDate;
        }

        public Guid SourceOrganizationId { get; private set; }
        public virtual Organization SourceOrganization { get; private set; }

        public Guid TargetOrganizationId { get; private set; }
        public virtual Organization TargetOrganization { get; private set; }

        public Guid? RelationshipTypeId { get; private set; }
        public virtual SelectValueMapping RelationshipType { get; private set; }

        [JsonInclude]
        public string Description { get; private set; }

        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
    }
}

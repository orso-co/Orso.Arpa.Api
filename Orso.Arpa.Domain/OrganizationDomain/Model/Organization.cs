using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.General.Attributes;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.OrganizationDomain.Commands;
using Orso.Arpa.Domain.SelectValueDomain.Model;

namespace Orso.Arpa.Domain.OrganizationDomain.Model
{
    public class Organization : BaseEntity
    {
        public Organization(Guid? id, CreateOrganization.Command command) : base(id)
        {
            Name = command.Name;
            ShortName = command.ShortName;
            Description = command.Description;
            Website = command.Website;
            Email = command.Email;
            Phone = command.Phone;
            LegalFormId = command.LegalFormId;
            OrganizationTypeId = command.OrganizationTypeId;
            Tags = command.Tags;
        }

        public Organization()
        {
        }

        public void Update(ModifyOrganization.Command command)
        {
            Name = command.Name;
            ShortName = command.ShortName;
            Description = command.Description;
            Website = command.Website;
            Email = command.Email;
            Phone = command.Phone;
            LegalFormId = command.LegalFormId;
            OrganizationTypeId = command.OrganizationTypeId;
            Tags = command.Tags;
        }

        [JsonInclude]
        public string Name { get; private set; }

        [JsonInclude]
        public string ShortName { get; private set; }

        [JsonInclude]
        public string Description { get; private set; }

        [JsonInclude]
        public string Website { get; private set; }

        [JsonInclude]
        public string Email { get; private set; }

        [JsonInclude]
        public string Phone { get; private set; }

        public Guid? LegalFormId { get; private set; }
        public virtual SelectValueMapping LegalForm { get; private set; }

        public Guid? OrganizationTypeId { get; private set; }
        public virtual SelectValueMapping OrganizationType { get; private set; }

        [JsonInclude]
        public string Tags { get; private set; }

        [CascadingSoftDelete]
        [JsonInclude]
        public virtual ICollection<PersonOrganization> PersonOrganizations { get; private set; } = new HashSet<PersonOrganization>();

        [CascadingSoftDelete]
        public virtual ICollection<OrganizationRelationship> RelationshipsAsSource { get; private set; } = new HashSet<OrganizationRelationship>();

        [CascadingSoftDelete]
        public virtual ICollection<OrganizationRelationship> RelationshipsAsTarget { get; private set; } = new HashSet<OrganizationRelationship>();

        public string DisplayName => !string.IsNullOrEmpty(ShortName) ? $"{Name} ({ShortName})" : Name;

        public override string ToString()
        {
            return DisplayName;
        }
    }
}

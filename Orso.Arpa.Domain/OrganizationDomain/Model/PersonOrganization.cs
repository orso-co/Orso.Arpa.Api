using System;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.OrganizationDomain.Commands;
using Orso.Arpa.Domain.PersonDomain.Model;
using Orso.Arpa.Domain.SelectValueDomain.Model;

namespace Orso.Arpa.Domain.OrganizationDomain.Model
{
    public class PersonOrganization : BaseEntity
    {
        public PersonOrganization(Guid? id, CreatePersonOrganization.Command command) : base(id)
        {
            PersonId = command.PersonId;
            OrganizationId = command.OrganizationId;
            Function = command.Function;
            RelationshipTypeId = command.RelationshipTypeId;
            StartDate = command.StartDate;
            EndDate = command.EndDate;
        }

        public PersonOrganization()
        {
        }

        public void Update(ModifyPersonOrganization.Command command)
        {
            Function = command.Function;
            RelationshipTypeId = command.RelationshipTypeId;
            StartDate = command.StartDate;
            EndDate = command.EndDate;
        }

        public Guid PersonId { get; private set; }
        public virtual Person Person { get; private set; }

        public Guid OrganizationId { get; private set; }
        public virtual Organization Organization { get; private set; }

        [JsonInclude]
        public string Function { get; private set; }

        public Guid? RelationshipTypeId { get; private set; }
        public virtual SelectValueMapping RelationshipType { get; private set; }

        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
    }
}

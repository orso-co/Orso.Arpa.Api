using System;
using FluentValidation;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.OrganizationDomain.Model;
using Orso.Arpa.Domain.PersonDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Create;

namespace Orso.Arpa.Domain.OrganizationDomain.Commands
{
    public static class CreatePersonOrganization
    {
        public class Command : ICreateCommand<PersonOrganization>
        {
            public Guid PersonId { get; set; }
            public Guid OrganizationId { get; set; }
            public string Function { get; set; }
            public Guid? RelationshipTypeId { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.PersonId)
                    .EntityExists<Command, Person>(arpaContext);

                RuleFor(c => c.OrganizationId)
                    .EntityExists<Command, Organization>(arpaContext);

                RuleFor(c => c.RelationshipTypeId)
                    .SelectValueMapping<Command, PersonOrganization>(arpaContext, po => po.RelationshipType)
                    .When(c => c.RelationshipTypeId.HasValue);
            }
        }
    }
}

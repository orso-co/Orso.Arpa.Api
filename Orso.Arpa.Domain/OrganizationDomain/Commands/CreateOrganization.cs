using System;
using FluentValidation;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.OrganizationDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Create;

namespace Orso.Arpa.Domain.OrganizationDomain.Commands
{
    public static class CreateOrganization
    {
        public class Command : ICreateCommand<Organization>
        {
            public string Name { get; set; }
            public string ShortName { get; set; }
            public string Description { get; set; }
            public string Website { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public Guid? LegalFormId { get; set; }
            public Guid? OrganizationTypeId { get; set; }
            public string Tags { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.LegalFormId)
                    .SelectValueMapping<Command, Organization>(arpaContext, o => o.LegalForm)
                    .When(c => c.LegalFormId.HasValue);

                RuleFor(c => c.OrganizationTypeId)
                    .SelectValueMapping<Command, Organization>(arpaContext, o => o.OrganizationType)
                    .When(c => c.OrganizationTypeId.HasValue);
            }
        }
    }
}

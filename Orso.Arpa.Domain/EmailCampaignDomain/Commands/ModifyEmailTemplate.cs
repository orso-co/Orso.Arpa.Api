using System;
using FluentValidation;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.EmailCampaignDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Modify;

namespace Orso.Arpa.Domain.EmailCampaignDomain.Commands;

public static class ModifyEmailTemplate
{
    public class Command : IModifyCommand<EmailTemplate>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProjectDataJson { get; set; }
        public string MjmlSource { get; set; }
        public string CompiledHtml { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator(IArpaContext arpaContext)
        {
            _ = RuleFor(d => d.Id)
                .EntityExists<Command, EmailTemplate>(arpaContext);
        }
    }
}

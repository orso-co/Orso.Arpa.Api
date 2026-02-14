using System;
using FluentValidation;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.EmailCampaignDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Modify;

namespace Orso.Arpa.Domain.EmailCampaignDomain.Commands;

public static class ModifyEmailCampaign
{
    public class Command : IModifyCommand<EmailCampaign>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public Guid EmailTemplateId { get; set; }
        public string PersonalizedHtml { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator(IArpaContext arpaContext)
        {
            _ = RuleFor(d => d.Id)
                .EntityExists<Command, EmailCampaign>(arpaContext);

            _ = RuleFor(d => d.EmailTemplateId)
                .EntityExists<Command, EmailTemplate>(arpaContext);
        }
    }
}

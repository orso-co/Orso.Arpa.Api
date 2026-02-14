using System;
using Orso.Arpa.Domain.EmailCampaignDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Create;

namespace Orso.Arpa.Domain.EmailCampaignDomain.Commands;

public static class CreateEmailCampaign
{
    public class Command : ICreateCommand<EmailCampaign>
    {
        public string Name { get; set; }
        public string Subject { get; set; }
        public Guid EmailTemplateId { get; set; }
    }
}

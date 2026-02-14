using Orso.Arpa.Domain.EmailCampaignDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Create;

namespace Orso.Arpa.Domain.EmailCampaignDomain.Commands;

public static class CreateEmailTemplate
{
    public class Command : ICreateCommand<EmailTemplate>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProjectDataJson { get; set; }
        public string MjmlSource { get; set; }
        public string CompiledHtml { get; set; }
    }
}

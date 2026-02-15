namespace Orso.Arpa.Domain.EmailCampaignDomain.Services;

public interface IMjmlCompilationService
{
    string CompileToHtml(string mjmlSource);
}

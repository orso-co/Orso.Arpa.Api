using Mjml.Net;
using Orso.Arpa.Domain.EmailCampaignDomain.Services;

namespace Orso.Arpa.Infrastructure.EmailCampaign;

public class MjmlCompilationService : IMjmlCompilationService
{
    private readonly MjmlRenderer _renderer = new();

    public string CompileToHtml(string mjmlSource)
    {
        var result = _renderer.Render(mjmlSource);
        return result.Html;
    }
}

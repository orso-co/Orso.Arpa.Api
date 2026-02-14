using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.General.Services;
using Orso.Arpa.Application.EmailCampaignApplication.Interfaces;
using Orso.Arpa.Application.EmailCampaignApplication.Model;
using Orso.Arpa.Domain.EmailCampaignDomain.Commands;
using Orso.Arpa.Domain.EmailCampaignDomain.Model;

namespace Orso.Arpa.Application.EmailCampaignApplication.Services;

public class EmailTemplateService :
    BaseService<
        EmailTemplateDto,
        EmailTemplate,
        EmailTemplateCreateDto,
        CreateEmailTemplate.Command,
        EmailTemplateModifyDto,
        EmailTemplateModifyBodyDto,
        ModifyEmailTemplate.Command>, IEmailTemplateService
{
    public EmailTemplateService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    public async Task<IEnumerable<EmailTemplateDto>> GetAsync()
    {
        return await base.GetAsync(
            orderBy: q => q.OrderByDescending(t => t.CreatedAt));
    }
}

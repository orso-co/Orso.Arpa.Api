using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Application.General.Services;
using Orso.Arpa.Application.EmailCampaignApplication.Interfaces;
using Orso.Arpa.Application.EmailCampaignApplication.Model;
using Orso.Arpa.Domain.EmailCampaignDomain.Commands;
using Orso.Arpa.Domain.EmailCampaignDomain.Model;
using Orso.Arpa.Domain.EmailCampaignDomain.Queries;
using Orso.Arpa.Domain.General.Interfaces;

namespace Orso.Arpa.Application.EmailCampaignApplication.Services;

public class EmailCampaignService :
    BaseService<
        EmailCampaignDto,
        EmailCampaign,
        EmailCampaignCreateDto,
        CreateEmailCampaign.Command,
        EmailCampaignModifyDto,
        EmailCampaignModifyBodyDto,
        ModifyEmailCampaign.Command>, IEmailCampaignService
{
    private readonly IArpaContext _arpaContext;

    public EmailCampaignService(IMediator mediator, IMapper mapper, IArpaContext arpaContext) : base(mediator, mapper)
    {
        _arpaContext = arpaContext;
    }

    public override async Task<EmailCampaignDto> GetByIdAsync(Guid id)
    {
        EmailCampaign entity = await _arpaContext.Set<EmailCampaign>()
            .Include(c => c.Recipients)
            .Include(c => c.Attachments)
            .Include(c => c.EmailTemplate)
            .FirstOrDefaultAsync(c => c.Id == id)
            ?? throw new Domain.General.Errors.NotFoundException(nameof(EmailCampaign), nameof(id));

        return _mapper.Map<EmailCampaignDto>(entity);
    }

    public async Task<IEnumerable<EmailCampaignListDto>> GetAsync()
    {
        List<EmailCampaign> campaigns = await _arpaContext.Set<EmailCampaign>()
            .Where(c => !c.Deleted)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();

        return _mapper.Map<IEnumerable<EmailCampaignListDto>>(campaigns);
    }

    public async Task<int> AddRecipientsAsync(Guid campaignId, IList<Guid> personIds)
    {
        return await _mediator.Send(new AddCampaignRecipients.Command
        {
            CampaignId = campaignId,
            PersonIds = personIds
        });
    }

    public async Task<int> RemoveRecipientsAsync(Guid campaignId, IList<Guid> personIds)
    {
        return await _mediator.Send(new RemoveCampaignRecipients.Command
        {
            CampaignId = campaignId,
            PersonIds = personIds
        });
    }

    public async Task SendAsync(Guid id)
    {
        await _mediator.Send(new SendEmailCampaign.Command { Id = id });
    }

    public async Task ScheduleAsync(Guid id, DateTime scheduledAt)
    {
        await _mediator.Send(new ScheduleEmailCampaign.Command { Id = id, ScheduledAt = scheduledAt });
    }

    public async Task CancelAsync(Guid id)
    {
        await _mediator.Send(new CancelEmailCampaign.Command { Id = id });
    }

    public async Task<CampaignAnalyticsDto> GetAnalyticsAsync(Guid id)
    {
        GetCampaignAnalytics.Result result = await _mediator.Send(new GetCampaignAnalytics.Query { CampaignId = id });
        return new CampaignAnalyticsDto
        {
            TotalRecipients = result.TotalRecipients,
            SentCount = result.SentCount,
            FailedCount = result.FailedCount,
            OpenedCount = result.OpenedCount,
            PendingCount = result.PendingCount,
            OpenRate = result.OpenRate
        };
    }

    public async Task SendTestAsync(Guid campaignId, string emailAddress)
    {
        await _mediator.Send(new SendTestEmail.Command
        {
            CampaignId = campaignId,
            EmailAddress = emailAddress
        });
    }

    public async Task<EmailCampaignDto> DuplicateAsync(Guid id, bool includeRecipients)
    {
        Guid newCampaignId = await _mediator.Send(new DuplicateEmailCampaign.Command
        {
            CampaignId = id,
            IncludeRecipients = includeRecipients
        });
        return await GetByIdAsync(newCampaignId);
    }
}

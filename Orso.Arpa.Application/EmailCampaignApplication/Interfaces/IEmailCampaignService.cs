using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.EmailCampaignApplication.Model;

namespace Orso.Arpa.Application.EmailCampaignApplication.Interfaces;

public interface IEmailCampaignService
{
    Task<EmailCampaignDto> CreateAsync(EmailCampaignCreateDto createDto);
    Task<IEnumerable<EmailCampaignListDto>> GetAsync();
    Task<EmailCampaignDto> GetByIdAsync(Guid id);
    Task ModifyAsync(EmailCampaignModifyDto modifyDto);
    Task DeleteAsync(Guid id);
    Task<int> AddRecipientsAsync(Guid campaignId, IList<Guid> personIds);
    Task<int> RemoveRecipientsAsync(Guid campaignId, IList<Guid> personIds);
    Task SendAsync(Guid id);
    Task ScheduleAsync(Guid id, DateTime scheduledAt);
    Task CancelAsync(Guid id);
    Task<CampaignAnalyticsDto> GetAnalyticsAsync(Guid id);
    Task SendTestAsync(Guid campaignId, string emailAddress);
    Task<EmailCampaignDto> DuplicateAsync(Guid id, bool includeRecipients);
}

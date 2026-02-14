using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.EmailCampaignDomain.Commands;
using Orso.Arpa.Domain.EmailCampaignDomain.Enums;

namespace Orso.Arpa.Domain.EmailCampaignDomain.Model;

public class EmailCampaign : BaseEntity
{
    public EmailCampaign(Guid? id, CreateEmailCampaign.Command command) : base(id)
    {
        Name = command.Name;
        Subject = command.Subject;
        EmailTemplateId = command.EmailTemplateId;
        Status = CampaignStatus.Draft;
    }

    protected EmailCampaign() { }

    public string Name { get; private set; }
    public string Subject { get; private set; }
    public Guid EmailTemplateId { get; private set; }
    public virtual EmailTemplate EmailTemplate { get; private set; }
    public string PersonalizedHtml { get; private set; }
    public CampaignStatus Status { get; private set; }
    public DateTime? ScheduledAt { get; private set; }
    public DateTime? SentAt { get; private set; }
    public int TotalRecipients { get; private set; }
    public int SentCount { get; private set; }
    public int FailedCount { get; private set; }
    public int OpenedCount { get; private set; }

    public virtual ICollection<EmailCampaignRecipient> Recipients { get; private set; } = new List<EmailCampaignRecipient>();
    public virtual ICollection<EmailCampaignAttachment> Attachments { get; private set; } = new List<EmailCampaignAttachment>();

    public void Update(ModifyEmailCampaign.Command command)
    {
        Name = command.Name;
        Subject = command.Subject;
        EmailTemplateId = command.EmailTemplateId;
        PersonalizedHtml = command.PersonalizedHtml;
    }

    public void Schedule(DateTime scheduledAt)
    {
        ScheduledAt = scheduledAt;
        Status = CampaignStatus.Scheduled;
    }

    public void StartSending()
    {
        Status = CampaignStatus.Sending;
    }

    public void MarkAsSent()
    {
        Status = CampaignStatus.Sent;
        SentAt = DateTime.UtcNow;
    }

    public void MarkAsFailed()
    {
        Status = CampaignStatus.Failed;
    }

    public void Cancel()
    {
        Status = CampaignStatus.Cancelled;
    }

    public void SetTotalRecipients(int count)
    {
        TotalRecipients = count;
    }

    public void IncrementSentCount()
    {
        SentCount++;
    }

    public void IncrementFailedCount()
    {
        FailedCount++;
    }

    public void IncrementOpenedCount()
    {
        OpenedCount++;
    }
}

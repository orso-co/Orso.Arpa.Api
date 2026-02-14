using System;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.EmailCampaignDomain.Enums;
using Orso.Arpa.Domain.PersonDomain.Model;

namespace Orso.Arpa.Domain.EmailCampaignDomain.Model;

public class EmailCampaignRecipient : BaseEntity
{
    public EmailCampaignRecipient(Guid? id, Guid emailCampaignId, Guid personId, string emailAddress, string displayName) : base(id)
    {
        EmailCampaignId = emailCampaignId;
        PersonId = personId;
        EmailAddress = emailAddress;
        DisplayName = displayName;
        Status = RecipientStatus.Pending;
        TrackingToken = Guid.NewGuid();
    }

    protected EmailCampaignRecipient() { }

    public Guid EmailCampaignId { get; private set; }
    public virtual EmailCampaign EmailCampaign { get; private set; }
    public Guid PersonId { get; private set; }
    public virtual Person Person { get; private set; }
    public string EmailAddress { get; private set; }
    public string DisplayName { get; private set; }
    public RecipientStatus Status { get; private set; }
    public DateTime? SentAt { get; private set; }
    public DateTime? OpenedAt { get; private set; }
    public Guid TrackingToken { get; private set; }
    public string ErrorMessage { get; private set; }

    public void MarkAsSent()
    {
        Status = RecipientStatus.Sent;
        SentAt = DateTime.UtcNow;
    }

    public void MarkAsFailed(string errorMessage)
    {
        Status = RecipientStatus.Failed;
        ErrorMessage = errorMessage;
    }

    public void MarkAsOpened()
    {
        if (OpenedAt == null)
        {
            OpenedAt = DateTime.UtcNow;
        }
    }
}

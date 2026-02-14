using System;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.PersonDomain.Model;

namespace Orso.Arpa.Domain.EmailCampaignDomain.Model;

public class EmailUnsubscription : BaseEntity
{
    public EmailUnsubscription(Guid? id, Guid personId, string emailAddress, string reason = null) : base(id)
    {
        PersonId = personId;
        EmailAddress = emailAddress;
        UnsubscribedAt = DateTime.UtcNow;
        Reason = reason;
    }

    protected EmailUnsubscription() { }

    public Guid PersonId { get; private set; }
    public virtual Person Person { get; private set; }
    public string EmailAddress { get; private set; }
    public DateTime UnsubscribedAt { get; private set; }
    public string Reason { get; private set; }
}

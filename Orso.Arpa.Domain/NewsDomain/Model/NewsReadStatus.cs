using System;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Domain.NewsDomain.Model;

public class NewsReadStatus : BaseEntity
{
    public NewsReadStatus(Guid? id, Guid newsId, Guid userId) : base(id)
    {
        NewsId = newsId;
        UserId = userId;
        ReadAt = DateTime.UtcNow;
    }

    [JsonConstructor]
    protected NewsReadStatus()
    {
    }

    public Guid NewsId { get; private set; }
    public virtual News News { get; private set; }

    public Guid UserId { get; private set; }
    public virtual User User { get; private set; }

    public DateTime ReadAt { get; private set; }
}

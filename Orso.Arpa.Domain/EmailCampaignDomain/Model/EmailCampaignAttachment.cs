using System;
using Orso.Arpa.Domain.General.Model;

namespace Orso.Arpa.Domain.EmailCampaignDomain.Model;

public class EmailCampaignAttachment : BaseEntity
{
    public EmailCampaignAttachment(Guid? id, Guid emailCampaignId, string fileName, string contentType, string storagePath, long fileSize) : base(id)
    {
        EmailCampaignId = emailCampaignId;
        FileName = fileName;
        ContentType = contentType;
        StoragePath = storagePath;
        FileSize = fileSize;
    }

    protected EmailCampaignAttachment() { }

    public Guid EmailCampaignId { get; private set; }
    public virtual EmailCampaign EmailCampaign { get; private set; }
    public string FileName { get; private set; }
    public string ContentType { get; private set; }
    public string StoragePath { get; private set; }
    public long FileSize { get; private set; }
}

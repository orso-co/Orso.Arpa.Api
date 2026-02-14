using System;
using System.Collections.Generic;
using AutoMapper;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.EmailCampaignDomain.Enums;
using Orso.Arpa.Domain.EmailCampaignDomain.Model;

namespace Orso.Arpa.Application.EmailCampaignApplication.Model;

public class EmailCampaignDto : BaseEntityDto
{
    public string Name { get; set; }
    public string Subject { get; set; }
    public Guid EmailTemplateId { get; set; }
    public string EmailTemplateName { get; set; }
    public string PersonalizedHtml { get; set; }
    public CampaignStatus Status { get; set; }
    public DateTime? ScheduledAt { get; set; }
    public DateTime? SentAt { get; set; }
    public int TotalRecipients { get; set; }
    public int SentCount { get; set; }
    public int FailedCount { get; set; }
    public int OpenedCount { get; set; }
    public IList<EmailCampaignRecipientDto> Recipients { get; set; }
    public IList<EmailCampaignAttachmentDto> Attachments { get; set; }
}

public class EmailCampaignRecipientDto
{
    public Guid Id { get; set; }
    public Guid PersonId { get; set; }
    public string EmailAddress { get; set; }
    public string DisplayName { get; set; }
    public RecipientStatus Status { get; set; }
    public DateTime? SentAt { get; set; }
    public DateTime? OpenedAt { get; set; }
    public string ErrorMessage { get; set; }
}

public class EmailCampaignAttachmentDto
{
    public Guid Id { get; set; }
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public long FileSize { get; set; }
}

public class EmailCampaignDtoMappingProfile : Profile
{
    public EmailCampaignDtoMappingProfile()
    {
        CreateMap<EmailCampaign, EmailCampaignDto>()
            .IncludeBase<BaseEntity, BaseEntityDto>()
            .ForMember(dest => dest.EmailTemplateName, opt => opt.MapFrom(src => src.EmailTemplate.Name));

        CreateMap<EmailCampaignRecipient, EmailCampaignRecipientDto>();
        CreateMap<EmailCampaignAttachment, EmailCampaignAttachmentDto>();
    }
}

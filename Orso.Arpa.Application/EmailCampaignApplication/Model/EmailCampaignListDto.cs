using System;
using AutoMapper;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.EmailCampaignDomain.Enums;
using Orso.Arpa.Domain.EmailCampaignDomain.Model;

namespace Orso.Arpa.Application.EmailCampaignApplication.Model;

public class EmailCampaignListDto : BaseEntityDto
{
    public string Name { get; set; }
    public string Subject { get; set; }
    public string EmailTemplateName { get; set; }
    public CampaignStatus Status { get; set; }
    public DateTime? ScheduledAt { get; set; }
    public DateTime? SentAt { get; set; }
    public int TotalRecipients { get; set; }
    public int SentCount { get; set; }
    public int OpenedCount { get; set; }
}

public class EmailCampaignListDtoMappingProfile : Profile
{
    public EmailCampaignListDtoMappingProfile()
    {
        CreateMap<EmailCampaign, EmailCampaignListDto>()
            .IncludeBase<BaseEntity, BaseEntityDto>()
            .ForMember(dest => dest.EmailTemplateName, opt => opt.MapFrom(src => src.EmailTemplate.Name));
    }
}

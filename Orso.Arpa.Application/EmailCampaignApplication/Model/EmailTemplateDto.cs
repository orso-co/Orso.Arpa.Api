using AutoMapper;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.EmailCampaignDomain.Model;

namespace Orso.Arpa.Application.EmailCampaignApplication.Model;

public class EmailTemplateDto : BaseEntityDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ProjectDataJson { get; set; }
    public string MjmlSource { get; set; }
    public string CompiledHtml { get; set; }
    public string ThumbnailPath { get; set; }
}

public class EmailTemplateDtoMappingProfile : Profile
{
    public EmailTemplateDtoMappingProfile()
    {
        CreateMap<EmailTemplate, EmailTemplateDto>()
            .IncludeBase<BaseEntity, BaseEntityDto>();
    }
}

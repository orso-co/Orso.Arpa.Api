using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General.Extensions;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.EmailCampaignDomain.Commands;

namespace Orso.Arpa.Application.EmailCampaignApplication.Model;

public class EmailTemplateModifyDto : IdFromRouteDto<EmailTemplateModifyBodyDto>
{
}

public class EmailTemplateModifyBodyDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ProjectDataJson { get; set; }
    public string MjmlSource { get; set; }
    public string CompiledHtml { get; set; }
}

public class EmailTemplateModifyDtoMappingProfile : Profile
{
    public EmailTemplateModifyDtoMappingProfile()
    {
        _ = CreateMap<EmailTemplateModifyDto, ModifyEmailTemplate.Command>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Body.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Body.Description))
            .ForMember(dest => dest.ProjectDataJson, opt => opt.MapFrom(src => src.Body.ProjectDataJson))
            .ForMember(dest => dest.MjmlSource, opt => opt.MapFrom(src => src.Body.MjmlSource))
            .ForMember(dest => dest.CompiledHtml, opt => opt.MapFrom(src => src.Body.CompiledHtml))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
    }
}

public class EmailTemplateModifyDtoValidator : IdFromRouteDtoValidator<EmailTemplateModifyDto, EmailTemplateModifyBodyDto>
{
    public EmailTemplateModifyDtoValidator()
    {
        _ = RuleFor(d => d.Body)
            .SetValidator(new EmailTemplateModifyBodyDtoValidator());
    }
}

public class EmailTemplateModifyBodyDtoValidator : AbstractValidator<EmailTemplateModifyBodyDto>
{
    public EmailTemplateModifyBodyDtoValidator()
    {
        _ = RuleFor(c => c.Name)
            .NotEmpty()
            .FreeText(200);

        _ = RuleFor(c => c.Description)
            .FreeText(500);
    }
}

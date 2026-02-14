using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General.Extensions;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.EmailCampaignDomain.Commands;

namespace Orso.Arpa.Application.EmailCampaignApplication.Model;

public class EmailCampaignModifyDto : IdFromRouteDto<EmailCampaignModifyBodyDto>
{
}

public class EmailCampaignModifyBodyDto
{
    public string Name { get; set; }
    public string Subject { get; set; }
    public Guid EmailTemplateId { get; set; }
    public string PersonalizedHtml { get; set; }
}

public class EmailCampaignModifyDtoMappingProfile : Profile
{
    public EmailCampaignModifyDtoMappingProfile()
    {
        _ = CreateMap<EmailCampaignModifyDto, ModifyEmailCampaign.Command>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Body.Name))
            .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.Body.Subject))
            .ForMember(dest => dest.EmailTemplateId, opt => opt.MapFrom(src => src.Body.EmailTemplateId))
            .ForMember(dest => dest.PersonalizedHtml, opt => opt.MapFrom(src => src.Body.PersonalizedHtml))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
    }
}

public class EmailCampaignModifyDtoValidator : IdFromRouteDtoValidator<EmailCampaignModifyDto, EmailCampaignModifyBodyDto>
{
    public EmailCampaignModifyDtoValidator()
    {
        _ = RuleFor(d => d.Body)
            .SetValidator(new EmailCampaignModifyBodyDtoValidator());
    }
}

public class EmailCampaignModifyBodyDtoValidator : AbstractValidator<EmailCampaignModifyBodyDto>
{
    public EmailCampaignModifyBodyDtoValidator()
    {
        _ = RuleFor(c => c.Name)
            .NotEmpty()
            .FreeText(200);

        _ = RuleFor(c => c.Subject)
            .NotEmpty()
            .FreeText(500);

        _ = RuleFor(c => c.EmailTemplateId)
            .NotEmpty();
    }
}

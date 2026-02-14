using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General.Extensions;
using Orso.Arpa.Domain.EmailCampaignDomain.Commands;

namespace Orso.Arpa.Application.EmailCampaignApplication.Model;

public class EmailCampaignCreateDto
{
    public string Name { get; set; }
    public string Subject { get; set; }
    public Guid EmailTemplateId { get; set; }
}

public class EmailCampaignCreateDtoMappingProfile : Profile
{
    public EmailCampaignCreateDtoMappingProfile()
    {
        _ = CreateMap<EmailCampaignCreateDto, CreateEmailCampaign.Command>();
    }
}

public class EmailCampaignCreateDtoValidator : AbstractValidator<EmailCampaignCreateDto>
{
    public EmailCampaignCreateDtoValidator()
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

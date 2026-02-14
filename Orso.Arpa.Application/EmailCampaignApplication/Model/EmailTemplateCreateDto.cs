using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General.Extensions;
using Orso.Arpa.Domain.EmailCampaignDomain.Commands;

namespace Orso.Arpa.Application.EmailCampaignApplication.Model;

public class EmailTemplateCreateDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ProjectDataJson { get; set; }
    public string MjmlSource { get; set; }
    public string CompiledHtml { get; set; }
}

public class EmailTemplateCreateDtoMappingProfile : Profile
{
    public EmailTemplateCreateDtoMappingProfile()
    {
        _ = CreateMap<EmailTemplateCreateDto, CreateEmailTemplate.Command>();
    }
}

public class EmailTemplateCreateDtoValidator : AbstractValidator<EmailTemplateCreateDto>
{
    public EmailTemplateCreateDtoValidator()
    {
        _ = RuleFor(c => c.Name)
            .NotEmpty()
            .FreeText(200);

        _ = RuleFor(c => c.Description)
            .FreeText(500);
    }
}

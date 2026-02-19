using System;
using System.Collections.Generic;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General.Extensions;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.SurveyDomain.Commands;

namespace Orso.Arpa.Application.SurveyApplication.Model;

public class SurveyModifyDto : IdFromRouteDto<SurveyModifyBodyDto>
{
}

public class SurveyModifyBodyDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsAnonymous { get; set; }
    public IList<QuestionCreateDto> Questions { get; set; } = new List<QuestionCreateDto>();
}

public class SurveyModifyDtoMappingProfile : Profile
{
    public SurveyModifyDtoMappingProfile()
    {
        _ = CreateMap<SurveyModifyDto, ModifySurvey.Command>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Body.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Body.Description))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.Body.StartDate))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.Body.EndDate))
            .ForMember(dest => dest.IsAnonymous, opt => opt.MapFrom(src => src.Body.IsAnonymous))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
    }
}

public class SurveyModifyDtoValidator : IdFromRouteDtoValidator<SurveyModifyDto, SurveyModifyBodyDto>
{
    public SurveyModifyDtoValidator()
    {
        _ = RuleFor(d => d.Body).SetValidator(new SurveyModifyBodyDtoValidator());
    }
}

public class SurveyModifyBodyDtoValidator : AbstractValidator<SurveyModifyBodyDto>
{
    public SurveyModifyBodyDtoValidator()
    {
        _ = RuleFor(c => c.Title)
            .NotEmpty()
            .FreeText(200);

        _ = RuleFor(c => c.Description)
            .FreeText(1000);
    }
}

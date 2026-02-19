using System;
using System.Collections.Generic;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General.Extensions;
using Orso.Arpa.Domain.SurveyDomain.Commands;

namespace Orso.Arpa.Application.SurveyApplication.Model;

public class SurveyCreateDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsAnonymous { get; set; }
    public IList<QuestionCreateDto> Questions { get; set; } = new List<QuestionCreateDto>();
}

public class QuestionCreateDto
{
    public string QuestionText { get; set; }
    public int QuestionType { get; set; }
    public int OrderIndex { get; set; }
    public bool IsRequired { get; set; }
    public string Settings { get; set; }
    public string ValidationRules { get; set; }
    public IList<AnswerOptionCreateDto> AnswerOptions { get; set; } = new List<AnswerOptionCreateDto>();
}

public class AnswerOptionCreateDto
{
    public string OptionText { get; set; }
    public int OrderIndex { get; set; }
    public string Value { get; set; }
}

public class SurveyCreateDtoMappingProfile : Profile
{
    public SurveyCreateDtoMappingProfile()
    {
        _ = CreateMap<SurveyCreateDto, CreateSurvey.Command>();
        _ = CreateMap<QuestionCreateDto, CreateSurvey.QuestionData>();
        _ = CreateMap<AnswerOptionCreateDto, CreateSurvey.AnswerOptionData>();
    }
}

public class SurveyCreateDtoValidator : AbstractValidator<SurveyCreateDto>
{
    public SurveyCreateDtoValidator()
    {
        _ = RuleFor(c => c.Title)
            .NotEmpty()
            .FreeText(200);

        _ = RuleFor(c => c.Description)
            .FreeText(1000);
    }
}

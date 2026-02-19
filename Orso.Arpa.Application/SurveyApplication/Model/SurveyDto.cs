using System;
using System.Collections.Generic;
using AutoMapper;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.SurveyDomain.Enums;
using Orso.Arpa.Domain.SurveyDomain.Model;

namespace Orso.Arpa.Application.SurveyApplication.Model;

public class SurveyDto : BaseEntityDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsActive { get; set; }
    public bool IsAnonymous { get; set; }
    public IList<SurveyQuestionDto> Questions { get; set; }
}

public class SurveyQuestionDto
{
    public Guid Id { get; set; }
    public string QuestionText { get; set; }
    public QuestionType QuestionType { get; set; }
    public int OrderIndex { get; set; }
    public bool IsRequired { get; set; }
    public string Settings { get; set; }
    public string ValidationRules { get; set; }
    public IList<SurveyAnswerOptionDto> AnswerOptions { get; set; }
}

public class SurveyAnswerOptionDto
{
    public Guid Id { get; set; }
    public string OptionText { get; set; }
    public int OrderIndex { get; set; }
    public string Value { get; set; }
}

public class SurveyDtoMappingProfile : Profile
{
    public SurveyDtoMappingProfile()
    {
        CreateMap<Survey, SurveyDto>()
            .IncludeBase<BaseEntity, BaseEntityDto>();

        CreateMap<SurveyQuestion, SurveyQuestionDto>();
        CreateMap<SurveyAnswerOption, SurveyAnswerOptionDto>();
    }
}

using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.SurveyDomain.Enums;

namespace Orso.Arpa.Application.SurveyApplication.Model;

public class SurveyResultsDto
{
    public int TotalResponses { get; set; }
    public int CompletedResponses { get; set; }
    public List<QuestionStatisticsDto> Questions { get; set; } = new();
}

public class QuestionStatisticsDto
{
    public Guid QuestionId { get; set; }
    public string QuestionText { get; set; }
    public QuestionType QuestionType { get; set; }
    public int AnswerCount { get; set; }
    public Dictionary<string, int> AnswerDistribution { get; set; } = new();
    public List<string> FreeTextAnswers { get; set; } = new();
    public double? AverageRating { get; set; }
}

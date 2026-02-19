using System;
using System.Collections.Generic;

namespace Orso.Arpa.Application.SurveyApplication.Model;

public class SurveyResponseDetailDto
{
    public Guid ResponseId { get; set; }
    public string UserDisplayName { get; set; }
    public string UserEmail { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public bool IsComplete { get; set; }
    public List<AnswerDetailDto> Answers { get; set; } = new();
}

public class AnswerDetailDto
{
    public Guid QuestionId { get; set; }
    public string QuestionText { get; set; }
    public string AnswerValue { get; set; }
}

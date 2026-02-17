using System;
using System.Collections.Generic;

namespace Orso.Arpa.Application.SurveyApplication.Model;

public class MyResponseDto
{
    public Guid Id { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public bool IsComplete { get; set; }
    public List<MyAnswerDto> Answers { get; set; } = new();
}

public class MyAnswerDto
{
    public Guid QuestionId { get; set; }
    public string AnswerValue { get; set; }
    public DateTime AnsweredAt { get; set; }
}

using System;
using System.Collections.Generic;

namespace Orso.Arpa.Application.SurveyApplication.Model;

public class SubmitSurveyResponseDto
{
    public IList<AnswerDto> Answers { get; set; } = new List<AnswerDto>();
}

public class AnswerDto
{
    public Guid QuestionId { get; set; }
    public string AnswerValue { get; set; }
}

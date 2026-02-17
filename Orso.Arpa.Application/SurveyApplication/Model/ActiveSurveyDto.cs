using System;

namespace Orso.Arpa.Application.SurveyApplication.Model;

public class ActiveSurveyDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int QuestionCount { get; set; }
    public bool HasUserResponded { get; set; }
}

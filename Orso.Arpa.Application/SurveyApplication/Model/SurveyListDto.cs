using System;

namespace Orso.Arpa.Application.SurveyApplication.Model;

public class SurveyListDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public bool IsAnonymous { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int QuestionCount { get; set; }
    public int ResponseCount { get; set; }
    public DateTime CreatedAt { get; set; }
}

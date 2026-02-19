using System;
using Orso.Arpa.Domain.General.Model;

namespace Orso.Arpa.Domain.SurveyDomain.Model;

public class SurveyAnswer : BaseEntity
{
    public SurveyAnswer(Guid? id, Guid responseId, Guid questionId, string answerValue) : base(id)
    {
        ResponseId = responseId;
        QuestionId = questionId;
        AnswerValue = answerValue;
        AnsweredAt = DateTime.UtcNow;
    }

    protected SurveyAnswer() { }

    public Guid ResponseId { get; private set; }
    public virtual SurveyUserResponse Response { get; private set; }
    public Guid QuestionId { get; private set; }
    public virtual SurveyQuestion Question { get; private set; }
    public string AnswerValue { get; private set; }
    public DateTime AnsweredAt { get; private set; }
}

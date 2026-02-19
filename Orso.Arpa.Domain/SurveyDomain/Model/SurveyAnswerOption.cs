using System;
using Orso.Arpa.Domain.General.Model;

namespace Orso.Arpa.Domain.SurveyDomain.Model;

public class SurveyAnswerOption : BaseEntity
{
    public SurveyAnswerOption(Guid? id, Guid questionId, string optionText, int orderIndex, string value) : base(id)
    {
        QuestionId = questionId;
        OptionText = optionText;
        OrderIndex = orderIndex;
        Value = value;
    }

    protected SurveyAnswerOption() { }

    public Guid QuestionId { get; private set; }
    public virtual SurveyQuestion Question { get; private set; }
    public string OptionText { get; private set; }
    public int OrderIndex { get; private set; }
    public string Value { get; private set; }

    public void Update(string optionText, int orderIndex, string value)
    {
        OptionText = optionText;
        OrderIndex = orderIndex;
        Value = value;
    }
}

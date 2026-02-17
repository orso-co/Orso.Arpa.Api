using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.SurveyDomain.Enums;

namespace Orso.Arpa.Domain.SurveyDomain.Model;

public class SurveyQuestion : BaseEntity
{
    public SurveyQuestion(Guid? id, Guid surveyId, string questionText, QuestionType questionType,
        int orderIndex, bool isRequired, string settings, string validationRules) : base(id)
    {
        SurveyId = surveyId;
        QuestionText = questionText;
        QuestionType = questionType;
        OrderIndex = orderIndex;
        IsRequired = isRequired;
        Settings = settings;
        ValidationRules = validationRules;
    }

    protected SurveyQuestion() { }

    public Guid SurveyId { get; private set; }
    public virtual Survey Survey { get; private set; }
    public string QuestionText { get; private set; }
    public QuestionType QuestionType { get; private set; }
    public int OrderIndex { get; private set; }
    public bool IsRequired { get; private set; }
    public string Settings { get; private set; }
    public string ValidationRules { get; private set; }

    public virtual ICollection<SurveyAnswerOption> AnswerOptions { get; private set; } = new List<SurveyAnswerOption>();

    public void Update(string questionText, QuestionType questionType, int orderIndex, bool isRequired, string settings, string validationRules)
    {
        QuestionText = questionText;
        QuestionType = questionType;
        OrderIndex = orderIndex;
        IsRequired = isRequired;
        Settings = settings;
        ValidationRules = validationRules;
    }
}

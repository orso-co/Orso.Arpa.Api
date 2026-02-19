using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.SurveyDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Create;

namespace Orso.Arpa.Domain.SurveyDomain.Commands;

public static class CreateSurvey
{
    public class Command : ICreateCommand<Survey>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsAnonymous { get; set; }
        public IList<QuestionData> Questions { get; set; } = new List<QuestionData>();
    }

    public class QuestionData
    {
        public string QuestionText { get; set; }
        public int QuestionType { get; set; }
        public int OrderIndex { get; set; }
        public bool IsRequired { get; set; }
        public string Settings { get; set; }
        public string ValidationRules { get; set; }
        public IList<AnswerOptionData> AnswerOptions { get; set; } = new List<AnswerOptionData>();
    }

    public class AnswerOptionData
    {
        public string OptionText { get; set; }
        public int OrderIndex { get; set; }
        public string Value { get; set; }
    }
}

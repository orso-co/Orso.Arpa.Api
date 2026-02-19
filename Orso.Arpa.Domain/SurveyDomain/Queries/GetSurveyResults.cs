using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.SurveyDomain.Enums;
using Orso.Arpa.Domain.SurveyDomain.Model;

namespace Orso.Arpa.Domain.SurveyDomain.Queries;

public static class GetSurveyResults
{
    public class Query : IRequest<Result>
    {
        public Guid SurveyId { get; set; }
    }

    public class Result
    {
        public int TotalResponses { get; set; }
        public int CompletedResponses { get; set; }
        public List<QuestionStatistics> Questions { get; set; } = new();
    }

    public class QuestionStatistics
    {
        public Guid QuestionId { get; set; }
        public string QuestionText { get; set; }
        public QuestionType QuestionType { get; set; }
        public int AnswerCount { get; set; }
        public Dictionary<string, int> AnswerDistribution { get; set; } = new();
        public List<string> FreeTextAnswers { get; set; } = new();
        public double? AverageRating { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result>
    {
        private readonly IArpaContext _arpaContext;

        public Handler(IArpaContext arpaContext)
        {
            _arpaContext = arpaContext;
        }

        public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
        {
            List<SurveyUserResponse> responses = await _arpaContext.Set<SurveyUserResponse>()
                .Where(r => r.SurveyId == request.SurveyId && !r.Deleted)
                .Include(r => r.Answers)
                .ToListAsync(cancellationToken);

            List<SurveyQuestion> questions = await _arpaContext.Set<SurveyQuestion>()
                .Where(q => q.SurveyId == request.SurveyId && !q.Deleted)
                .OrderBy(q => q.OrderIndex)
                .ToListAsync(cancellationToken);

            var allAnswers = responses.SelectMany(r => r.Answers.Where(a => !a.Deleted)).ToList();

            var result = new Result
            {
                TotalResponses = responses.Count,
                CompletedResponses = responses.Count(r => r.IsComplete),
                Questions = questions.Select(q =>
                {
                    var questionAnswers = allAnswers.Where(a => a.QuestionId == q.Id).ToList();
                    var stats = new QuestionStatistics
                    {
                        QuestionId = q.Id,
                        QuestionText = q.QuestionText,
                        QuestionType = q.QuestionType,
                        AnswerCount = questionAnswers.Count,
                    };

                    switch (q.QuestionType)
                    {
                        case QuestionType.YesNo:
                        case QuestionType.YesNoMaybe:
                        case QuestionType.SingleChoice:
                        case QuestionType.MultipleChoice:
                            stats.AnswerDistribution = questionAnswers
                                .GroupBy(a => a.AnswerValue)
                                .ToDictionary(g => g.Key, g => g.Count());
                            break;

                        case QuestionType.Rating:
                        case QuestionType.NumericInput:
                            stats.AnswerDistribution = questionAnswers
                                .GroupBy(a => a.AnswerValue)
                                .ToDictionary(g => g.Key, g => g.Count());
                            var numericValues = questionAnswers
                                .Select(a => double.TryParse(a.AnswerValue, out double v) ? v : (double?)null)
                                .Where(v => v.HasValue)
                                .Select(v => v.Value)
                                .ToList();
                            if (numericValues.Any())
                            {
                                stats.AverageRating = numericValues.Average();
                            }
                            break;

                        case QuestionType.FreeText:
                            stats.FreeTextAnswers = questionAnswers
                                .Select(a => a.AnswerValue)
                                .Where(v => !string.IsNullOrWhiteSpace(v))
                                .ToList();
                            break;
                    }

                    return stats;
                }).ToList()
            };

            return result;
        }
    }
}

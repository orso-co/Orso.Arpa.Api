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

public static class GetSurveyById
{
    public class Query : IRequest<Result>
    {
        public Guid Id { get; set; }
    }

    public class Result
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsAnonymous { get; set; }
        public List<QuestionResult> Questions { get; set; } = new();
    }

    public class QuestionResult
    {
        public Guid Id { get; set; }
        public string QuestionText { get; set; }
        public QuestionType QuestionType { get; set; }
        public int OrderIndex { get; set; }
        public bool IsRequired { get; set; }
        public string Settings { get; set; }
        public string ValidationRules { get; set; }
        public List<AnswerOptionResult> AnswerOptions { get; set; } = new();
    }

    public class AnswerOptionResult
    {
        public Guid Id { get; set; }
        public string OptionText { get; set; }
        public int OrderIndex { get; set; }
        public string Value { get; set; }
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
            Survey survey = await _arpaContext.Set<Survey>()
                .Include(s => s.Questions.Where(q => !q.Deleted).OrderBy(q => q.OrderIndex))
                    .ThenInclude(q => q.AnswerOptions.Where(a => !a.Deleted).OrderBy(a => a.OrderIndex))
                .FirstOrDefaultAsync(s => s.Id == request.Id && !s.Deleted, cancellationToken);

            if (survey == null) return null;

            return new Result
            {
                Id = survey.Id,
                Title = survey.Title,
                Description = survey.Description,
                StartDate = survey.StartDate,
                EndDate = survey.EndDate,
                IsActive = survey.IsActive,
                IsAnonymous = survey.IsAnonymous,
                Questions = survey.Questions.Select(q => new QuestionResult
                {
                    Id = q.Id,
                    QuestionText = q.QuestionText,
                    QuestionType = q.QuestionType,
                    OrderIndex = q.OrderIndex,
                    IsRequired = q.IsRequired,
                    Settings = q.Settings,
                    ValidationRules = q.ValidationRules,
                    AnswerOptions = q.AnswerOptions.Select(a => new AnswerOptionResult
                    {
                        Id = a.Id,
                        OptionText = a.OptionText,
                        OrderIndex = a.OrderIndex,
                        Value = a.Value
                    }).ToList()
                }).ToList()
            };
        }
    }
}

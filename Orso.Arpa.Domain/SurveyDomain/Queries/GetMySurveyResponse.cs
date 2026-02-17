using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.SurveyDomain.Model;

namespace Orso.Arpa.Domain.SurveyDomain.Queries;

public static class GetMySurveyResponse
{
    public class Query : IRequest<Result>
    {
        public Guid SurveyId { get; set; }
        public Guid UserId { get; set; }
    }

    public class Result
    {
        public Guid Id { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public bool IsComplete { get; set; }
        public List<AnswerResult> Answers { get; set; } = new();
    }

    public class AnswerResult
    {
        public Guid QuestionId { get; set; }
        public string AnswerValue { get; set; }
        public DateTime AnsweredAt { get; set; }
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
            SurveyUserResponse response = await _arpaContext.Set<SurveyUserResponse>()
                .Include(r => r.Answers)
                .FirstOrDefaultAsync(r => r.SurveyId == request.SurveyId && r.UserId == request.UserId && !r.Deleted, cancellationToken);

            if (response == null) return null;

            return new Result
            {
                Id = response.Id,
                StartedAt = response.StartedAt,
                CompletedAt = response.CompletedAt,
                IsComplete = response.IsComplete,
                Answers = response.Answers.Where(a => !a.Deleted).Select(a => new AnswerResult
                {
                    QuestionId = a.QuestionId,
                    AnswerValue = a.AnswerValue,
                    AnsweredAt = a.AnsweredAt
                }).ToList()
            };
        }
    }
}

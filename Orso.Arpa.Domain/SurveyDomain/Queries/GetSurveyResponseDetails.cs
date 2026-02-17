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

public static class GetSurveyResponseDetails
{
    public class Query : IRequest<IEnumerable<Result>>
    {
        public Guid SurveyId { get; set; }
    }

    public class Result
    {
        public Guid ResponseId { get; set; }
        public string UserDisplayName { get; set; }
        public string UserEmail { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public bool IsComplete { get; set; }
        public List<AnswerDetail> Answers { get; set; } = new();
    }

    public class AnswerDetail
    {
        public Guid QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string AnswerValue { get; set; }
    }

    public class Handler : IRequestHandler<Query, IEnumerable<Result>>
    {
        private readonly IArpaContext _arpaContext;

        public Handler(IArpaContext arpaContext)
        {
            _arpaContext = arpaContext;
        }

        public async Task<IEnumerable<Result>> Handle(Query request, CancellationToken cancellationToken)
        {
            List<SurveyUserResponse> responses = await _arpaContext.Set<SurveyUserResponse>()
                .Where(r => r.SurveyId == request.SurveyId && !r.Deleted)
                .Include(r => r.User)
                .Include(r => r.Answers.Where(a => !a.Deleted))
                    .ThenInclude(a => a.Question)
                .OrderByDescending(r => r.CompletedAt)
                .ToListAsync(cancellationToken);

            return responses.Select(r => new Result
            {
                ResponseId = r.Id,
                UserDisplayName = r.User?.UserName,
                UserEmail = r.User?.Email,
                StartedAt = r.StartedAt,
                CompletedAt = r.CompletedAt,
                IsComplete = r.IsComplete,
                Answers = r.Answers.Select(a => new AnswerDetail
                {
                    QuestionId = a.QuestionId,
                    QuestionText = a.Question?.QuestionText,
                    AnswerValue = a.AnswerValue
                }).ToList()
            });
        }
    }
}

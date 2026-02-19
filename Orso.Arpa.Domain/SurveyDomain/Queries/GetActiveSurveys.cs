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

public static class GetActiveSurveys
{
    public class Query : IRequest<IEnumerable<Result>>
    {
        public Guid UserId { get; set; }
    }

    public class Result
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int QuestionCount { get; set; }
        public bool HasUserResponded { get; set; }
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
            DateTime now = DateTime.UtcNow;

            List<Survey> activeSurveys = await _arpaContext.Set<Survey>()
                .Where(s => s.IsActive && !s.Deleted)
                .Where(s => !s.StartDate.HasValue || s.StartDate.Value <= now)
                .Where(s => !s.EndDate.HasValue || s.EndDate.Value >= now)
                .Include(s => s.Questions)
                .Include(s => s.UserResponses)
                .ToListAsync(cancellationToken);

            return activeSurveys.Select(s => new Result
            {
                Id = s.Id,
                Title = s.Title,
                Description = s.Description,
                QuestionCount = s.Questions.Count(q => !q.Deleted),
                HasUserResponded = s.UserResponses.Any(r => r.UserId == request.UserId && !r.Deleted)
            });
        }
    }
}

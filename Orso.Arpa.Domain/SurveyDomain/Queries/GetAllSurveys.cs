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

public static class GetAllSurveys
{
    public class Query : IRequest<IEnumerable<Result>>
    {
    }

    public class Result
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

    public class Handler : IRequestHandler<Query, IEnumerable<Result>>
    {
        private readonly IArpaContext _arpaContext;

        public Handler(IArpaContext arpaContext)
        {
            _arpaContext = arpaContext;
        }

        public async Task<IEnumerable<Result>> Handle(Query request, CancellationToken cancellationToken)
        {
            List<Survey> surveys = await _arpaContext.Set<Survey>()
                .Where(s => !s.Deleted)
                .Include(s => s.Questions)
                .Include(s => s.UserResponses)
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync(cancellationToken);

            return surveys.Select(s => new Result
            {
                Id = s.Id,
                Title = s.Title,
                Description = s.Description,
                IsActive = s.IsActive,
                IsAnonymous = s.IsAnonymous,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                QuestionCount = s.Questions.Count(q => !q.Deleted),
                ResponseCount = s.UserResponses.Count(r => !r.Deleted),
                CreatedAt = s.CreatedAt
            });
        }
    }
}

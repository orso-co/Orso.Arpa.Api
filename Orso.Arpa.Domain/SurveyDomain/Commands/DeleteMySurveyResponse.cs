using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.SurveyDomain.Model;

namespace Orso.Arpa.Domain.SurveyDomain.Commands;

public static class DeleteMySurveyResponse
{
    public class Command : IRequest
    {
        public Guid SurveyId { get; set; }
        public Guid UserId { get; set; }
    }

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IArpaContext _arpaContext;

        public Handler(IArpaContext arpaContext)
        {
            _arpaContext = arpaContext;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            SurveyUserResponse response = await _arpaContext.Set<SurveyUserResponse>()
                .Include(r => r.Answers)
                .FirstOrDefaultAsync(r => r.SurveyId == request.SurveyId && r.UserId == request.UserId && !r.Deleted, cancellationToken);

            if (response != null)
            {
                foreach (SurveyAnswer answer in response.Answers.ToList())
                {
                    _ = _arpaContext.Remove(answer);
                }
                _ = _arpaContext.Remove(response);
                await _arpaContext.SaveChangesAsync(cancellationToken);
            }
            return Unit.Value;
        }
    }
}

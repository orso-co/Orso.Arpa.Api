using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.SurveyDomain.Model;

namespace Orso.Arpa.Domain.SurveyDomain.Commands;

public static class SubmitSurveyResponse
{
    public class Command : IRequest
    {
        public Guid SurveyId { get; set; }
        public Guid UserId { get; set; }
        public string IpAddress { get; set; }
        public IList<AnswerData> Answers { get; set; } = new List<AnswerData>();
    }

    public class AnswerData
    {
        public Guid QuestionId { get; set; }
        public string AnswerValue { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator(IArpaContext arpaContext)
        {
            _ = RuleFor(d => d.SurveyId)
                .EntityExists<Command, Survey>(arpaContext);
        }
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
            // Delete existing response if retaking
            SurveyUserResponse existingResponse = await _arpaContext.Set<SurveyUserResponse>()
                .Include(r => r.Answers)
                .FirstOrDefaultAsync(r => r.SurveyId == request.SurveyId && r.UserId == request.UserId && !r.Deleted, cancellationToken);

            if (existingResponse != null)
            {
                foreach (SurveyAnswer answer in existingResponse.Answers.ToList())
                {
                    _ = _arpaContext.Remove(answer);
                }
                _ = _arpaContext.Remove(existingResponse);
                await _arpaContext.SaveChangesAsync(cancellationToken);
            }

            // Create new response
            var response = new SurveyUserResponse(null, request.SurveyId, request.UserId);

            foreach (AnswerData answerData in request.Answers)
            {
                var answer = new SurveyAnswer(null, response.Id, answerData.QuestionId, answerData.AnswerValue);
                response.Answers.Add(answer);
            }

            response.Complete(request.IpAddress);

            _ = _arpaContext.Add(response);
            await _arpaContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}

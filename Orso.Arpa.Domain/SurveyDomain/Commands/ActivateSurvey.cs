using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.SurveyDomain.Model;

namespace Orso.Arpa.Domain.SurveyDomain.Commands;

public static class ActivateSurvey
{
    public class Command : IRequest
    {
        public Guid Id { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator(IArpaContext arpaContext)
        {
            _ = RuleFor(d => d.Id)
                .EntityExists<Command, Survey>(arpaContext);
        }
    }

    public class Handler : IRequestHandler<Command>
    {
        private readonly IArpaContext _arpaContext;

        public Handler(IArpaContext arpaContext)
        {
            _arpaContext = arpaContext;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            Survey survey = await _arpaContext.GetByIdAsync<Survey>(request.Id, cancellationToken);
            survey.Activate();
            await _arpaContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}

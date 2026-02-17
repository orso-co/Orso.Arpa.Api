using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.MediathekDomain.Enums;
using Orso.Arpa.Domain.MediathekDomain.Model;

namespace Orso.Arpa.Domain.MediathekDomain.Commands
{
    public static class DenyMediathekAccessRequest
    {
        public class Command : IRequest<Unit>
        {
            public Guid Id { get; set; }
            public string ProcessedBy { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.Id)
                    .MustAsync(async (id, cancellation) => await arpaContext.EntityExistsAsync<MediathekAccessRequest>(id, cancellation))
                    .WithErrorCode("404")
                    .WithMessage("Access request could not be found.");

                RuleFor(c => c.Id)
                    .MustAsync(async (id, cancellation) =>
                    {
                        var request = await arpaContext.Set<MediathekAccessRequest>().FindAsync([id], cancellation);
                        return request?.Status == MediathekAccessRequestStatus.Pending;
                    })
                    .WithMessage("Access request is not in pending status.");
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
                MediathekAccessRequest accessRequest = await _arpaContext.GetByIdAsync<MediathekAccessRequest>(request.Id, cancellationToken);
                accessRequest.Deny(request.ProcessedBy);
                await _arpaContext.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}

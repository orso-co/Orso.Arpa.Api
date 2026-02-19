using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.MediathekDomain.Enums;
using Orso.Arpa.Domain.MediathekDomain.Model;

namespace Orso.Arpa.Domain.MediathekDomain.Commands
{
    public static class ApproveMediathekAccessRequest
    {
        public class Command : IRequest<MediathekAccess>
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

        public class Handler : IRequestHandler<Command, MediathekAccess>
        {
            private readonly IArpaContext _arpaContext;

            public Handler(IArpaContext arpaContext)
            {
                _arpaContext = arpaContext;
            }

            public async Task<MediathekAccess> Handle(Command request, CancellationToken cancellationToken)
            {
                MediathekAccessRequest accessRequest = await _arpaContext.Set<MediathekAccessRequest>()
                    .Include(r => r.Person)
                    .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

                accessRequest.Approve(request.ProcessedBy);

                var access = new MediathekAccess(Guid.NewGuid(), accessRequest.PersonId, request.ProcessedBy, null);
                _arpaContext.Add(access);

                await _arpaContext.SaveChangesAsync(cancellationToken);

                _arpaContext.ClearChangeTracker();
                return await _arpaContext.Set<MediathekAccess>()
                    .Include(a => a.Person)
                    .FirstOrDefaultAsync(a => a.Id == access.Id, cancellationToken);
            }
        }
    }
}

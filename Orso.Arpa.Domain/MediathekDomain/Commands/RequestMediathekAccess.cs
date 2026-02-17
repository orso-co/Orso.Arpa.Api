using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.MediathekDomain.Enums;
using Orso.Arpa.Domain.MediathekDomain.Model;
using Orso.Arpa.Domain.PersonDomain.Model;

namespace Orso.Arpa.Domain.MediathekDomain.Commands
{
    public static class RequestMediathekAccess
    {
        public class Command : IRequest<MediathekAccessRequest>
        {
            public Guid PersonId { get; set; }
            public string Message { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.PersonId)
                    .MustAsync(async (personId, cancellation) => await arpaContext.EntityExistsAsync<Person>(personId, cancellation))
                    .WithErrorCode("404")
                    .WithMessage("Person could not be found.");

                RuleFor(c => c.PersonId)
                    .MustAsync(async (personId, cancellation) =>
                        !await arpaContext.Set<MediathekAccess>().AnyAsync(a => a.PersonId == personId && a.IsActive, cancellation))
                    .WithMessage("Person already has active Mediathek access.");

                RuleFor(c => c.PersonId)
                    .MustAsync(async (personId, cancellation) =>
                        !await arpaContext.Set<MediathekAccessRequest>().AnyAsync(r => r.PersonId == personId && r.Status == MediathekAccessRequestStatus.Pending, cancellation))
                    .WithMessage("Person already has a pending access request.");
            }
        }

        public class Handler : IRequestHandler<Command, MediathekAccessRequest>
        {
            private readonly IArpaContext _arpaContext;

            public Handler(IArpaContext arpaContext)
            {
                _arpaContext = arpaContext;
            }

            public async Task<MediathekAccessRequest> Handle(Command request, CancellationToken cancellationToken)
            {
                var accessRequest = new MediathekAccessRequest(Guid.NewGuid(), request.PersonId, request.Message);
                _arpaContext.Add(accessRequest);
                await _arpaContext.SaveChangesAsync(cancellationToken);
                return accessRequest;
            }
        }
    }
}

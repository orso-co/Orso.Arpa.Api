using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.MediathekDomain.Model;
using Orso.Arpa.Domain.PersonDomain.Model;

namespace Orso.Arpa.Domain.MediathekDomain.Commands
{
    public static class GrantMediathekAccess
    {
        public class Command : IRequest<MediathekAccess>
        {
            public Guid PersonId { get; set; }
            public string GrantedBy { get; set; }
            public string Notes { get; set; }
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
                var access = new MediathekAccess(Guid.NewGuid(), request.PersonId, request.GrantedBy, request.Notes);
                _arpaContext.Add(access);

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    _arpaContext.ClearChangeTracker();
                    return await _arpaContext.Set<MediathekAccess>()
                        .Include(a => a.Person)
                        .FirstOrDefaultAsync(a => a.Id == access.Id, cancellationToken);
                }

                return access;
            }
        }
    }
}

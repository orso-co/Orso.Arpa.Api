using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Orso.Arpa.Domain.General.Errors;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.MusicLibraryDomain.Model;

namespace Orso.Arpa.Domain.MusicLibraryDomain.Commands
{
    public static class DeleteSetlist
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext context)
            {
                RuleFor(c => c.Id)
                    .EntityExists<Command, Setlist>(context);
            }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly IArpaContext _context;

            public Handler(IArpaContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                Setlist setlist = await _context.Setlists.FindAsync([request.Id], cancellationToken)
                    ?? throw new NotFoundException(nameof(Setlist), nameof(request.Id));

                _context.Setlists.Remove(setlist);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}

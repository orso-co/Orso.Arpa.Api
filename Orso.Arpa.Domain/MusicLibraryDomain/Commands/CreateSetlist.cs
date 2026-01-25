using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.MusicLibraryDomain.Model;

namespace Orso.Arpa.Domain.MusicLibraryDomain.Commands
{
    public static class CreateSetlist
    {
        public class Command : IRequest<Setlist>
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public bool IsTemplate { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.Name)
                    .NotEmpty()
                    .MaximumLength(200);

                RuleFor(c => c.Description)
                    .MaximumLength(2000);
            }
        }

        public class Handler : IRequestHandler<Command, Setlist>
        {
            private readonly IArpaContext _context;

            public Handler(IArpaContext context)
            {
                _context = context;
            }

            public async Task<Setlist> Handle(Command request, CancellationToken cancellationToken)
            {
                var setlist = new Setlist(
                    Guid.NewGuid(),
                    request.Name,
                    request.Description,
                    request.IsTemplate);

                _context.Setlists.Add(setlist);
                await _context.SaveChangesAsync(cancellationToken);

                return setlist;
            }
        }
    }
}

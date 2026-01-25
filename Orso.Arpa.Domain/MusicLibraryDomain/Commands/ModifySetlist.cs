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
    public static class ModifySetlist
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool IsTemplate { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext context)
            {
                RuleFor(c => c.Id)
                    .EntityExists<Command, Setlist>(context);

                RuleFor(c => c.Name)
                    .NotEmpty()
                    .MaximumLength(200);

                RuleFor(c => c.Description)
                    .MaximumLength(2000);
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

                setlist.Name = request.Name;
                setlist.Description = request.Description;
                setlist.IsTemplate = request.IsTemplate;

                _context.Setlists.Update(setlist);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}

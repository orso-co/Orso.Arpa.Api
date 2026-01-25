using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.General.Errors;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.MusicLibraryDomain.Model;
using Orso.Arpa.Domain.SectionDomain.Model;

namespace Orso.Arpa.Domain.MusicLibraryDomain.Commands
{
    public static class AddSectionToMusicPieceFile
    {
        public class Command : IRequest<Unit>
        {
            public Guid MusicPieceFileId { get; set; }
            public Guid SectionId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.MusicPieceFileId)
                    .EntityExists<Command, MusicPieceFile>(arpaContext);

                RuleFor(c => c.SectionId)
                    .EntityExists<Command, Section>(arpaContext);

                RuleFor(c => c)
                    .MustAsync(async (cmd, cancellation) =>
                    {
                        return !await arpaContext.Set<MusicPieceFileSection>()
                            .AnyAsync(fs => fs.MusicPieceFileId == cmd.MusicPieceFileId
                                         && fs.SectionId == cmd.SectionId, cancellation);
                    })
                    .WithMessage("This section is already assigned to the file.");
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
                var fileSection = new MusicPieceFileSection(null, request.MusicPieceFileId, request.SectionId);

                _arpaContext.Add(fileSection);

                if (await _arpaContext.SaveChangesAsync(cancellationToken) < 1)
                {
                    throw new AffectedRowCountMismatchException(nameof(MusicPieceFileSection));
                }

                return Unit.Value;
            }
        }
    }
}

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
    public static class DeleteMusicPieceFile
    {
        public class Command : IRequest<Unit>
        {
            public Guid Id { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.Id)
                    .EntityExists<Command, MusicPieceFile>(arpaContext);
            }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly IMusicPieceFileAccessor _fileAccessor;
            private readonly IArpaContext _arpaContext;

            public Handler(IMusicPieceFileAccessor fileAccessor, IArpaContext arpaContext)
            {
                _fileAccessor = fileAccessor;
                _arpaContext = arpaContext;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var musicPieceFile = await _arpaContext.GetByIdAsync<MusicPieceFile>(request.Id, cancellationToken);

                // Delete from storage
                await _fileAccessor.DeleteAsync(musicPieceFile.StorageFileName);

                // Delete from database
                _arpaContext.Remove(musicPieceFile);

                if (await _arpaContext.SaveChangesAsync(cancellationToken) < 1)
                {
                    throw new AffectedRowCountMismatchException(nameof(MusicPieceFile));
                }

                return Unit.Value;
            }
        }
    }
}

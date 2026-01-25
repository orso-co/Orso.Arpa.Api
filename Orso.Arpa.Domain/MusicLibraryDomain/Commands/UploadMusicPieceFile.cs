using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Orso.Arpa.Domain.General.Errors;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.MusicLibraryDomain.Model;

namespace Orso.Arpa.Domain.MusicLibraryDomain.Commands
{
    public static class UploadMusicPieceFile
    {
        public class Command : IRequest<MusicPieceFile>
        {
            public Guid MusicPieceId { get; set; }
            public Guid? MusicPiecePartId { get; set; }
            public IFormFile FormFile { get; set; }
            public string Description { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.MusicPieceId)
                    .EntityExists<Command, MusicPiece>(arpaContext);

                RuleFor(c => c.MusicPiecePartId)
                    .EntityExists<Command, MusicPiecePart>(arpaContext)
                    .When(c => c.MusicPiecePartId.HasValue);

                RuleFor(c => c.FormFile)
                    .NotNull()
                    .WithMessage("A file must be provided.");

                RuleFor(c => c.FormFile.Length)
                    .LessThanOrEqualTo(50 * 1024 * 1024) // 50 MB max
                    .When(c => c.FormFile != null)
                    .WithMessage("File size must not exceed 50 MB.");

                RuleFor(c => c.Description)
                    .MaximumLength(500);
            }
        }

        public class Handler : IRequestHandler<Command, MusicPieceFile>
        {
            private readonly IFileNameGenerator _fileNameGenerator;
            private readonly IMusicPieceFileAccessor _fileAccessor;
            private readonly IArpaContext _arpaContext;

            public Handler(
                IFileNameGenerator fileNameGenerator,
                IMusicPieceFileAccessor fileAccessor,
                IArpaContext arpaContext)
            {
                _fileNameGenerator = fileNameGenerator;
                _fileAccessor = fileAccessor;
                _arpaContext = arpaContext;
            }

            public async Task<MusicPieceFile> Handle(Command request, CancellationToken cancellationToken)
            {
                var storageFileName = _fileNameGenerator.GenerateRandomFileName(request.FormFile);

                // Upload to storage
                await _fileAccessor.SaveAsync(request.FormFile, storageFileName);

                // Create database entity
                var musicPieceFile = new MusicPieceFile(null, request, storageFileName);

                _arpaContext.Add(musicPieceFile);

                if (await _arpaContext.SaveChangesAsync(cancellationToken) < 1)
                {
                    // Rollback file upload on database failure
                    await _fileAccessor.DeleteAsync(storageFileName);
                    throw new AffectedRowCountMismatchException(nameof(MusicPieceFile));
                }

                return musicPieceFile;
            }
        }
    }
}

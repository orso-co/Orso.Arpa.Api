using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Application.General.Services;
using Orso.Arpa.Application.MusicPieceApplication.Interfaces;
using Orso.Arpa.Application.MusicPieceApplication.Model;
using Orso.Arpa.Domain.General.GenericHandlers;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.MusicLibraryDomain;
using Orso.Arpa.Domain.MusicLibraryDomain.Commands;
using Orso.Arpa.Domain.MusicLibraryDomain.Model;

namespace Orso.Arpa.Application.MusicPieceApplication.Services
{
    public class MusicPieceService : BaseService<
        MusicPieceDto,
        MusicPiece,
        MusicPieceCreateDto,
        CreateMusicPiece.Command,
        MusicPieceModifyDto,
        MusicPieceModifyBodyDto,
        ModifyMusicPiece.Command
        >, IMusicPieceService
    {
        private readonly IArpaContext _arpaContext;
        private readonly IMusicPieceFileAccessor _fileAccessor;

        public MusicPieceService(
            IMediator mediator,
            IMapper mapper,
            IArpaContext arpaContext,
            IMusicPieceFileAccessor fileAccessor) : base(mediator, mapper)
        {
            _arpaContext = arpaContext;
            _fileAccessor = fileAccessor;
        }

        public async Task<IEnumerable<MusicPieceDto>> GetAsync(bool includeArchived = false)
        {
            Expression<Func<MusicPiece, bool>> predicate = includeArchived
                ? default
                : p => !p.IsArchived;

            IQueryable<MusicPiece> entities = await _mediator.Send(new List.Query<MusicPiece>(
                predicate: predicate,
                orderBy: q => q.OrderBy(p => p.Composer).ThenBy(p => p.Title)));

            List<MusicPiece> list = await entities.ToListAsync();
            return _mapper.Map<IEnumerable<MusicPieceDto>>(list);
        }

        public async Task SetArchivedAsync(Guid id, bool isArchived)
        {
            MusicPiece musicPiece = await _arpaContext.GetByIdAsync<MusicPiece>(id, default);
            musicPiece.SetArchived(isArchived);
            _ = await _arpaContext.SaveChangesAsync(default);
        }

        public async Task<MusicPieceFileDto> UploadFileAsync(Guid musicPieceId, Guid? partId, IFormFile file, string description)
        {
            var command = new UploadMusicPieceFile.Command
            {
                MusicPieceId = musicPieceId,
                MusicPiecePartId = partId,
                FormFile = file,
                Description = description
            };

            MusicPieceFile uploadedFile = await _mediator.Send(command);
            return _mapper.Map<MusicPieceFileDto>(uploadedFile);
        }

        public async Task<(byte[] Content, string ContentType, string FileName)> DownloadFileAsync(Guid fileId)
        {
            MusicPieceFile file = await _arpaContext.GetByIdAsync<MusicPieceFile>(fileId, default);
            IFileResult fileResult = await _fileAccessor.GetAsync(file.StorageFileName);

            return (fileResult.Content, file.ContentType, file.FileName);
        }

        public async Task DeleteFileAsync(Guid fileId)
        {
            var command = new DeleteMusicPieceFile.Command { Id = fileId };
            await _mediator.Send(command);
        }

        public async Task AddSectionToFileAsync(Guid fileId, Guid sectionId)
        {
            var command = new AddSectionToMusicPieceFile.Command
            {
                MusicPieceFileId = fileId,
                SectionId = sectionId
            };
            await _mediator.Send(command);
        }

        public async Task RemoveSectionFromFileAsync(Guid fileId, Guid sectionId)
        {
            var command = new RemoveSectionFromMusicPieceFile.Command
            {
                MusicPieceFileId = fileId,
                SectionId = sectionId
            };
            await _mediator.Send(command);
        }

        public async Task<AutoAssignSectionsResultDto> AutoAssignSectionsAsync(Guid? musicPieceId, bool dryRun)
        {
            var command = new AutoAssignSectionsToFiles.Command
            {
                MusicPieceId = musicPieceId,
                DryRun = dryRun
            };
            AutoAssignSectionsToFiles.Result result = await _mediator.Send(command);

            return new AutoAssignSectionsResultDto
            {
                FilesProcessed = result.FilesProcessed,
                FilesMatched = result.FilesMatched,
                SectionsAssigned = result.SectionsAssigned,
                Assignments = result.Assignments.Select(a => new FileAssignmentDto
                {
                    FileId = a.FileId,
                    FileName = a.FileName,
                    AssignedSections = a.AssignedSections
                }).ToList()
            };
        }
    }
}

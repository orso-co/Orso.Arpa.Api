using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Orso.Arpa.Application.MusicPieceApplication.Model;

namespace Orso.Arpa.Application.MusicPieceApplication.Interfaces
{
    public interface IMusicPieceService
    {
        Task<IEnumerable<MusicPieceDto>> GetAsync(bool includeArchived = false);
        Task<MusicPieceDto> GetByIdAsync(Guid id);
        Task<MusicPieceDto> CreateAsync(MusicPieceCreateDto createDto);
        Task ModifyAsync(MusicPieceModifyDto modifyDto);
        Task DeleteAsync(Guid id);
        Task SetArchivedAsync(Guid id, bool isArchived);

        // File operations
        Task<MusicPieceFileDto> UploadFileAsync(Guid musicPieceId, Guid? partId, IFormFile file, string description);
        Task<(byte[] Content, string ContentType, string FileName)> DownloadFileAsync(Guid fileId);
        Task DeleteFileAsync(Guid fileId);

        // File section operations
        Task AddSectionToFileAsync(Guid fileId, Guid sectionId);
        Task RemoveSectionFromFileAsync(Guid fileId, Guid sectionId);
        Task<AutoAssignSectionsResultDto> AutoAssignSectionsAsync(Guid? musicPieceId, bool dryRun);
    }
}

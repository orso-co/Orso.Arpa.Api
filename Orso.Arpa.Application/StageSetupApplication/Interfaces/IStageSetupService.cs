using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Orso.Arpa.Application.StageSetupApplication.Model;

namespace Orso.Arpa.Application.StageSetupApplication.Interfaces
{
    public interface IStageSetupService
    {
        /// <summary>
        /// Get all stage setups for a project
        /// </summary>
        Task<IEnumerable<StageSetupDto>> GetByProjectAsync(Guid projectId, bool includeHidden = false);

        /// <summary>
        /// Get a single stage setup by ID
        /// </summary>
        Task<StageSetupDto> GetByIdAsync(Guid id);

        /// <summary>
        /// Create a new stage setup with PDF file upload
        /// </summary>
        Task<StageSetupDto> CreateAsync(Guid projectId, StageSetupCreateDto createDto, Stream fileStream, string fileName, string contentType);

        /// <summary>
        /// Modify stage setup metadata
        /// </summary>
        Task ModifyAsync(StageSetupModifyBodyDto modifyDto);

        /// <summary>
        /// Delete a stage setup
        /// </summary>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Get the PDF file stream for a stage setup
        /// </summary>
        Task<(Stream FileStream, string FileName, string ContentType)> GetFileAsync(Guid id);

        /// <summary>
        /// Replace the PDF file for a stage setup
        /// </summary>
        Task ReplaceFileAsync(Guid id, Stream fileStream, string fileName, string contentType);

        /// <summary>
        /// Activate a stage setup (deactivates all others for the project)
        /// </summary>
        Task ActivateAsync(Guid projectId, Guid id);

        /// <summary>
        /// Set visibility for performers
        /// </summary>
        Task SetVisibilityAsync(Guid projectId, Guid id, bool isVisibleToPerformers);
    }
}

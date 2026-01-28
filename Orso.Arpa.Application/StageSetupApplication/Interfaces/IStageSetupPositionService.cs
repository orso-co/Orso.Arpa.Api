using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.StageSetupApplication.Model;

namespace Orso.Arpa.Application.StageSetupApplication.Interfaces
{
    public interface IStageSetupPositionService
    {
        /// <summary>
        /// Get all positions for a stage setup
        /// </summary>
        Task<IEnumerable<StageSetupPositionDto>> GetBySetupAsync(Guid stageSetupId);

        /// <summary>
        /// Create a new position
        /// </summary>
        Task<StageSetupPositionDto> CreateAsync(Guid stageSetupId, StageSetupPositionCreateDto createDto);

        /// <summary>
        /// Modify a position
        /// </summary>
        Task ModifyAsync(StageSetupPositionModifyBodyDto modifyDto);

        /// <summary>
        /// Delete a position (moves musician back to pool)
        /// </summary>
        Task DeleteAsync(Guid stageSetupId, Guid musicianProfileId);

        /// <summary>
        /// Bulk update positions (for drag-drop operations)
        /// </summary>
        Task<IEnumerable<StageSetupPositionDto>> BulkUpdateAsync(Guid stageSetupId, BulkUpdatePositionsDto bulkUpdateDto);
    }
}

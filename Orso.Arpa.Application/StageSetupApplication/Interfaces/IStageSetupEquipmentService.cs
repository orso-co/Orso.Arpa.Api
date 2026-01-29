using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.StageSetupApplication.Model;

namespace Orso.Arpa.Application.StageSetupApplication.Interfaces
{
    public interface IStageSetupEquipmentService
    {
        Task<IEnumerable<StageSetupEquipmentDto>> GetBySetupAsync(Guid stageSetupId);
        Task<StageSetupEquipmentDto> CreateAsync(Guid stageSetupId, StageSetupEquipmentCreateDto createDto);
        Task ModifyAsync(Guid id, StageSetupEquipmentModifyDto modifyDto);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<StageSetupEquipmentDto>> BulkUpdateAsync(Guid stageSetupId, BulkUpdateEquipmentDto bulkUpdateDto);
    }
}

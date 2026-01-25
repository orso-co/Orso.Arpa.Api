using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.SelectValueApplication.Model;

namespace Orso.Arpa.Application.SelectValueApplication.Interfaces
{
    public interface ISelectValueService
    {
        Task<IEnumerable<SelectValueDto>> GetAsync(string tableName, string propertyName);
        Task<SelectValueDto> CreateMappingAsync(SelectValueMappingCreateDto createDto);
        Task ModifyMappingAsync(SelectValueMappingModifyDto modifyDto);
        Task DeleteMappingAsync(Guid id);
    }
}

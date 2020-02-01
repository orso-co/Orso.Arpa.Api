using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.Logic.Regions;

namespace Orso.Arpa.Application.Interfaces
{
    public interface IRegionService
    {
        Task<RegionDto> CreateAsync(Create.Dto createDto);

        Task<RegionDto> GetByIdAsync(Guid id);

        Task<IEnumerable<RegionDto>> GetAsync();

        Task ModifyAsync(Modify.Dto modifyDto);

        Task DeleteAsync(Guid id);
    }
}

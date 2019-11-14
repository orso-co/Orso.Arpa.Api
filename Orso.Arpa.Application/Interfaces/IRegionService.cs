using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.Dtos;

namespace Orso.Arpa.Application.Interfaces
{
    public interface IRegionService
    {
        Task<RegionDto> CreateAsync(RegionCreateDto createDto);

        Task<RegionDto> GetByIdAsync(Guid id);

        Task<List<RegionDto>> GetAsync();

        Task ModifyAsync(RegionModifyDto modifyDto);

        Task DeleteAsync(Guid id);
    }
}

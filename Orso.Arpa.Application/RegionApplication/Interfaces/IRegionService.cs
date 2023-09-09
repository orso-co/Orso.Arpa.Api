using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.RegionApplication.Model;
using Orso.Arpa.Domain.RegionDomain.Enums;

namespace Orso.Arpa.Application.RegionApplication.Interfaces
{
    public interface IRegionService
    {
        Task<RegionDto> CreateAsync(RegionCreateDto createDto);

        Task<RegionDto> GetByIdAsync(Guid id);

        Task<IEnumerable<RegionDto>> GetAsync(RegionPreferenceType regionPreferenceType);

        Task ModifyAsync(RegionModifyDto modifyDto);

        Task DeleteAsync(Guid id);
    }
}

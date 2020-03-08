using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Orso.Arpa.Application.RegionApplication;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.Interfaces
{
    public interface IRegionService
    {
        Task<RegionDto> CreateAsync(RegionCreateDto createDto);

        Task<RegionDto> GetByIdAsync(Guid id);

        Task<IEnumerable<RegionDto>> GetAsync(
            Expression<Func<Region, bool>> predicate = null,
            Func<IQueryable<Region>, IOrderedQueryable<Region>> orderBy = null,
            int? skip = null,
            int? take = null);

        Task ModifyAsync(RegionModifyDto modifyDto);

        Task DeleteAsync(Guid id);
    }
}

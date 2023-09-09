using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.SectionApplication.Model;
using Orso.Arpa.Application.SelectValueApplication.Model;

namespace Orso.Arpa.Application.SectionApplication.Interfaces
{
    public interface ISectionService
    {
        Task<SectionTreeDto> GetTreeAsync(int? maxLevel);

        Task<IEnumerable<SectionDto>> GetAsync(bool instrumentsWithChildrenOnly);
        Task<IEnumerable<SectionDto>> GetDoublingInstrumentsAsync(Guid id);
        Task<IEnumerable<SelectValueDto>> GetPositionsAsync(Guid id);
    }
}

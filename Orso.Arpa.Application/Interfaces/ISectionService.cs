using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.SectionApplication;

namespace Orso.Arpa.Application.Interfaces
{
    public interface ISectionService
    {
        Task<SectionTreeDto> GetTreeAsync(int? maxLevel);

        Task<IEnumerable<SectionDto>> GetAsync(bool instrumentsOnly);
        Task<IEnumerable<SectionDto>> GetDoublingInstrumentsAsync(Guid id);
    }
}
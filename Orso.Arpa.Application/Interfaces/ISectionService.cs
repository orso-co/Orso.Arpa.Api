using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.Logic.Sections;

namespace Orso.Arpa.Application.Interfaces
{
    public interface ISectionService
    {
        Task<IEnumerable<SectionDto>> GetAsync();
    }
}

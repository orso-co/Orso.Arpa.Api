using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.ProjectApplication;

namespace Orso.Arpa.Application.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDto>> GetAsync(bool includeCompleted);
    }
}

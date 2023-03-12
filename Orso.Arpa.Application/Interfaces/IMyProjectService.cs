using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.MyProjectApplication;


namespace Orso.Arpa.Application.Interfaces
{
    public interface IMyProjectService
    {
        Task<IEnumerable<MyProjectDto>> GetMyProjectsAsync(int? offset, int? limit, bool includeCompleted);

        Task<MyProjectParticipationDto> SetProjectParticipationStatus(
            MyProjectParticipationModifyDto myProjectParticipationModifyDto);
    }
}

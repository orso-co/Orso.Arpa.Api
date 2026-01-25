using System;
using System.Threading.Tasks;
using Orso.Arpa.Application.MyProjectApplication.Model;
using Orso.Arpa.Application.SetlistApplication.Model;

namespace Orso.Arpa.Application.MyProjectApplication.Interfaces
{
    public interface IMyProjectService
    {
        Task<MyProjectListDto> GetMyProjectsAsync(int? offset, int? limit, bool includeCompleted);

        Task<MyProjectParticipationDto> SetProjectParticipationStatus(
            MyProjectParticipationModifyDto myProjectParticipationModifyDto);

        Task<SetlistDto> GetProjectSetlistAsync(Guid projectId);
    }
}

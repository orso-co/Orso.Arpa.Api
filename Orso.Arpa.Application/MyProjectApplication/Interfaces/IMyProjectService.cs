using System.Threading.Tasks;
using Orso.Arpa.Application.MyProjectApplication.Model;

namespace Orso.Arpa.Application.MyProjectApplication.Interfaces
{
    public interface IMyProjectService
    {
        Task<MyProjectListDto> GetMyProjectsAsync(int? offset, int? limit, bool includeCompleted);

        Task<MyProjectParticipationDto> SetProjectParticipationStatus(
            MyProjectParticipationModifyDto myProjectParticipationModifyDto);
    }
}

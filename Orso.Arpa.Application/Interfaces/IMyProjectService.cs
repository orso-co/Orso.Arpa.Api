using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.MyProjectApplication;


namespace Orso.Arpa.Application.Interfaces
{
    public interface IMyProjectService
    {
        Task<IEnumerable<MyProjectDto>> GetMyProjectsAsync();

        Task<MyProjectParticipationDto> SetProjectParticipationStatus(
            MyProjectParticipationModifyDto myProjectParticipationModifyDto);
    }
}

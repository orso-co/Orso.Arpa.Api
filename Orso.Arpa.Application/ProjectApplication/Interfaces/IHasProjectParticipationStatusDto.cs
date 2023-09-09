using Orso.Arpa.Domain.ProjectDomain.Enums;

namespace Orso.Arpa.Application.ProjectApplication.Interfaces
{
    public interface IHasProjectParticipationStatusDto
    {
        ProjectParticipationStatusInner? ParticipationStatusInner { get; }

        ProjectParticipationStatusInternal? ParticipationStatusInternal { get; }
    }
}

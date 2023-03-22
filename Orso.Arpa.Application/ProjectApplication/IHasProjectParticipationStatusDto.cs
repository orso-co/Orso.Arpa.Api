using Orso.Arpa.Domain.Enums;

namespace Orso.Arpa.Application.ProjectApplication
{
    public interface IHasProjectParticipationStatusDto
    {
        ProjectParticipationStatusInner? ParticipationStatusInner { get; }

        ProjectParticipationStatusInternal? ParticipationStatusInternal { get; }
    }
}

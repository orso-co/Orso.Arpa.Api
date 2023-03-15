using AutoMapper;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Enums;

namespace Orso.Arpa.Application.ProjectApplication
{
    public class ProjectParticipationStatusResultResolver : IValueResolver<ProjectParticipation, IHasProjectParticipationStatusDto, ProjectParticipationStatusResult>
    {
        public ProjectParticipationStatusResult Resolve(ProjectParticipation source, IHasProjectParticipationStatusDto destination, ProjectParticipationStatusResult member, ResolutionContext context)
        {
            return ProjectParticipationStatusInner.Acceptance.Equals(source.ParticipationStatusInner)
                && ProjectParticipationStatusInternal.Acceptance.Equals(source.ParticipationStatusInternal)
                ? ProjectParticipationStatusResult.Acceptance
                : ProjectParticipationStatusInner.Refusal.Equals(source.ParticipationStatusInner)
                || ProjectParticipationStatusInternal.Refusal.Equals(source.ParticipationStatusInternal)
                ? ProjectParticipationStatusResult.Refusal
                : ProjectParticipationStatusResult.Pending;
        }
    }
}

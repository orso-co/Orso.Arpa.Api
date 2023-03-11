using System.Collections.Generic;
using System.Linq;
using Orso.Arpa.Domain.Enums;

namespace Orso.Arpa.Domain.Logic.ProjectParticipations
{
    public static class ProjectParticipationStatusInheritanceEvaluator
    {
        public static ProjectParticipationStatusInner EvaluateNewParticpationStatusInner(
            IEnumerable<ProjectParticipationStatusInner?> childrenStatus)
        {
            return childrenStatus.Any(status => ProjectParticipationStatusInner.Acceptance.Equals(status))
                ? ProjectParticipationStatusInner.Acceptance
                : childrenStatus.All(status => ProjectParticipationStatusInner.Refusal.Equals(status))
                ? ProjectParticipationStatusInner.Refusal
                : childrenStatus.All(status => ProjectParticipationStatusInner.Interested.Equals(status))
                ? ProjectParticipationStatusInner.Interested
                : ProjectParticipationStatusInner.Pending;
        }

        public static ProjectParticipationStatusInternal EvaluateNewParticipationStatusInternal(
            IEnumerable<ProjectParticipationStatusInternal?> childrenStatus)
        {
            return childrenStatus.Any(status => ProjectParticipationStatusInternal.Acceptance.Equals(status))
                ? ProjectParticipationStatusInternal.Acceptance
                : childrenStatus.All(status => ProjectParticipationStatusInternal.Refusal.Equals(status))
                ? ProjectParticipationStatusInternal.Refusal
                : childrenStatus.All(status => ProjectParticipationStatusInternal.Candidate.Equals(status))
                ? ProjectParticipationStatusInternal.Candidate
                : ProjectParticipationStatusInternal.Pending;
        }

        public static ProjectInvitationStatus EvaluateNewInvitationStatus(
            IEnumerable<ProjectInvitationStatus?> childrenStatus)
        {
            return childrenStatus.Any(status => ProjectInvitationStatus.Invited.Equals(status))
                ? ProjectInvitationStatus.Invited
                : childrenStatus.All(status => ProjectInvitationStatus.Candidate.Equals(status))
                ? ProjectInvitationStatus.Candidate
                : childrenStatus.All(status => ProjectInvitationStatus.NotInvited.Equals(status))
                ? ProjectInvitationStatus.NotInvited
                : ProjectInvitationStatus.Unclear;
        }
    }
}

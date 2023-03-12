using System.Collections.Generic;
using System.Linq;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Misc;

namespace Orso.Arpa.Domain.Logic.ProjectParticipations
{
    public static class ProjectParticipationStatusInheritanceEvaluator
    {
        public static ProjectParticipationStatusInner EvaluateNewParticpationStatusInner(
            IEnumerable<ProjectParticipationStatusInner?> childrenStatus)
        {
            return childrenStatus.Any(status => ProjectParticipationStatusInner.Acceptance.Equals(status))
                ? ProjectParticipationStatusInner.Acceptance
                : childrenStatus.AreAllSame()
                ? childrenStatus.First() ?? ProjectParticipationStatusInner.Pending
                : ProjectParticipationStatusInner.Pending;
        }

        public static ProjectParticipationStatusInternal EvaluateNewParticipationStatusInternal(
            IEnumerable<ProjectParticipationStatusInternal?> childrenStatus)
        {
            return childrenStatus.Any(status => ProjectParticipationStatusInternal.Acceptance.Equals(status))
                ? ProjectParticipationStatusInternal.Acceptance
                : childrenStatus.AreAllSame()
                ? childrenStatus.First() ?? ProjectParticipationStatusInternal.Pending
                : ProjectParticipationStatusInternal.Pending;
        }

        public static ProjectInvitationStatus EvaluateNewInvitationStatus(
            IEnumerable<ProjectInvitationStatus?> childrenStatus)
        {
            return childrenStatus.Any(status => ProjectInvitationStatus.Invited.Equals(status))
                ? ProjectInvitationStatus.Invited
                : childrenStatus.AreAllSame()
                ? childrenStatus.First() ?? ProjectInvitationStatus.Unclear
                : ProjectInvitationStatus.Unclear;
        }
    }
}

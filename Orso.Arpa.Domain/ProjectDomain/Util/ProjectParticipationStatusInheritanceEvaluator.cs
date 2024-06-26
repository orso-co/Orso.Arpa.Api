using System.Collections.Generic;
using System.Linq;
using Orso.Arpa.Domain.ProjectDomain.Enums;
using Orso.Arpa.Misc.Extensions;

namespace Orso.Arpa.Domain.ProjectDomain.Util
{
    public static class ProjectParticipationStatusInheritanceEvaluator
    {
        private static readonly ProjectParticipationStatusInner[] refusalStates = [ProjectParticipationStatusInner.Refusal, ProjectParticipationStatusInner.RehearsalsOnly];

        public static ProjectParticipationStatusInner EvaluateNewParticpationStatusInner(
            IEnumerable<ProjectParticipationStatusInner?> childrenStatus)
        {
            if (childrenStatus.Any(status => ProjectParticipationStatusInner.Acceptance.Equals(status)))
            {
                return ProjectParticipationStatusInner.Acceptance;
            }
            if(childrenStatus.All(s => s.HasValue && refusalStates.Contains((ProjectParticipationStatusInner)s))) {
                return ProjectParticipationStatusInner.Refusal;
            }
            return childrenStatus.AreAllSame()
                ? childrenStatus.First() ?? ProjectParticipationStatusInner.Pending
                : ProjectParticipationStatusInner.Pending;
        }

        public static ProjectParticipationStatusInternal EvaluateNewParticipationStatusInternal(
            IEnumerable<ProjectParticipationStatusInternal?> childrenStatus)
        {
            if (childrenStatus.Any(status => ProjectParticipationStatusInternal.Acceptance.Equals(status)))
            {
                return ProjectParticipationStatusInternal.Acceptance;
            }
            return childrenStatus.AreAllSame()
                ? childrenStatus.First() ?? ProjectParticipationStatusInternal.Pending
                : ProjectParticipationStatusInternal.Pending;
        }

        public static ProjectInvitationStatus EvaluateNewInvitationStatus(
            IEnumerable<ProjectInvitationStatus?> childrenStatus)
        {
            if (childrenStatus.Any(status => ProjectInvitationStatus.Invited.Equals(status)))
            {
                return ProjectInvitationStatus.Invited;
            }
            return childrenStatus.AreAllSame()
                ? childrenStatus.First() ?? ProjectInvitationStatus.Unclear
                : ProjectInvitationStatus.Unclear;
        }
    }
}

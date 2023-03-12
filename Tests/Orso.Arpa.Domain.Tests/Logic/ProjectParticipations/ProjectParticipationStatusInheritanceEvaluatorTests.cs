using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Domain.Logic.ProjectParticipations;

namespace Orso.Arpa.Domain.Tests.Logic.ProjectParticipations
{
    [TestFixture]
    public class ProjectParticipationStatusInheritanceEvaluatorTests
    {
        private static IEnumerable<TestCaseData> s_participationStatusInnerTestData
        {
            get
            {
                yield return new TestCaseData(
                    new ProjectParticipationStatusInner?[] { ProjectParticipationStatusInner.Acceptance, ProjectParticipationStatusInner.Acceptance },
                    ProjectParticipationStatusInner.Acceptance);
                yield return new TestCaseData(
                    new ProjectParticipationStatusInner?[] { ProjectParticipationStatusInner.Refusal, ProjectParticipationStatusInner.Refusal },
                    ProjectParticipationStatusInner.Refusal);
                yield return new TestCaseData(
                    new ProjectParticipationStatusInner?[] { ProjectParticipationStatusInner.Interested, ProjectParticipationStatusInner.Interested },
                    ProjectParticipationStatusInner.Interested);
                yield return new TestCaseData(
                    new ProjectParticipationStatusInner?[] { ProjectParticipationStatusInner.Pending, ProjectParticipationStatusInner.Pending },
                    ProjectParticipationStatusInner.Pending);
                yield return new TestCaseData(
                    new ProjectParticipationStatusInner?[] { ProjectParticipationStatusInner.Acceptance, null },
                    ProjectParticipationStatusInner.Acceptance);
                yield return new TestCaseData(
                    new ProjectParticipationStatusInner?[] { ProjectParticipationStatusInner.Acceptance, ProjectParticipationStatusInner.Refusal },
                    ProjectParticipationStatusInner.Acceptance);
                yield return new TestCaseData(
                    new ProjectParticipationStatusInner?[] { ProjectParticipationStatusInner.Interested, ProjectParticipationStatusInner.Refusal },
                    ProjectParticipationStatusInner.Pending);
                yield return new TestCaseData(
                    new ProjectParticipationStatusInner?[] { null, ProjectParticipationStatusInner.Refusal },
                    ProjectParticipationStatusInner.Pending);
                yield return new TestCaseData(
                    new ProjectParticipationStatusInner?[] { null, null },
                    ProjectParticipationStatusInner.Pending);
            }
        }

        [Test]
        [TestCaseSource(nameof(s_participationStatusInnerTestData))]
        public void EvaluateNewParticpationStatusInner_Should_Return_ExpectedStatus(
            IEnumerable<ProjectParticipationStatusInner?> childrenStatus,
            ProjectParticipationStatusInner expectedResult)
        {
            // Act
            ProjectParticipationStatusInner result = ProjectParticipationStatusInheritanceEvaluator
                .EvaluateNewParticpationStatusInner(childrenStatus);

            // Assert
            _ = result.Should().Be(expectedResult);
        }

        private static IEnumerable<TestCaseData> s_participationStatusInternalTestData
        {
            get
            {
                yield return new TestCaseData(
                    new ProjectParticipationStatusInternal?[] { ProjectParticipationStatusInternal.Acceptance, ProjectParticipationStatusInternal.Acceptance },
                    ProjectParticipationStatusInternal.Acceptance);
                yield return new TestCaseData(
                    new ProjectParticipationStatusInternal?[] { ProjectParticipationStatusInternal.Refusal, ProjectParticipationStatusInternal.Refusal },
                    ProjectParticipationStatusInternal.Refusal);
                yield return new TestCaseData(
                    new ProjectParticipationStatusInternal?[] { ProjectParticipationStatusInternal.Candidate, ProjectParticipationStatusInternal.Candidate },
                    ProjectParticipationStatusInternal.Candidate);
                yield return new TestCaseData(
                    new ProjectParticipationStatusInternal?[] { ProjectParticipationStatusInternal.Pending, ProjectParticipationStatusInternal.Pending },
                    ProjectParticipationStatusInternal.Pending);
                yield return new TestCaseData(
                    new ProjectParticipationStatusInternal?[] { ProjectParticipationStatusInternal.Acceptance, null },
                    ProjectParticipationStatusInternal.Acceptance);
                yield return new TestCaseData(
                    new ProjectParticipationStatusInternal?[] { ProjectParticipationStatusInternal.Acceptance, ProjectParticipationStatusInternal.Refusal },
                    ProjectParticipationStatusInternal.Acceptance);
                yield return new TestCaseData(
                    new ProjectParticipationStatusInternal?[] { ProjectParticipationStatusInternal.Candidate, ProjectParticipationStatusInternal.Refusal },
                    ProjectParticipationStatusInternal.Pending);
                yield return new TestCaseData(
                    new ProjectParticipationStatusInternal?[] { null, ProjectParticipationStatusInternal.Refusal },
                    ProjectParticipationStatusInternal.Pending);
                yield return new TestCaseData(
                    new ProjectParticipationStatusInternal?[] { null, null },
                    ProjectParticipationStatusInternal.Pending);
            }
        }

        [Test]
        [TestCaseSource(nameof(s_participationStatusInternalTestData))]
        public void EvaluateNewParticipationStatusInternal_Should_Return_ExpectedStatus(
            IEnumerable<ProjectParticipationStatusInternal?> childrenStatus,
            ProjectParticipationStatusInternal expectedResult)
        {
            // Act
            ProjectParticipationStatusInternal result = ProjectParticipationStatusInheritanceEvaluator
                .EvaluateNewParticipationStatusInternal(childrenStatus);

            // Assert
            _ = result.Should().Be(expectedResult);
        }

        private static IEnumerable<TestCaseData> s_invitationStatusTestData
        {
            get
            {
                yield return new TestCaseData(
                    new ProjectInvitationStatus?[] { ProjectInvitationStatus.Invited, ProjectInvitationStatus.Candidate },
                    ProjectInvitationStatus.Invited);
                yield return new TestCaseData(
                    new ProjectInvitationStatus?[] { ProjectInvitationStatus.Invited, ProjectInvitationStatus.Invited },
                    ProjectInvitationStatus.Invited);
                yield return new TestCaseData(
                    new ProjectInvitationStatus?[] { ProjectInvitationStatus.Candidate, ProjectInvitationStatus.Candidate },
                    ProjectInvitationStatus.Candidate);
                yield return new TestCaseData(
                    new ProjectInvitationStatus?[] { ProjectInvitationStatus.NotInvited, ProjectInvitationStatus.NotInvited },
                    ProjectInvitationStatus.NotInvited);
                yield return new TestCaseData(
                    new ProjectInvitationStatus?[] { ProjectInvitationStatus.Unclear, ProjectInvitationStatus.Unclear },
                    ProjectInvitationStatus.Unclear);
                yield return new TestCaseData(
                    new ProjectInvitationStatus?[] { ProjectInvitationStatus.Candidate, ProjectInvitationStatus.NotInvited },
                    ProjectInvitationStatus.Unclear);
                yield return new TestCaseData(
                    new ProjectInvitationStatus?[] { ProjectInvitationStatus.Candidate, null },
                    ProjectInvitationStatus.Unclear);
                yield return new TestCaseData(
                    new ProjectInvitationStatus?[] { null, null },
                    ProjectInvitationStatus.Unclear);
            }
        }

        [Test]
        [TestCaseSource(nameof(s_invitationStatusTestData))]
        public void EvaluateNewInvitationStatus_Should_Return_ExpectedStatus(
            IEnumerable<ProjectInvitationStatus?> childrenStatus,
            ProjectInvitationStatus expectedResult)
        {
            // Act
            ProjectInvitationStatus result = ProjectParticipationStatusInheritanceEvaluator
                .EvaluateNewInvitationStatus(childrenStatus);

            // Assert
            _ = result.Should().Be(expectedResult);
        }
    }
}

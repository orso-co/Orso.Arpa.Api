using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.ProjectApplication.Model;
using Orso.Arpa.Domain.ProjectDomain.Commands;
using Orso.Arpa.Domain.ProjectDomain.Enums;
using Orso.Arpa.Domain.ProjectDomain.Model;

namespace Orso.Arpa.Application.Tests.ProjectApplication
{
    [TestFixture]
    public class ProjectParticipationTests
    {
        private static IEnumerable<TestCaseData> s_testData
        {
            get
            {
                yield return new TestCaseData(null, null, ProjectParticipationStatusResult.Pending);
                yield return new TestCaseData(ProjectParticipationStatusInner.RehearsalsOnly, null, ProjectParticipationStatusResult.Pending);
                yield return new TestCaseData(null, ProjectParticipationStatusInternal.Candidate, ProjectParticipationStatusResult.Pending);
                yield return new TestCaseData(ProjectParticipationStatusInner.Interested, ProjectParticipationStatusInternal.Candidate, ProjectParticipationStatusResult.Pending);
                yield return new TestCaseData(ProjectParticipationStatusInner.Acceptance, ProjectParticipationStatusInternal.Refusal, ProjectParticipationStatusResult.Refusal);
                yield return new TestCaseData(ProjectParticipationStatusInner.Refusal, ProjectParticipationStatusInternal.Acceptance, ProjectParticipationStatusResult.Refusal);
                yield return new TestCaseData(ProjectParticipationStatusInner.Acceptance, ProjectParticipationStatusInternal.Acceptance, ProjectParticipationStatusResult.Acceptance);
                yield return new TestCaseData(ProjectParticipationStatusInner.Acceptance, ProjectParticipationStatusInternal.Candidate, ProjectParticipationStatusResult.Pending);
                yield return new TestCaseData(ProjectParticipationStatusInner.RehearsalsOnly, ProjectParticipationStatusInternal.Acceptance, ProjectParticipationStatusResult.Pending);
            }
        }

        [Test]
        [TestCaseSource(nameof(s_testData))]
        public void Should_Map_To_ProjectParticipationStatusResult(
            ProjectParticipationStatusInner? projectParticipationStatusInner,
            ProjectParticipationStatusInternal? projectParticipationStatusInternal,
            ProjectParticipationStatusResult expectedResult)
        {
            // Arrange
            var projectParticipation = new ProjectParticipation(new SetProjectParticipation.Command
            {
                ParticipationStatusInner = projectParticipationStatusInner,
                ParticipationStatusInternal = projectParticipationStatusInternal
            });

            _ = new ProjectParticipationDto();

            // Assert
            _ = projectParticipation.ParticipationStatusResult.Should().Be(expectedResult);
        }
    }
}

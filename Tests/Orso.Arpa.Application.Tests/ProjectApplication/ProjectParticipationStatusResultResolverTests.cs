using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.ProjectApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Domain.Logic.ProjectParticipations;

namespace Orso.Arpa.Application.Tests.ProjectApplication
{
    [TestFixture]
    public class ProjectParticipationStatusResultResolverTests
    {
        private ProjectParticipationStatusResultResolver CreateProjectParticipationStatusResultResolver()
        {
            return new ProjectParticipationStatusResultResolver();
        }

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
            ProjectParticipationStatusResultResolver projectParticipationStatusResultResolver = CreateProjectParticipationStatusResultResolver();
            var source = new ProjectParticipation(new SetProjectParticipation.Command
            {
                ParticipationStatusInner = projectParticipationStatusInner,
                ParticipationStatusInternal = projectParticipationStatusInternal
            });
            IHasProjectParticipationStatusDto destination = new ProjectParticipationDto();

            // Act
            ProjectParticipationStatusResult result = projectParticipationStatusResultResolver.Resolve(
                source,
                destination,
                default,
                null);

            // Assert
            _ = result.Should().Be(expectedResult);
        }
    }
}

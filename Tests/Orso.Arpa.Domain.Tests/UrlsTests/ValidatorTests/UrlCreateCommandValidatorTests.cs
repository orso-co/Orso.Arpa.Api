using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;
using static Orso.Arpa.Domain.Logic.Urls.Create;

namespace Orso.Arpa.Domain.Tests.UrlTests.ValidatorTests
{
    [TestFixture]
    public class UrlCreateCommandValidatorTests
    {
        private IArpaContext _arpaContext;
        private Validator _validator;
        private DbSet<Project> _mockProjectDbSet;

        [SetUp]
        public void SetUp()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _mockProjectDbSet = MockDbSets.Projects;

            _arpaContext.Projects.Returns(_mockProjectDbSet);
            _validator = new Validator(_arpaContext);
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_ProjecId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Project>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(c => c.ProjectId, ProjectSeedData.RockingXMas.Id);
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Not_Existing_ProjectId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Url>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            await _validator.ShouldHaveNotFoundErrorForAsync(c => c.ProjectId, Guid.NewGuid(), nameof(Project));
        }
    }
}

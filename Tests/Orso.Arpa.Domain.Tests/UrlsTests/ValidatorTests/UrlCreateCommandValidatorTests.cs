using System;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
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
        public void Should_Not_Have_Validation_Error_If_Valid_ProjecId_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(c => c.Id, ProjectSeedData.RockingXMas.Id);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_ProjectId_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorFor(c => c.Id, Guid.NewGuid());
        }
    }
}

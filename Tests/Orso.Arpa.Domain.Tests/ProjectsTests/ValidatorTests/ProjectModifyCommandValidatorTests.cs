using System;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;
using static Orso.Arpa.Domain.Logic.Projects.Modify;

namespace Orso.Arpa.Domain.Tests.ProjectTests.ValidatorTests
{
    [TestFixture]
    public class ProjectModifyCommandValidatorTests
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
            _arpaContext.Set<Project>().Returns(_mockProjectDbSet);

            _validator = new Validator(_arpaContext);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Duplicate_Number()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.Number, new Command() {
                Id = ProjectSeedData.RockingXMas.Id,
                Number = ProjectSeedData.HoorayForHollywood.Number
            });
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Unused_Number()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.Number, new Command()
            {
                Id = ProjectSeedData.RockingXMas.Id,
                Number = "New Number"
            });
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Own_Number()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.Number, new Command()
            {
                Id = ProjectSeedData.RockingXMas.Id,
                Number = ProjectSeedData.RockingXMas.Number
            });
        }

        [Test]
        public void Should_Have_Validation_Error_If_Id_Does_Not_Exist()
        {
            _validator.ShouldHaveValidationErrorFor(c => c.Id, new Command()
            {
                Id = Guid.NewGuid(),
                Number = "some number"
            });
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Id_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.Id, new Command()
            {
                Id = ProjectSeedData.RockingXMas.Id,
                Number = "some number"
            });
        }
    }
}

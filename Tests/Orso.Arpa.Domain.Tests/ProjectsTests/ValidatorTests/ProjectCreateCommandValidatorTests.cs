using System;
using System.Threading;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Tests.Extensions;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;
using static Orso.Arpa.Domain.Logic.Projects.Create;

namespace Orso.Arpa.Domain.Tests.ProjectTests.ValidatorTests
{
    [TestFixture]
    public class ProjectCreateCommandValidatorTests
    {
        private IArpaContext _arpaContext;
        private Validator _validator;
        private DbSet<Project> _mockProjectDbSet;
        private DbSet<SelectValueCategory> _mockSelectValueCategoryDbSet;

        [SetUp]
        public void SetUp()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _validator = new Validator(_arpaContext);
            _mockProjectDbSet = MockDbSets.Projects;
            _mockSelectValueCategoryDbSet = MockDbSets.SelectValueCategories;
            _arpaContext.SelectValueCategories.Returns(_mockSelectValueCategoryDbSet);
            _arpaContext.Projects.Returns(_mockProjectDbSet);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Duplicate_Code_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.Code, new Command()
            {
                Code = ProjectSeedData.HoorayForHollywood.Code
            });
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Unused_Code_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.Code, new Command()
            {
                Code = "New Code"
            });
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_ParentId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Project>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _validator.ShouldNotHaveValidationErrorFor(command => command.ParentId, new Command()
            {
                Code = "New Code",
                ParentId = ProjectSeedData.RockingXMas.Id
            });
        }

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_ParentId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Project>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _validator.ShouldThrowNotFoundExceptionFor(command => command.ParentId, new Command()
            {
                Code = "New Code",
                ParentId = Guid.NewGuid()
            });
        }

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_StateId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _validator.ShouldHaveValidationErrorFor(command => command.StateId, new Command()
            {
                Code = "New Code",
                StateId = SelectValueMappingSeedData.AddressTypeMappings[0].Id
            });
        }

        [Test]
        public void Should_Have_Validation_Error_If_Not_Existing_StateId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _validator.ShouldThrowNotFoundExceptionFor(command => command.StateId, new Command()
            {
                Code = "New Code",
                StateId = Guid.NewGuid()
            });
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Empty_StateId_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.StateId, new Command()
            {
                Code = "New Code",
                StateId = null
            });
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_StateId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _validator.ShouldNotHaveValidationErrorFor(command => command.StateId, new Command()
            {
                Code = "New Code",
                StateId = SelectValueMappingSeedData.ProjectStateMappings[0].Id
            });
        }

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_GenreId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _validator.ShouldHaveValidationErrorFor(command => command.GenreId, new Command()
            {
                Code = "New Code",
                GenreId = SelectValueMappingSeedData.AddressTypeMappings[0].Id
            });
        }

        [Test]
        public void Should_Have_Validation_Error_If_Not_Existing_GenreId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _validator.ShouldThrowNotFoundExceptionFor(command => command.GenreId, new Command()
            {
                Code = "New Code",
                GenreId = Guid.NewGuid()
            });
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Empty_GenreId_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.GenreId, new Command()
            {
                Code = "New Code",
                GenreId = null
            });
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_GenreId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _validator.ShouldNotHaveValidationErrorFor(command => command.GenreId, new Command()
            {
                Code = "New Code",
                GenreId = SelectValueMappingSeedData.ProjectGenreMappings[0].Id
            });
        }

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_TypeId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _validator.ShouldHaveValidationErrorFor(command => command.TypeId, new Command()
            {
                Code = "New Code",
                TypeId = SelectValueMappingSeedData.AddressTypeMappings[0].Id
            });
        }

        [Test]
        public void Should_Have_Validation_Error_If_Not_Existing_TypeId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _validator.ShouldThrowNotFoundExceptionFor(command => command.TypeId, new Command()
            {
                Code = "New Code",
                TypeId = Guid.NewGuid()
            });
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Empty_TypeId_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.TypeId, new Command()
            {
                Code = "New Code",
                TypeId = null
            });
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_TypeId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _validator.ShouldNotHaveValidationErrorFor(command => command.TypeId, new Command()
            {
                Code = "New Code",
                TypeId = SelectValueMappingSeedData.ProjectTypeMappings[0].Id
            });
        }
    }
}

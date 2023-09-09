using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.ProjectDomain.Commands;
using Orso.Arpa.Domain.ProjectDomain.Model;
using Orso.Arpa.Domain.SelectValueDomain.Model;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.ProjectTests.ValidatorTests
{
    [TestFixture]
    public class ProjectModifyCommandValidatorTests
    {
        private IArpaContext _arpaContext;
        private ModifyProject.Validator _validator;
        private DbSet<Project> _mockProjectDbSet;
        private DbSet<SelectValueCategory> _mockSelectValueCategoryDbSet;

        [SetUp]
        public void SetUp()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _mockProjectDbSet = MockDbSets.Projects;
            _mockSelectValueCategoryDbSet = MockDbSets.SelectValueCategories;
            _arpaContext.SelectValueCategories.Returns(_mockSelectValueCategoryDbSet);
            _arpaContext.Projects.Returns(_mockProjectDbSet);

            _validator = new ModifyProject.Validator(_arpaContext);
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Duplicate_Code()
        {
            _arpaContext.EntityExistsAsync<Project>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldHaveValidationErrorForExactAsync(command => command.Code, new ModifyProject.Command()
            {
                Id = ProjectSeedData.RockingXMas.Id,
                Code = ProjectSeedData.HoorayForHollywood.Code
            });
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Unused_Code()
        {
            _arpaContext.EntityExistsAsync<Project>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.Code, new ModifyProject.Command()
            {
                Id = ProjectSeedData.RockingXMas.Id,
                Code = "New Code"
            });
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Own_Code()
        {
            _arpaContext.EntityExistsAsync<Project>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.Code, new ModifyProject.Command()
            {
                Id = ProjectSeedData.RockingXMas.Id,
                Code = ProjectSeedData.RockingXMas.Code
            });
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Id_Does_Not_Exist()
        {
            _ = _arpaContext.EntityExistsAsync<Project>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            await _validator.ShouldHaveNotFoundErrorFor(c => c.Id, new ModifyProject.Command()
            {
                Id = Guid.NewGuid(),
                Code = "some code"
            }, nameof(Project));
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_Id_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Project>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.Id, new ModifyProject.Command()
            {
                Id = ProjectSeedData.RockingXMas.Id,
                Code = "some code"
            });
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_ParentId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Project>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.ParentId, new ModifyProject.Command()
            {
                Id = ProjectSeedData.HoorayForHollywood.Id,
                Code = "New Code",
                ParentId = ProjectSeedData.RockingXMas.Id
            });
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Invalid_ParentId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Project>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            await _validator.ShouldHaveNotFoundErrorFor(command => command.ParentId, new ModifyProject.Command()
            {
                Id = ProjectSeedData.RockingXMas.Id,
                Code = "New Code",
                ParentId = Guid.NewGuid()
            }, nameof(Project));
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Invalid_GenreId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Project>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldHaveValidationErrorForExactAsync(command => command.GenreId, new ModifyProject.Command()
            {
                Code = "New Code",
                GenreId = SelectValueMappingSeedData.AddressTypeMappings[0].Id
            });
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Not_Existing_GenreId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Project>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            await _validator.ShouldHaveNotFoundErrorFor(command => command.GenreId, new ModifyProject.Command()
            {
                Code = "New Code",
                GenreId = Guid.NewGuid()
            }, nameof(SelectValueMapping));
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Empty_GenreId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Project>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.GenreId, new ModifyProject.Command()
            {
                Code = "New Code",
                GenreId = null
            });
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_GenreId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Project>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.GenreId, new ModifyProject.Command()
            {
                Code = "New Code",
                GenreId = SelectValueMappingSeedData.ProjectGenreMappings[0].Id
            });
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Invalid_TypeId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Project>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldHaveValidationErrorForExactAsync(command => command.TypeId, new ModifyProject.Command()
            {
                Code = "New Code",
                TypeId = SelectValueMappingSeedData.AddressTypeMappings[0].Id
            });
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Not_Existing_TypeId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Project>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            await _validator.ShouldHaveNotFoundErrorFor(command => command.TypeId, new ModifyProject.Command()
            {
                Code = "New Code",
                TypeId = Guid.NewGuid()
            }, nameof(SelectValueMapping));
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Empty_TypeId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Project>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.TypeId, new ModifyProject.Command()
            {
                Code = "New Code",
                TypeId = null
            });
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_TypeId_Is_Supplied()
        {
            _ = _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.EntityExistsAsync<Project>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.TypeId, new ModifyProject.Command()
            {
                Code = "New Code",
                TypeId = SelectValueMappingSeedData.ProjectTypeMappings[0].Id
            });
        }
    }
}

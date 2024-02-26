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
    public class ProjectCreateCommandValidatorTests
    {
        private IArpaContext _arpaContext;
        private CreateProject.Validator _validator;
        private DbSet<Project> _mockProjectDbSet;
        private DbSet<SelectValueCategory> _mockSelectValueCategoryDbSet;

        [SetUp]
        public void SetUp()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _validator = new CreateProject.Validator(_arpaContext);
            _mockProjectDbSet = MockDbSets.Projects;
            _mockSelectValueCategoryDbSet = MockDbSets.SelectValueCategories;
            _ = _arpaContext.Set<SelectValueCategory>().Returns(_mockSelectValueCategoryDbSet);
            _ = _arpaContext.Set<Project>().Returns(_mockProjectDbSet);
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Duplicate_Code_Is_Supplied()
        {
            _ = await _validator.ShouldHaveValidationErrorForExactAsync(command => command.Code, new CreateProject.Command()
            {
                Code = ProjectSeedData.HoorayForHollywood.Code
            });
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Unused_Code_Is_Supplied()
        {
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.Code, new CreateProject.Command()
            {
                Code = "New Code"
            });
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_ParentId_Is_Supplied()
        {
            _ = _arpaContext.EntityExistsAsync<Project>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.ParentId, new CreateProject.Command()
            {
                Code = "New Code",
                ParentId = ProjectSeedData.RockingXMas.Id
            });
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Invalid_ParentId_Is_Supplied()
        {
            _ = _arpaContext.EntityExistsAsync<Project>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            await _validator.ShouldHaveNotFoundErrorFor(command => command.ParentId, new CreateProject.Command()
            {
                Code = "New Code",
                ParentId = Guid.NewGuid()
            }, nameof(Project));
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Invalid_GenreId_Is_Supplied()
        {
            _ = _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = await _validator.ShouldHaveValidationErrorForExactAsync(command => command.GenreId, new CreateProject.Command()
            {
                Code = "New Code",
                GenreId = SelectValueMappingSeedData.PersonGenderMappings[0].Id
            });
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Not_Existing_GenreId_Is_Supplied()
        {
            _ = _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            await _validator.ShouldHaveNotFoundErrorFor(command => command.GenreId, new CreateProject.Command()
            {
                Code = "New Code",
                GenreId = Guid.NewGuid()
            }, nameof(SelectValueMapping));
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Empty_GenreId_Is_Supplied()
        {
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.GenreId, new CreateProject.Command()
            {
                Code = "New Code",
                GenreId = null
            });
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_GenreId_Is_Supplied()
        {
            _ = _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.GenreId, new CreateProject.Command()
            {
                Code = "New Code",
                GenreId = SelectValueMappingSeedData.ProjectGenreMappings[0].Id
            });
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Invalid_TypeId_Is_Supplied()
        {
            _ = _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = await _validator.ShouldHaveValidationErrorForExactAsync(command => command.TypeId, new CreateProject.Command()
            {
                Code = "New Code",
                TypeId = SelectValueMappingSeedData.PersonGenderMappings[0].Id
            });
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Not_Existing_TypeId_Is_Supplied()
        {
            _ = _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            await _validator.ShouldHaveNotFoundErrorFor(command => command.TypeId, new CreateProject.Command()
            {
                Code = "New Code",
                TypeId = Guid.NewGuid()
            }, nameof(SelectValueMapping));
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Empty_TypeId_Is_Supplied()
        {
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.TypeId, new CreateProject.Command()
            {
                Code = "New Code",
                TypeId = null
            });
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_TypeId_Is_Supplied()
        {
            _ = _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.TypeId, new CreateProject.Command()
            {
                Code = "New Code",
                TypeId = SelectValueMappingSeedData.ProjectTypeMappings[0].Id
            });
        }
    }
}

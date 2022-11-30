using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;
using static Orso.Arpa.Domain.Logic.Appointments.Modify;

namespace Orso.Arpa.Domain.Tests.AppointmentTests.ValidatorTests
{
    [TestFixture]
    public class AppointmentModifyCommandValidatorTests
    {
        private IArpaContext _arpaContext;
        private Validator _validator;
        private DbSet<SelectValueCategory> _mockSelectValueCategoryDbSet;

        [SetUp]
        public void SetUp()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _mockSelectValueCategoryDbSet = MockDbSets.SelectValueCategories;
            _ = _arpaContext.SelectValueCategories.Returns(_mockSelectValueCategoryDbSet);
            _validator = new Validator(_arpaContext);
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Id_Does_Not_Exist()
        {
            _ = _arpaContext.EntityExistsAsync<Appointment>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            await _validator.ShouldHaveNotFoundErrorForAsync(c => c.Id, Guid.NewGuid(), nameof(Appointment));
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_Id_Is_Supplied()
        {
            _ = _arpaContext.EntityExistsAsync<Appointment>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.Id, AppointmentSeedData.RockingXMasRehearsal.Id);
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_SalaryId_Does_Not_Exist()
        {
            _ = _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _ = _arpaContext.EntityExistsAsync<Appointment>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldHaveNotFoundErrorForAsync(c => c.SalaryId, Guid.NewGuid(), nameof(SelectValueMapping));
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Invalid_SalaryId_Is_Supplied()
        {
            _ = _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.EntityExistsAsync<Appointment>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = await _validator.ShouldHaveValidationErrorForExactAsync(c => c.SalaryId, SelectValueMappingSeedData.AddressTypeMappings[0].Id);
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_SalaryId_Is_Supplied()
        {
            _ = _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.EntityExistsAsync<Appointment>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(c => c.SalaryId, SelectValueMappingSeedData.AppointmentSalaryMappings[0].Id);
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Empty_SalaryId_Is_Supplied()
        {
            _ = _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.EntityExistsAsync<Appointment>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(c => c.SalaryId, (Guid?)null);
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_SalaryPatternId_Does_Not_Exist()
        {
            _ = _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _ = _arpaContext.EntityExistsAsync<Appointment>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldHaveNotFoundErrorForAsync(c => c.SalaryPatternId, Guid.NewGuid(), nameof(SelectValueMapping));
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Invalid_SalaryPatternId_Is_Supplied()
        {
            _ = _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.EntityExistsAsync<Appointment>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = await _validator.ShouldHaveValidationErrorForExactAsync(c => c.SalaryPatternId, SelectValueMappingSeedData.AddressTypeMappings[0].Id);
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_SalaryPatternId_Is_Supplied()
        {
            _ = _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.EntityExistsAsync<Appointment>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(c => c.SalaryPatternId, SelectValueMappingSeedData.AppointmentSalaryPatternMappings[0].Id);
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Empty_SalaryPatternId_Is_Supplied()
        {
            _ = _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.EntityExistsAsync<Appointment>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(c => c.SalaryPatternId, (Guid?)null);
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_ExpectationId_Does_Not_Exist()
        {
            _ = _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _ = _arpaContext.EntityExistsAsync<Appointment>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldHaveNotFoundErrorForAsync(c => c.ExpectationId, Guid.NewGuid(), nameof(SelectValueMapping));
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Invalid_ExpectationId_Is_Supplied()
        {
            _ = _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.EntityExistsAsync<Appointment>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = await _validator.ShouldHaveValidationErrorForExactAsync(c => c.ExpectationId, SelectValueMappingSeedData.AddressTypeMappings[0].Id);
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_ExpectationId_Is_Supplied()
        {
            _ = _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.EntityExistsAsync<Appointment>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(c => c.ExpectationId, SelectValueMappingSeedData.AppointmentExpectationMappings[0].Id);
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Empty_ExpectationId_Is_Supplied()
        {
            _ = _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.EntityExistsAsync<Appointment>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(c => c.ExpectationId, (Guid?)null);
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_CategoryId_Does_Not_Exist()
        {
            _ = _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _ = _arpaContext.EntityExistsAsync<Appointment>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldHaveNotFoundErrorForAsync(c => c.CategoryId, Guid.NewGuid(), nameof(SelectValueMapping));
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Invalid_CategoryId_Is_Supplied()
        {
            _ = _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.EntityExistsAsync<Appointment>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = await _validator.ShouldHaveValidationErrorForExactAsync(c => c.CategoryId, SelectValueMappingSeedData.AddressTypeMappings[0].Id);
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_CategoryId_Is_Supplied()
        {
            _ = _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.EntityExistsAsync<Appointment>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(c => c.CategoryId, SelectValueMappingSeedData.AppointmentCategoryMappings[0].Id);
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Empty_CategoryId_Is_Supplied()
        {
            _ = _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.EntityExistsAsync<Appointment>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(c => c.CategoryId, (Guid?)null);
        }
    }
}

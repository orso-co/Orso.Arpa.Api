using System;
using System.Threading;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.FakeData;
using static Orso.Arpa.Domain.Logic.Appointments.Create;

namespace Orso.Arpa.Domain.Tests.AppointmentTests.ValidatorTests
{
    [TestFixture]
    public class AppointmentCreateCommandValidatorTests
    {
        private IArpaContext _arpaContext;
        private Validator _validator;
        private DbSet<SelectValueCategory> _mockSelectValueCategoryDbSet;

        [SetUp]
        public void SetUp()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _validator = new Validator(_arpaContext);
            _mockSelectValueCategoryDbSet = MockDbSets.SelectValueCategories;
            _arpaContext.SelectValueCategories.Returns(_mockSelectValueCategoryDbSet);
        }

        [Test]
        public void Should_Have_Validation_Error_If_SalaryId_Does_Not_Exist()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _validator.ShouldHaveValidationErrorFor(c => c.SalaryId, Guid.NewGuid());
        }

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_SalaryId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _validator.ShouldHaveValidationErrorFor(c => c.SalaryId, SelectValueMappingSeedData.AddressTypeMappings[0].Id);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_SalaryId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _validator.ShouldNotHaveValidationErrorFor(c => c.SalaryId, SelectValueMappingSeedData.AppointmentSalaryMappings[0].Id);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Empty_SalaryId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _validator.ShouldNotHaveValidationErrorFor(c => c.SalaryId, (Guid?)null);
        }

        [Test]
        public void Should_Have_Validation_Error_If_SalaryPatternId_Does_Not_Exist()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _validator.ShouldHaveValidationErrorFor(c => c.SalaryPatternId, Guid.NewGuid());
        }

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_SalaryPatternId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _validator.ShouldHaveValidationErrorFor(c => c.SalaryPatternId, SelectValueMappingSeedData.AddressTypeMappings[0].Id);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_SalaryPatternId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _validator.ShouldNotHaveValidationErrorFor(c => c.SalaryPatternId, SelectValueMappingSeedData.AppointmentSalaryPatternMappings[0].Id);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Empty_SalaryPatternId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _validator.ShouldNotHaveValidationErrorFor(c => c.SalaryPatternId, (Guid?)null);
        }

        [Test]
        public void Should_Have_Validation_Error_If_ExpectationId_Does_Not_Exist()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _validator.ShouldHaveValidationErrorFor(c => c.ExpectationId, Guid.NewGuid());
        }

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_ExpectationId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _validator.ShouldHaveValidationErrorFor(c => c.ExpectationId, SelectValueMappingSeedData.AddressTypeMappings[0].Id);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_ExpectationId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _validator.ShouldNotHaveValidationErrorFor(c => c.ExpectationId, SelectValueMappingSeedData.AppointmentExpectationMappings[0].Id);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Empty_ExpectationId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _validator.ShouldNotHaveValidationErrorFor(c => c.ExpectationId, (Guid?)null);
        }

        [Test]
        public void Should_Have_Validation_Error_If_StatusId_Does_Not_Exist()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _validator.ShouldHaveValidationErrorFor(c => c.StatusId, Guid.NewGuid());
        }

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_StatusId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _validator.ShouldHaveValidationErrorFor(c => c.StatusId, SelectValueMappingSeedData.AddressTypeMappings[0].Id);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_StatusId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _validator.ShouldNotHaveValidationErrorFor(c => c.StatusId, SelectValueMappingSeedData.AppointmentStatusMappings[0].Id);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Empty_StatusId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _validator.ShouldNotHaveValidationErrorFor(c => c.StatusId, (Guid?)null);
        }

        [Test]
        public void Should_Have_Validation_Error_If_CategoryId_Does_Not_Exist()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _validator.ShouldHaveValidationErrorFor(c => c.CategoryId, Guid.NewGuid());
        }

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_CategoryId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _validator.ShouldHaveValidationErrorFor(c => c.CategoryId, SelectValueMappingSeedData.AddressTypeMappings[0].Id);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_CategoryId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _validator.ShouldNotHaveValidationErrorFor(c => c.CategoryId, SelectValueMappingSeedData.AppointmentCategoryMappings[0].Id);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Empty_CategoryId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _validator.ShouldNotHaveValidationErrorFor(c => c.CategoryId, (Guid?)null);
        }
    }
}

using System;
using System.Linq;
using FluentAssertions;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using MockQueryable.NSubstitute;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Application.Validation;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Regions.Seed;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class RegionModifyDtoValidatorTests
    {
        private RegionModifyDtoValidator _validator;
        private IReadOnlyRepository _repository;

        [SetUp]
        public void Setup()
        {
            _repository = Substitute.For<IReadOnlyRepository>();
            _validator = new RegionModifyDtoValidator(_repository);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_Id_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.Id, Guid.Empty);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Id_Does_Not_Exist()
        {
            IQueryable<Region> regionsMock = RegionSeedData.Regions.AsQueryable().BuildMock();
            _repository.GetAll<Region>().Returns(regionsMock);
            _repository.GetByIdAsync<Region>(Arg.Any<Guid>()).Returns(default(Region));

            Func<ValidationResult> func = () => _validator.Validate(new Dtos.RegionModifyDto { Id = Guid.NewGuid(), Name = "Name" });

            func.Should().Throw<RestException>();
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_Name_Is_Supplied([Values(null, "")] string name)
        {
            IQueryable<Region> regionsMock = RegionSeedData.Regions.AsQueryable().BuildMock();
            _repository.GetAll<Region>().Returns(regionsMock);

            _validator.ShouldHaveValidationErrorFor(command => command.Name, name);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Name_Does_Already_Exist()
        {
            IQueryable<Region> regionsMock = RegionSeedData.Regions.AsQueryable().BuildMock();
            _repository.GetAll<Region>().Returns(regionsMock);

            _validator.ShouldHaveValidationErrorFor(command => command.Name, RegionSeedData.Stuttgart.Name);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Name_Is_Supplied()
        {
            IQueryable<Region> regionsMock = RegionSeedData.Regions.AsQueryable().BuildMock();
            _repository.GetAll<Region>().Returns(regionsMock);

            _validator.ShouldNotHaveValidationErrorFor(command => command.Name, "Honolulu");
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Id_Is_Supplied()
        {
            _repository.GetByIdAsync<Region>(Arg.Any<Guid>()).Returns(RegionSeedData.Berlin);

            _validator.ShouldNotHaveValidationErrorFor(command => command.Id, Guid.NewGuid());
        }
    }
}

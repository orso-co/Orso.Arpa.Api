using System;
using System.Linq;
using FluentAssertions;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using MockQueryable.NSubstitute;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Persistence.Seed;
using static Orso.Arpa.Domain.Logic.Regions.Modify;

namespace Orso.Arpa.Domain.Tests.RegionTests.ValidatorTests
{
    [TestFixture]
    public class RegionModifyCommandValidatorTests
    {
        private Validator _validator;
        private IReadOnlyRepository _repository;

        [SetUp]
        public void Setup()
        {
            _repository = Substitute.For<IReadOnlyRepository>();
            _validator = new Validator(_repository);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Id_Does_Not_Exist()
        {
            IQueryable<Region> regionsMock = RegionSeedData.Regions.AsQueryable().BuildMock();
            _repository.GetAll<Region>().Returns(regionsMock);
            _repository.GetByIdAsync<Region>(Arg.Any<Guid>()).Returns(default(Region));

            Func<ValidationResult> func = () => _validator.Validate(new Command { Id = Guid.NewGuid(), Name = "Name" });

            func.Should().Throw<RestException>();
        }

        [Test]
        public void Should_Have_Validation_Error_If_Name_Does_Already_Exist()
        {
            IQueryable<Region> regionsMock = RegionSeedData.Regions.AsQueryable().BuildMock();
            _repository.GetAll<Region>().Returns(regionsMock);
            _repository.GetByIdAsync<Region>(Arg.Any<Guid>()).Returns(RegionSeedData.Berlin);
            _validator.ShouldHaveValidationErrorFor(command => command.Name, RegionSeedData.Stuttgart.Name);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Name_Is_Supplied()
        {
            IQueryable<Region> regionsMock = RegionSeedData.Regions.AsQueryable().BuildMock();
            _repository.GetAll<Region>().Returns(regionsMock);
            _repository.GetByIdAsync<Region>(Arg.Any<Guid>()).Returns(RegionSeedData.Berlin);
            _validator.ShouldNotHaveValidationErrorFor(command => command.Name, "Honolulu");
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Id_Is_Supplied()
        {
            _repository.GetByIdAsync<Region>(Arg.Any<Guid>()).Returns(RegionSeedData.Berlin);
            IQueryable<Region> regionsMock = RegionSeedData.Regions.AsQueryable().BuildMock();
            _repository.GetAll<Region>().Returns(regionsMock);
            _validator.ShouldNotHaveValidationErrorFor(command => command.Id, Guid.NewGuid());
        }
    }
}

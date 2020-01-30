using System.Linq;
using FluentValidation.TestHelper;
using MockQueryable.NSubstitute;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Regions.Seed;
using static Orso.Arpa.Domain.Regions.Create;

namespace Orso.Arpa.Domain.Tests.RegionTests.ValidatorTests
{
    [TestFixture]
    public class RegionCreateCommandValidatorTests
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
    }
}

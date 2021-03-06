using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.FakeData;
using static Orso.Arpa.Domain.Logic.Regions.Create;

namespace Orso.Arpa.Domain.Tests.RegionTests.ValidatorTests
{
    [TestFixture]
    public class RegionCreateCommandValidatorTests
    {
        private Validator _validator;
        private IArpaContext _arpaContext;

        [SetUp]
        public void Setup()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _validator = new Validator(_arpaContext);
            DbSet<Region> mockRegions = MockDbSets.Regions;
            _arpaContext.Regions.Returns(mockRegions);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Name_Does_Already_Exist()
        {
            _validator.ShouldHaveValidationErrorForExact(command => command.Name, new Command { Name = RegionSeedData.Stuttgart.Name });
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Name_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExact(command => command.Name, new Command { Name = "Honolulu" });
        }
    }
}

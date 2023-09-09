using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.RegionDomain.Commands;
using Orso.Arpa.Domain.RegionDomain.Model;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Domain.Tests.RegionTests.ValidatorTests
{
    [TestFixture]
    public class RegionCreateCommandValidatorTests
    {
        private CreateRegion.Validator _validator;
        private IArpaContext _arpaContext;

        [SetUp]
        public void Setup()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _validator = new CreateRegion.Validator(_arpaContext);
            DbSet<Region> mockRegions = MockDbSets.Regions;
            _arpaContext.Regions.Returns(mockRegions);
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Name_Does_Already_Exist()
        {
            await _validator.ShouldHaveValidationErrorForExactAsync(command => command.Name, new CreateRegion.Command { Name = RegionSeedData.StuttgartCity.Name });
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_Name_Is_Supplied()
        {
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.Name, new CreateRegion.Command { Name = "Honolulu" });
        }
    }
}

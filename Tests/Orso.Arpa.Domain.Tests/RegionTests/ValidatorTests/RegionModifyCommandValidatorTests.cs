using System;
using System.Threading;
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
    public class RegionModifyCommandValidatorTests
    {
        private ModifyRegion.Validator _validator;
        private IArpaContext _arpaContext;

        [SetUp]
        public void Setup()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _validator = new ModifyRegion.Validator(_arpaContext);
            DbSet<Region> mockRegions = MockDbSets.Regions;
            _arpaContext.Set<Region>().Returns(mockRegions);
            _arpaContext.Regions.Returns(mockRegions);
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Id_Does_Not_Exist()
        {
            await _validator.ShouldHaveNotFoundErrorFor(command => command.Id,
                new ModifyRegion.Command { Id = Guid.NewGuid(), Name = "Name" }, nameof(Region));
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Name_Does_Already_Exist()
        {
            _arpaContext.EntityExistsAsync<Region>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldHaveValidationErrorForExactAsync(command => command.Name,
                new ModifyRegion.Command { Id = RegionSeedData.Freiburg.Id, Name = RegionSeedData.StuttgartCity.Name });
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_Id_And_Name_Are_Supplied()
        {
            _arpaContext.EntityExistsAsync<Region>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.Name,
                new ModifyRegion.Command { Id = RegionSeedData.Freiburg.Id, Name = "Honolulu" });
        }
    }
}

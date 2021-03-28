using System;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Application;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.FakeData;
using static Orso.Arpa.Domain.Logic.Regions.Modify;

namespace Orso.Arpa.Domain.Tests.RegionTests.ValidatorTests
{
    [TestFixture]
    public class RegionModifyCommandValidatorTests
    {
        private Validator _validator;
        private IArpaContext _arpaContext;

        [SetUp]
        public void Setup()
        {
            IStringLocalizer<DomainResource>  localizer =
                new StringLocalizer<DomainResource> (
                    new ResourceManagerStringLocalizerFactory(
                        new OptionsWrapper<LocalizationOptions>(new LocalizationOptions()),
                        new LoggerFactory()));
            _arpaContext = Substitute.For<IArpaContext>();
            _validator = new Validator(_arpaContext, localizer);
            DbSet<Region> mockRegions = MockDbSets.Regions;
            _arpaContext.Set<Region>().Returns(mockRegions);
            _arpaContext.Regions.Returns(mockRegions);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Id_Does_Not_Exist()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.Id,
                new Command { Id = Guid.NewGuid(), Name = "Name" });
        }

        [Test]
        public void Should_Have_Validation_Error_If_Name_Does_Already_Exist()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.Name,
                new Command { Id = RegionSeedData.Freiburg.Id, Name = RegionSeedData.Stuttgart.Name });
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Id_And_Name_Are_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.Name,
                new Command { Id = RegionSeedData.Freiburg.Id, Name = "Honolulu" });
        }
    }
}

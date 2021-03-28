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
            IStringLocalizer<DomainResource>  localizer =
                new StringLocalizer<DomainResource> (
                    new ResourceManagerStringLocalizerFactory(
                        new OptionsWrapper<LocalizationOptions>(new LocalizationOptions()),
                        new LoggerFactory()));
            _arpaContext = Substitute.For<IArpaContext>();
            _validator = new Validator(_arpaContext, localizer);
            DbSet<Region> mockRegions = MockDbSets.Regions;
            _arpaContext.Regions.Returns(mockRegions);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Name_Does_Already_Exist()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.Name, new Command { Name = RegionSeedData.Stuttgart.Name });
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Name_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.Name, new Command { Name = "Honolulu" });
        }
    }
}

using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Application.AddressApplication.Model;
using Orso.Arpa.Application.General.MappingActions;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Application.RoomApplication.Model;
using Orso.Arpa.Application.SelectValueApplication.Model;
using Orso.Arpa.Application.VenueApplication.Model;
using Orso.Arpa.Domain.SelectValueDomain.Model;
using Orso.Arpa.Domain.VenueDomain.Model;
using Orso.Arpa.Infrastructure.Localization;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class VenueDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            _ = services.AddSingleton<LocalizeAction<SelectValueMapping, SelectValueDto>>();
            services.AddSingleton<LocalizeAction<RoomSection, RoomSectionDto>>();
            services.AddSingleton<LocalizeAction<RoomEquipment, RoomEquipmentDto>>();
            _ = services.AddSingleton(_localizerCache);
            _ = services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<VenueDtoMappingProfile>();
                cfg.AddProfile<AddressDtoMappingProfile>();
                cfg.AddProfile<RoomDtoMappingProfile>();
                cfg.AddProfile<BaseEntityDtoMappingProfile>();
                cfg.AddProfile<SelectValueDtoMappingProfile>();
                cfg.AddProfile<RoomSectionDtoMappingProfile>();
                cfg.AddProfile<RoomEquipmentDtoMappingProfile>();
            });

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            _mapper = serviceProvider.GetService<IMapper>();
        }

        private IMapper _mapper;

        private readonly ILocalizerCache _localizerCache = Substitute.For<ILocalizerCache>();


        [Test]
        public void Should_Map()
        {
            // Arrange
            Venue venue = FakeVenues.WeiherhofSchule;
            VenueDto expectedDto = VenueDtoData.WeiherhofSchule;

            // Act
            VenueDto dto = _mapper.Map<VenueDto>(venue);

            // Assert
            dto.Should().BeEquivalentTo(expectedDto);
        }
    }
}

using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Application.General.MappingActions;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Application.RoomApplication.Model;
using Orso.Arpa.Application.SectionApplication.Model;
using Orso.Arpa.Application.SelectValueApplication.Model;
using Orso.Arpa.Domain.SectionDomain.Model;
using Orso.Arpa.Domain.SelectValueDomain.Model;
using Orso.Arpa.Domain.VenueDomain.Model;
using Orso.Arpa.Infrastructure.Localization;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class RoomDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddSingleton<LocalizeAction<SelectValueMapping, SelectValueDto>>();
            services.AddSingleton<LocalizeAction<Section, SectionDto>>();
            services.AddSingleton<LocalizeAction<RoomSection, RoomSectionDto>>();
            services.AddSingleton<LocalizeAction<RoomEquipment, RoomEquipmentDto>>();
            services.AddSingleton(_localizerCache);
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<RoomDtoMappingProfile>();
                cfg.AddProfile<BaseEntityDtoMappingProfile>();
                cfg.AddProfile<SelectValueDtoMappingProfile>();
                cfg.AddProfile<RoomSectionDtoMappingProfile>();
                cfg.AddProfile<RoomEquipmentDtoMappingProfile>();
                cfg.AddProfile<SectionDtoMappingProfile>();
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
            Room room = FakeRooms.AulaWeiherhofSchule;
            RoomDto expectedDto = RoomDtoData.AulaWeiherhofSchule;

            // Act
            RoomDto dto = _mapper.Map<RoomDto>(room);

            // Assert
            dto.Should().BeEquivalentTo(expectedDto);
        }
    }
}

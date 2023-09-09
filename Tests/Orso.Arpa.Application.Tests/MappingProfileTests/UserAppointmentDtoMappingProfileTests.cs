using System.Linq;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Application.AddressApplication.Model;
using Orso.Arpa.Application.General.MappingActions;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Application.MeApplication.Model;
using Orso.Arpa.Application.ProjectApplication.Model;
using Orso.Arpa.Application.RoleApplication.Model;
using Orso.Arpa.Application.RoomApplication.Model;
using Orso.Arpa.Application.SelectValueApplication.Model;
using Orso.Arpa.Application.UrlApplication.Model;
using Orso.Arpa.Application.VenueApplication.Model;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.SelectValueDomain.Model;
using Orso.Arpa.Domain.UserDomain.Model;
using Orso.Arpa.Infrastructure.Localization;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class UserAppointmentDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            _ = services.AddSingleton<LocalizeAction<SelectValueMapping, SelectValueDto>>();
            _ = services.AddSingleton<LocalizeAction<Role, RoleDto>>();
            _ = services.AddSingleton(_localizerCache);
            _ = services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<MyAppointmentDtoMappingProfile>();
                cfg.AddProfile<BaseEntityDtoMappingProfile>();
                cfg.AddProfile<VenueDtoMappingProfile>();
                cfg.AddProfile<AddressDtoMappingProfile>();
                cfg.AddProfile<RoomDtoMappingProfile>();
                cfg.AddProfile<ProjectDtoMappingProfile>();
                cfg.AddProfile<UrlDtoMappingProfile>();
                cfg.AddProfile<RoleDtoMappingProfile>();
                cfg.AddProfile<SelectValueDtoMappingProfile>();
            });

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            _mapper = serviceProvider.GetService<IMapper>();
        }

        private IMapper _mapper;
        private readonly ILocalizerCache _localizerCache = Substitute.For<ILocalizerCache>();

        [Test]
        public void Should_Map_Appointment_To_MyAppointmentDto()
        {
            // Arrange
            Appointment appointment = FakeAppointments.RockingXMasRehearsal;
            _ = appointment.ProjectAppointments.First().Project.Urls.Remove(appointment.ProjectAppointments.First().Project.Urls.First());
            MyAppointmentDto expectedDto = UserAppointmentDtoTestData.RockingXMasDressRehearsal;

            // Act
            MyAppointmentDto dto = _mapper.Map<MyAppointmentDto>(appointment);

            // Assert
            _ = dto.Should().BeEquivalentTo(expectedDto, opt => opt
                .Excluding(dto => dto.Prediction)
                .Excluding(dto => dto.Result)
                .Excluding(dto => dto.CommentByPerformerInner));
        }
    }
}

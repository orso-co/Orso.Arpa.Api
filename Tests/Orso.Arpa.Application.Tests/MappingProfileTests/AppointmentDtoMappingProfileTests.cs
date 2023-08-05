using System.Collections.Generic;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Application.AppointmentApplication;
using Orso.Arpa.Application.General;
using Orso.Arpa.Application.ProjectApplication;
using Orso.Arpa.Application.RoleApplication;
using Orso.Arpa.Application.SelectValueApplication;
using Orso.Arpa.Application.UrlApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Roles;
using Orso.Arpa.Infrastructure.Localization;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class AppointmentDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddSingleton<RoleBasedSetNullAction<Appointment, AppointmentDto>>();
            services.AddSingleton<LocalizeAction<Appointment, AppointmentDto>>();
            services.AddSingleton<LocalizeAction<SelectValueMapping, SelectValueDto>>();
            services.AddSingleton<LocalizeAction<Role, RoleDto>>();
            services.AddSingleton(_tokenAccessor);
            services.AddSingleton(_localizerCache);
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<AppointmentDtoMappingProfile>();
                cfg.AddProfile<BaseEntityDtoMappingProfile>();
                cfg.AddProfile<ProjectDtoMappingProfile>();
                cfg.AddProfile<UrlDtoMappingProfile>();
                cfg.AddProfile<RoleDtoMappingProfile>();
                cfg.AddProfile<SelectValueDtoMappingProfile>();
            });

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            _mapper = serviceProvider.GetService<IMapper>();
        }

        private IMapper _mapper;
        private readonly ITokenAccessor _tokenAccessor = Substitute.For<ITokenAccessor>();
        private readonly ILocalizerCache _localizerCache = Substitute.For<ILocalizerCache>();

        [Test]
        public void Should_Map()
        {
            // Arrange
            Appointment appointment = FakeAppointments.RockingXMasRehearsal;
            AppointmentDto expectedDto = AppointmentDtoData.RockingXMasRehearsal;

            _tokenAccessor.GetUserRoles().Returns(new List<string> { RoleNames.Staff });

            // Act
            AppointmentDto dto = _mapper.Map<AppointmentDto>(appointment);

            // Assert
            dto.Should().BeEquivalentTo(expectedDto, opt => opt
                .Excluding(dto => dto.Participations));
        }
    }
}

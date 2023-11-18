using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Application.General.MappingActions;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Application.SectionApplication.Model;
using Orso.Arpa.Application.UserApplication.Model;
using Orso.Arpa.Domain.SectionDomain.Model;
using Orso.Arpa.Domain.UserDomain.Model;
using Orso.Arpa.Infrastructure.Localization;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class UserDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddSingleton<LocalizeAction<Section, SectionDto>>();
            services.AddSingleton<UserStatusResolver>();
            services.AddSingleton(_localizerCache);
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<BaseEntityDtoMappingProfile>();
                cfg.AddProfile<UserDtoMappingProfile>();
                cfg.AddProfile<SectionDtoMappingProfile>();
            });

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            _mapper = serviceProvider.GetService<IMapper>();
        }

        private IMapper _mapper;
        private readonly ILocalizerCache _localizerCache = Substitute.For<ILocalizerCache>();

        [Test]
        public void Should_Map_User_To_UserDto()
        {
            // Arrange
            User user = FakeUsers.Performer;
            UserDto expectedDto = UserDtoData.Performer;

            // Act
            UserDto dto = _mapper.Map<UserDto>(user);

            // Assert
            dto.Should().BeEquivalentTo(expectedDto);
        }
    }
}

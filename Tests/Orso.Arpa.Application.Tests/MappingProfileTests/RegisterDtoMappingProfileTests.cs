using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Application.MappingProfiles;
using Orso.Arpa.Domain.Auth;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class RegisterDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<UserRegisterDtoMappingProfile>());

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            var dto = new UserRegisterDto
            {
                Password = UserSeedData.ValidPassword,
                Email = UserSeedData.Orsianer.Email,
                GivenName = "Orsi",
                Surname = "Aner",
                UserName = UserSeedData.Orsianer.UserName
            };

            // Act
            UserRegister.Command command = _mapper.Map<UserRegister.Command>(dto);

            // Assert
            command.Should().BeEquivalentTo(dto);
        }
    }
}

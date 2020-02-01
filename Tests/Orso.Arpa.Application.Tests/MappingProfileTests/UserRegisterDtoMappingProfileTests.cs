using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Domain.Logic.Auth;
using Orso.Arpa.Tests.Shared.TestSeedData;
using static Orso.Arpa.Application.Logic.Auth.UserRegister;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class UserRegisterDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            var dto = new Dto
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

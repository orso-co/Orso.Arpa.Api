using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.AuthApplication;
using Orso.Arpa.Domain.Logic.Auth;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class UserRegisterDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<UserRegisterDtoMappingProfile>());

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map_To_UserRegisterCommand()
        {
            // Arrange
            var dto = new UserRegisterDto
            {
                Password = UserSeedData.ValidPassword,
                Email = UserSeedData.Orsianer.Email,
                GivenName = "Orsi",
                Surname = "Aner",
                UserName = UserSeedData.Orsianer.UserName,
                ClientUri = "http://localhost:4200"
            };

            // Act
            UserRegister.Command command = _mapper.Map<UserRegister.Command>(dto);

            // Assert
            command.Should().BeEquivalentTo(dto);
        }

        [Test]
        public void Should_Map_To_CreateEmailConfirmationTokenCommand()
        {
            // Arrange
            var dto = new UserRegisterDto
            {
                Password = UserSeedData.ValidPassword,
                Email = UserSeedData.Orsianer.Email,
                GivenName = "Orsi",
                Surname = "Aner",
                UserName = UserSeedData.Orsianer.UserName,
                ClientUri = "http://localhost:4200"
            };

            // Act
            CreateEmailConfirmationToken.Command command = _mapper.Map<CreateEmailConfirmationToken.Command>(dto);

            // Assert
            command.Email.Should().BeEquivalentTo(dto.Email);
            command.ClientUri.Should().BeEquivalentTo(dto.ClientUri);
        }
    }
}

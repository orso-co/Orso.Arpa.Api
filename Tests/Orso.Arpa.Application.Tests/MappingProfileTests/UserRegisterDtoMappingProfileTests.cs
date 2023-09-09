using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.AuthApplication.Model;
using Orso.Arpa.Domain.UserDomain.Commands;
using Orso.Arpa.Persistence.Seed;
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
                Email = UserTestSeedData.Performer.Email,
                GivenName = "Orsi",
                Surname = "Aner",
                UserName = UserTestSeedData.Performer.UserName,
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
                Email = UserTestSeedData.Performer.Email,
                GivenName = "Orsi",
                Surname = "Aner",
                UserName = UserTestSeedData.Performer.UserName,
                ClientUri = "http://localhost:4200"
            };

            // Act
            CreateEmailConfirmationToken.Command command = _mapper.Map<CreateEmailConfirmationToken.Command>(dto);

            // Assert
            command.UsernameOrEmail.Should().BeEquivalentTo(dto.Email);
            command.ClientUri.Should().BeEquivalentTo(dto.ClientUri);
        }
    }
}

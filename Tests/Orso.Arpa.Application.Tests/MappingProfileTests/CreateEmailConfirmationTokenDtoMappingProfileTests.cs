using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.AuthApplication.Model;
using Orso.Arpa.Domain.UserDomain.Commands;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class CreateEmailConfirmationTokenDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<CreateEmailConfirmationTokenDtoMappingProfile>());

            _mapper = new Mapper(config);
        }

        private Mapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            var dto = new CreateEmailConfirmationTokenDto
            {
                UsernameOrEmail = UserTestSeedData.Performer.Email,
                ClientUri = "Http://localhost:4200"
            };

            // Act
            CreateEmailConfirmationToken.Command command = _mapper.Map<CreateEmailConfirmationToken.Command>(dto);

            // Assert
            command.Should().BeEquivalentTo(dto);
        }
    }
}

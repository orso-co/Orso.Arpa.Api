using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.AuthApplication;
using Orso.Arpa.Domain.Logic.Auth;
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

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            var dto = new CreateEmailConfirmationTokenDto
            {
                Email = UserSeedData.Orsianer.Email,
                ClientUri = "Http://localhost:4200"
            };

            // Act
            CreateEmailConfirmationToken.Command command = _mapper.Map<CreateEmailConfirmationToken.Command>(dto);

            // Assert
            command.Should().BeEquivalentTo(dto);
        }
    }
}

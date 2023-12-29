using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.AuthApplication.Model;
using Orso.Arpa.Domain.UserDomain.Commands;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class ConfirmEmailDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<ConfirmEmailDtoMappingProfile>());

            _mapper = new Mapper(config);
        }

        private Mapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            var dto = new ConfirmEmailDto
            {
                Email = UserTestSeedData.Performer.Email,
                Token = "token%2B"
            };
            var expectedCommand = new ConfirmEmail.Command
            {
                Email = dto.Email,
                Token = "token+"
            };

            // Act
            ConfirmEmail.Command command = _mapper.Map<ConfirmEmail.Command>(dto);

            // Assert
            command.Should().BeEquivalentTo(expectedCommand);
        }
    }
}

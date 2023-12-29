using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.AuthApplication.Model;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class TokenDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<TokenDtoMappingProfile>());

            _mapper = new Mapper(config);
        }

        private Mapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            const string token = "token";

            // Act
            TokenDto dto = _mapper.Map<TokenDto>(token);

            // Assert
            dto.Token.Should().Be(token);
        }
    }
}

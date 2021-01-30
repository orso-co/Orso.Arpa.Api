using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.MeApplication;
using Orso.Arpa.Domain.Logic.Me;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class MyProfileModifyDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MyProfileModifyDtoMappingProfile>());

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            var dto = new MyProfileModifyDto
            {
                Email = UserSeedData.Performer.Email,
                GivenName = "Orsi",
                Surname = "Aner",
                PhoneNumber = "123456"
            };

            // Act
            Modify.Command command = _mapper.Map<Modify.Command>(dto);

            // Assert
            command.Should().BeEquivalentTo(dto);
        }
    }
}

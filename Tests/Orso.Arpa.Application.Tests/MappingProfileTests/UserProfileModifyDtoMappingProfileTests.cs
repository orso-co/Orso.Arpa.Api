using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Domain.Logic.Me;
using Orso.Arpa.Tests.Shared.TestSeedData;
using static Orso.Arpa.Application.Logic.Me.Modify;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class UserProfileModifyDtoMappingProfileTests
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
                Email = UserSeedData.Orsianer.Email,
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

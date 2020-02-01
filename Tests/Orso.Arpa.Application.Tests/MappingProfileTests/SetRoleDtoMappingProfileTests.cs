using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Domain.Logic.Auth;
using Orso.Arpa.Domain.Roles;
using Orso.Arpa.Tests.Shared.TestSeedData;
using static Orso.Arpa.Application.Logic.Auth.SetRole;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class SetRoleDtoMappingProfileTests
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
                UserName = UserSeedData.Orsianer.UserName,
                RoleName = RoleNames.Orsonaut
            };

            // Act
            SetRole.Command command = _mapper.Map<SetRole.Command>(dto);

            // Assert
            command.Should().BeEquivalentTo(dto);
        }
    }
}

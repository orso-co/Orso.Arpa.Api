using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.AuditLogApplication.Model;
using Orso.Arpa.Domain.AuditLogDomain.Model;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class AuditLogDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AuditLogDtoMappingProfile>());

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            AuditLog auditLog = AuditLogSeedData.UpdateRegion;
            AuditLogDto expectedDto = AuditLogDtoData.UpdateRegion;

            // Act
            AuditLogDto dto = _mapper.Map<AuditLogDto>(auditLog);

            // Assert
            dto.Should().BeEquivalentTo(expectedDto);
        }
    }
}

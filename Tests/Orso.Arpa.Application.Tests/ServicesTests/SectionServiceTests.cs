using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Application.Logic.Sections;
using Orso.Arpa.Application.Services;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.DtoTestData;

namespace Orso.Arpa.Application.Tests.ServicesTests
{
    [TestFixture]
    public class SectionServiceTests
    {
        private IMediator _subMediator;
        private IMapper _subMapper;

        [SetUp]
        public void SetUp()
        {
            _subMediator = Substitute.For<IMediator>();
            _subMapper = Substitute.For<IMapper>();
        }

        private SectionService CreateService()
        {
            return new SectionService(
                _subMediator,
                _subMapper);
        }

        [Test]
        public async Task GetAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            SectionService service = CreateService();
            IList<SectionDto> expectedDtos = SectionDtoData.Sections;
            _subMapper.Map<IEnumerable<SectionDto>>(Arg.Any<IEnumerable<Section>>())
                .Returns(expectedDtos);

            // Act
            IEnumerable<SectionDto> result = await service.GetAsync();

            // Assert
            result.Should().BeEquivalentTo(expectedDtos);
        }
    }
}

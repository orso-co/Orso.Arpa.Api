using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Application.Services;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.DtoTestData;

namespace Orso.Arpa.Application.Tests.ServicesTests
{
    [TestFixture]
    public class SelectValueServiceTests
    {
        private IMediator _subMediator;
        private IMapper _subMapper;

        [SetUp]
        public void SetUp()
        {
            _subMediator = Substitute.For<IMediator>();
            _subMapper = Substitute.For<IMapper>();
        }

        private SelectValueService CreateService()
        {
            return new SelectValueService(
                _subMediator,
                _subMapper);
        }

        [Test]
        public async Task GetAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            SelectValueService service = CreateService();
            string tableName = null;
            string propertyName = null;
            IList<SelectValueDto> expectedDtos = SelectValueDtoData.ProjectGenres;
            _subMapper.Map<IEnumerable<SelectValueDto>>(Arg.Any<IEnumerable<SelectValueMapping>>())
                .Returns(expectedDtos);

            // Act
            IEnumerable<SelectValueDto> result = await service.GetAsync(
                tableName,
                propertyName);

            // Assert
            result.Should().BeEquivalentTo(expectedDtos);
        }
    }
}

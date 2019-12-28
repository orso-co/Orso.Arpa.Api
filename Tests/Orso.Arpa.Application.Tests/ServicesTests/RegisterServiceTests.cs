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
    public class RegisterServiceTests
    {
        private IMediator _subMediator;
        private IMapper _subMapper;

        [SetUp]
        public void SetUp()
        {
            _subMediator = Substitute.For<IMediator>();
            _subMapper = Substitute.For<IMapper>();
        }

        private RegisterService CreateService()
        {
            return new RegisterService(
                _subMediator,
                _subMapper);
        }

        [Test]
        public async Task GetAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            RegisterService service = CreateService();
            IList<RegisterDto> expectedDtos = RegisterDtoData.Registers;
            _subMapper.Map<IEnumerable<RegisterDto>>(Arg.Any<IEnumerable<Register>>())
                .Returns(expectedDtos);

            // Act
            IEnumerable<RegisterDto> result = await service.GetAsync();

            // Assert
            result.Should().BeEquivalentTo(expectedDtos);
        }
    }
}

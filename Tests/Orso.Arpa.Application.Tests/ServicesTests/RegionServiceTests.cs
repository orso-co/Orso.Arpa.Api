using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Application.Logic.Regions;
using Orso.Arpa.Application.Services;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.DtoTestData;

namespace Orso.Arpa.Application.Tests.ServicesTests
{
    [TestFixture]
    public class RegionServiceTests
    {
        private IMediator _subMediator;
        private IMapper _subMapper;

        [SetUp]
        public void SetUp()
        {
            _subMediator = Substitute.For<IMediator>();
            _subMapper = Substitute.For<IMapper>();
        }

        private RegionService CreateService()
        {
            return new RegionService(
                _subMediator,
                _subMapper);
        }

        [Test]
        public async Task CreateAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            RegionService service = CreateService();
            Logic.Regions.Create.Dto createDto = null;
            RegionDto expectedDto = RegionDtoData.Freiburg;
            _subMapper.Map<RegionDto>(Arg.Any<Region>()).Returns(expectedDto);

            // Act
            RegionDto result = await service.CreateAsync(
                createDto);

            // Assert
            result.Should().Be(expectedDto);
        }

        [Test]
        public void DeleteAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            RegionService service = CreateService();
            var id = default(Guid);

            // Act
            Func<Task> func = async () => await service.DeleteAsync(
                id);

            // Assert
            func.Should().NotThrow();
        }

        [Test]
        public async Task GetAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            RegionService service = CreateService();
            IList<RegionDto> expectedDtos = RegionDtoData.Regions;
            _subMapper.Map<IEnumerable<RegionDto>>(Arg.Any<IImmutableList<Region>>()).Returns(expectedDtos);

            // Act
            IEnumerable<RegionDto> result = await service.GetAsync();

            // Assert
            result.Should().BeEquivalentTo(expectedDtos);
        }

        [Test]
        public async Task GetByIdAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            RegionService service = CreateService();
            var id = default(Guid);
            RegionDto expectedDto = RegionDtoData.Stuttgart;
            _subMapper.Map<RegionDto>(Arg.Any<Region>()).Returns(expectedDto);

            // Act
            RegionDto result = await service.GetByIdAsync(
                id);

            // Assert
            result.Should().Be(expectedDto);
        }

        [Test]
        public void ModifyAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            RegionService service = CreateService();
            Logic.Regions.Modify.Dto modifyDto = null;

            // Act
            Func<Task> func = async () => await service.ModifyAsync(
               modifyDto);

            // Assert
            func.Should().NotThrow();
        }
    }
}

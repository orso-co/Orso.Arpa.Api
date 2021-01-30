using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Application.RoleApplication;
using Orso.Arpa.Application.Services;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Roles = Orso.Arpa.Domain.Roles;

namespace Orso.Arpa.Application.Tests.ServicesTests
{
    [TestFixture]
    public class RoleServiceTests
    {
        private RoleService _roleService;
        private IMapper _mapper;
        private IMediator _mediator;

        [SetUp]
        public void Setup()
        {
            _mapper = Substitute.For<IMapper>();
            _mediator = Substitute.For<IMediator>();
            _roleService = new RoleService(_mediator, _mapper);
        }

        [Test]
        public async Task Should_Get_Async()
        {
            // Arrange
            _mediator.Send(Arg.Any<Domain.Logic.Roles.List.Query>()).Returns(RoleSeedData.Roles);

            _mapper.Map<RoleDto>(Arg.Any<Role>())
                .Returns(RoleDtoData.Performer, RoleDtoData.Staff, RoleDtoData.Admin);

            // Act
            IEnumerable<RoleDto> dtos = await _roleService.GetAsync();

            // Assert
            dtos.Should().BeEquivalentTo(RoleDtoData.Roles);
        }
    }
}

using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Logic.AppointmentParticipations;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.AppointmentPerticipationsTests.CommandHandlerTests
{
    [TestFixture]
    public class SetResultHandlerTests
    {
        private IMapper _mapper;
        private IArpaContext _arpaContext;
        private SetResult.Handler _handler;

        [SetUp]
        public void Setup()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _mapper = Substitute.For<IMapper>();
            _handler = new SetResult.Handler(_arpaContext, _mapper);
        }

        [Test]
        public async Task Should_Set_Result()
        {
            // Arrange
            _arpaContext.Appointments
                    .FindAsync(Arg.Any<Guid>())
                .Returns(AppointmentSeedData.RockingXMasConcert);
            _arpaContext.SaveChangesAsync(Arg.Any<CancellationToken>())
                .Returns(1);
            _mapper.Map<SetResult.Command, Create.Command>(Arg.Any<SetResult.Command>())
                .Returns(new Create.Command());

            // Act
            Unit result = await _handler.Handle(
                new SetResult.Command
                {
                    PersonId = Guid.NewGuid(),
                    ResultId = Guid.NewGuid(),
                    Id = Guid.NewGuid()
                },
                new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(Unit.Value);
        }
    }
}

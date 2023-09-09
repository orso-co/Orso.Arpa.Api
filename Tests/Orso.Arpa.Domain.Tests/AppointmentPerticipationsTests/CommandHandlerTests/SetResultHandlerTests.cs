using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.AppointmentDomain.Commands;
using Orso.Arpa.Domain.AppointmentDomain.Enums;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.AppointmentPerticipationsTests.CommandHandlerTests
{
    [TestFixture]
    public class SetResultHandlerTests
    {
        private IMapper _mapper;
        private IArpaContext _arpaContext;
        private SetAppointmentParticipationResult.Handler _handler;

        [SetUp]
        public void Setup()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _mapper = Substitute.For<IMapper>();
            _handler = new SetAppointmentParticipationResult.Handler(_arpaContext, _mapper);
        }

        [Test]
        public async Task Should_Set_Result()
        {
            // Arrange
            _ = _arpaContext.Appointments
                    .FindAsync(Arg.Any<object[]>(), Arg.Any<CancellationToken>())
                .Returns(AppointmentSeedData.RockingXMasConcert);
            _ = _arpaContext.SaveChangesAsync(Arg.Any<CancellationToken>())
                .Returns(1);
            _ = _mapper.Map<SetAppointmentParticipationResult.Command, CreateAppointmentParticipation.Command>(Arg.Any<SetAppointmentParticipationResult.Command>())
                .Returns(new CreateAppointmentParticipation.Command());

            // Act
            Unit result = await _handler.Handle(
                new SetAppointmentParticipationResult.Command
                {
                    PersonId = Guid.NewGuid(),
                    Result = AppointmentParticipationResult.Present,
                    Id = Guid.NewGuid()
                },
                new CancellationToken());

            // Assert
            _ = result.Should().BeEquivalentTo(Unit.Value);
        }
    }
}

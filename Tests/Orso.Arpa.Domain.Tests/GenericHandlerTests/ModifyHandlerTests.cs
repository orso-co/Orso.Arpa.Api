using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.GenericHandlers;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.GenericHandlerTests
{
    [TestFixture]
    public class ModifyHandlerTests
    {
        private IArpaContext _arpaContext;
        private IMapper _mapper;
        private Modify.Handler<Appointment> _handler;

        [SetUp]
        public void Setup()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _mapper = Substitute.For<IMapper>();
            _handler = new Modify.Handler<Appointment>(_arpaContext, _mapper);
        }

        [Test]
        public async Task Should_Modify()
        {
            // Arrange
            Appointment expectedAppointment = AppointmentSeedData.RockingXMasConcert;
            _arpaContext.FindAsync<Appointment>(Arg.Any<object[]>(), Arg.Any<CancellationToken>())
                .Returns(expectedAppointment);
            _arpaContext.SaveChangesAsync(Arg.Any<CancellationToken>())
                .Returns(1);

            // Act
            Unit result = await _handler.Handle(new Logic.Appointments.Modify.Command(), new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(Unit.Value);
        }
    }
}

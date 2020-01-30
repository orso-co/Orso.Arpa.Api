using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Logic.Appointments;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.AppointmentTests.CommandHandlerTests
{
    [TestFixture]
    public class RemoveRoomHandlerTests
    {
        private IRepository _repository;
        private IUnitOfWork _unitOfWork;
        private RemoveRoom.Handler _handler;

        [SetUp]
        public void Setup()
        {
            _repository = Substitute.For<IRepository>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _handler = new RemoveRoom.Handler(_repository, _unitOfWork);
        }

        [Test]
        public async Task Should_Remove_Project()
        {
            // Arrange
            _repository.GetByIdAsync<Appointment>(Arg.Any<Guid>())
                .Returns(AppointmentSeedData.AfterShowParty);
            _unitOfWork.CommitAsync()
                .Returns(true);

            // Act
            Unit result = await _handler.Handle(new RemoveRoom.Command(Guid.NewGuid(), RoomSeedData.AulaWeiherhofSchule.Id), new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(Unit.Value);
        }
    }
}

using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Appointments;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.AppointmentTests.CommandHandlerTests
{
    [TestFixture]
    public class RemoveProjectHandlerTests
    {
        private IRepository _repository;
        private IUnitOfWork _unitOfWork;
        private RemoveProject.Handler _handler;

        [SetUp]
        public void Setup()
        {
            _repository = Substitute.For<IRepository>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _handler = new RemoveProject.Handler(_repository, _unitOfWork);
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
            Unit result = await _handler.Handle(new RemoveProject.Command(Guid.NewGuid(), ProjectSeedData.RockingXMas.Id), new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(Unit.Value);
        }
    }
}

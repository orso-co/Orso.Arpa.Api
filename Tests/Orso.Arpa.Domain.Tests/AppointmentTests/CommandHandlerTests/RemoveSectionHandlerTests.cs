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
using Orso.Arpa.Domain.Sections.Seed;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.AppointmentTests.CommandHandlerTests
{
    [TestFixture]
    public class RemoveSectionHandlerTests
    {
        private IRepository _repository;
        private IUnitOfWork _unitOfWork;
        private RemoveSection.Handler _handler;

        [SetUp]
        public void Setup()
        {
            _repository = Substitute.For<IRepository>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _handler = new RemoveSection.Handler(_repository, _unitOfWork);
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
            Unit result = await _handler.Handle(new RemoveSection.Command(Guid.NewGuid(), SectionSeedData.Alto.Id), new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(Unit.Value);
        }
    }
}

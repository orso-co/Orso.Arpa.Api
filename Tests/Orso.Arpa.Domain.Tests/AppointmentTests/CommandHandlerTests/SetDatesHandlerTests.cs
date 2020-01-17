using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
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
    public class SetDatesHandlerTests
    {
        private IRepository _repository;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private SetDates.Handler _handler;

        [SetUp]
        public void Setup()
        {
            _repository = Substitute.For<IRepository>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _mapper = Substitute.For<IMapper>();
            _handler = new SetDates.Handler(_repository, _unitOfWork, _mapper);
        }

        [Test]
        public async Task Should_Set_Dates()
        {
            // Arrange
            _repository.GetByIdAsync<Appointment>(Arg.Any<Guid>())
                .Returns(AppointmentSeedData.RockingXMasConcert);
            _unitOfWork.CommitAsync()
                .Returns(true);

            // Act
            Unit result = await _handler.Handle(
                new SetDates.Command
                {
                    StartTime = DateTime.UtcNow,
                    EndTime = DateTime.UtcNow.AddHours(2),
                    Id = Guid.NewGuid()
                },
                new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(Unit.Value);
        }
    }
}

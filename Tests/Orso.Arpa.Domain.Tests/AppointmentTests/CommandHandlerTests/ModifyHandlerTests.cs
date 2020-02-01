using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
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
    public class ModifyHandlerTests
    {
        private IRepository _repository;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private Modify.Handler _handler;

        [SetUp]
        public void Setup()
        {
            _repository = Substitute.For<IRepository>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _mapper = Substitute.For<IMapper>();
            _handler = new Modify.Handler(_repository, _unitOfWork, _mapper);
        }

        [Test]
        public async Task Should_Modify()
        {
            // Arrange
            Appointment expectedAppointment = AppointmentSeedData.RockingXMasConcert;
            _repository.GetByIdAsync<Appointment>(Arg.Any<Guid>())
                .Returns(expectedAppointment);
            _unitOfWork.CommitAsync()
                .Returns(true);

            // Act
            Unit result = await _handler.Handle(new Modify.Command(), new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(Unit.Value);
        }
    }
}

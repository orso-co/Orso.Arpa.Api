using System;
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
        private IRepository _repository;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private Modify.Handler<Appointment> _handler;

        [SetUp]
        public void Setup()
        {
            _repository = Substitute.For<IRepository>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _mapper = Substitute.For<IMapper>();
            _handler = new Modify.Handler<Appointment>(_repository, _unitOfWork, _mapper);
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
            Unit result = await _handler.Handle(new Logic.Appointments.Modify.Command(), new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(Unit.Value);
        }
    }
}

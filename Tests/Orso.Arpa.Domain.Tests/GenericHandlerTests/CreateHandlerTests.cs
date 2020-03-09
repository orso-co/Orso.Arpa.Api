using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.GenericHandlers;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.GenericHandlerTests
{
    [TestFixture]
    public class CreateHandlerTests
    {
        private IRepository _repository;
        private IUnitOfWork _unitOfWork;
        private Create.Handler<Appointment> _handler;

        [SetUp]
        public void Setup()
        {
            _repository = Substitute.For<IRepository>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _handler = new Create.Handler<Appointment>(_repository, _unitOfWork);
        }

        [Test]
        public async Task Should_Add_Project()
        {
            // Arrange
            Appointment expectedAppointment = AppointmentSeedData.RockingXMasConcert;
            _repository.AddAsync(Arg.Any<Appointment>())
                .Returns(expectedAppointment);
            _unitOfWork.CommitAsync()
                .Returns(true);

            // Act
            Appointment result = await _handler.Handle(new Logic.Appointments.Create.Command(), new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(expectedAppointment);
        }
    }
}

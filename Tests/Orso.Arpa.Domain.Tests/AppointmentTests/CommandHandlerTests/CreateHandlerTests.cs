using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Appointments;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.AppointmentTests.CommandHandlerTests
{
    [TestFixture]
    public class CreateHandlerTests
    {
        private IRepository _repository;
        private IUnitOfWork _unitOfWork;
        private Create.Handler _handler;

        [SetUp]
        public void Setup()
        {
            _repository = Substitute.For<IRepository>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _handler = new Create.Handler(_repository, _unitOfWork);
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
            Appointment result = await _handler.Handle(new Create.Command(), new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(expectedAppointment);
        }
    }
}

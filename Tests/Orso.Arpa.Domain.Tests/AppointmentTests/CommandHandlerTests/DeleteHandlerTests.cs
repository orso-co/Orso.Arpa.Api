using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Appointments;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Tests.AppointmentTests.CommandHandlerTests
{
    [TestFixture]
    public class DeleteHandlerTests
    {
        private IRepository _repository;
        private IUnitOfWork _unitOfWork;
        private Delete.Handler _handler;

        [SetUp]
        public void Setup()
        {
            _repository = Substitute.For<IRepository>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _handler = new Delete.Handler(_repository, _unitOfWork);
        }

        [Test]
        public async Task Should_Delete()
        {
            // Arrange
            _unitOfWork.CommitAsync().Returns(true);

            // Act
            Unit result = await _handler.Handle(new Delete.Command(), new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(Unit.Value);
        }
    }
}

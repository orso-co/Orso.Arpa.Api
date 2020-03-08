using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.GenericHandlers;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Tests.GenericHandlerTests
{
    [TestFixture]
    public class DeleteHandlerTests
    {
        private IRepository _repository;
        private IUnitOfWork _unitOfWork;
        private Delete.Handler<Appointment> _handler;

        [SetUp]
        public void Setup()
        {
            _repository = Substitute.For<IRepository>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _handler = new Delete.Handler<Appointment>(_repository, _unitOfWork);
        }

        [Test]
        public async Task Should_Delete()
        {
            // Arrange
            _unitOfWork.CommitAsync().Returns(true);

            // Act
            Unit result = await _handler.Handle(new Delete.Command<Appointment>(), new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(Unit.Value);
        }
    }
}

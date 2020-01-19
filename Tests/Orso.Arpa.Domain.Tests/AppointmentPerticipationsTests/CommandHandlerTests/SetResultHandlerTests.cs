using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.AppointmentParticipations;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.AppointmentPerticipationsTests.CommandHandlerTests
{
    [TestFixture]
    public class SetResultHandlerTests
    {
        private IRepository _repository;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private SetResult.Handler _handler;

        [SetUp]
        public void Setup()
        {
            _repository = Substitute.For<IRepository>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _mapper = Substitute.For<IMapper>();
            _handler = new SetResult.Handler(_repository, _unitOfWork, _mapper);
        }

        [Test]
        public async Task Should_Set_Result()
        {
            // Arrange
            _repository.GetByIdAsync<Appointment>(Arg.Any<Guid>())
                .Returns(AppointmentSeedData.RockingXMasConcert);
            _unitOfWork.CommitAsync()
                .Returns(true);
            _mapper.Map<SetResult.Command, Create.Command>(Arg.Any<SetResult.Command>())
                .Returns(new Create.Command());

            // Act
            Unit result = await _handler.Handle(
                new SetResult.Command
                {
                    PersonId = Guid.NewGuid(),
                    ResultId = Guid.NewGuid(),
                    Id = Guid.NewGuid()
                },
                new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(Unit.Value);
        }
    }
}

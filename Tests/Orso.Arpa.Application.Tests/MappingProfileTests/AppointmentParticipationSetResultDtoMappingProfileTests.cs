using System;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class AppointmentParticipationSetResultDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AppointmentParticipationSetResultDtoMappingProfile>());

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            var dto = new AppointmentParticipationSetResultDto
            {
                Id = Guid.NewGuid(),
                PersonId = Guid.NewGuid(),
                Body = new AppointmentParticipationSetResultBodyDto
                {
                    Result = AppointmentParticipationResult.Absent
                }
            };
            var expectedCommand = new SetResult.Command
            {
                Id = dto.Id,
                PersonId = dto.PersonId,
                Result = AppointmentParticipationResult.Absent
            };

            // Act
            SetResult.Command command = _mapper.Map<SetResult.Command>(dto);

            // Assert
            _ = command.Should().BeEquivalentTo(expectedCommand);
        }
    }
}

using System;

namespace Orso.Arpa.Application.Dtos
{
    public class AppointmentParticipationDto : BaseEntityDto
    {
        public Guid PersonId { get; private set; }
        public PersonDto Person { get; private set; }
        public Guid AppointmentId { get; private set; }
        public Guid? ResultId { get; private set; }
        public Guid? PredictionId { get; private set; }
        public Guid? ExpectationId { get; private set; }
    }
}

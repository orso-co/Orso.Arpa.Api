using System;

namespace Orso.Arpa.Application.Dtos
{
    public class AppointmentParticipationDto : BaseEntityDto
    {
        public Guid PersonId { get; set; }
        public PersonDto Person { get; set; }
        public Guid AppointmentId { get; set; }
        public Guid? ResultId { get; set; }
        public Guid? PredictionId { get; set; }
        public Guid? ExpectationId { get; set; }
    }
}

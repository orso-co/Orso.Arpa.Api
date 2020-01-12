using System;

namespace Orso.Arpa.Application.Dtos
{
    public class AppointmentParticipationDto : BaseEntityDto
    {
        public Guid? ResultId { get; set; }
        public Guid? PredictionId { get; set; }
    }
}

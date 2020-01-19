using System;

namespace Orso.Arpa.Application.Dtos
{
    public class SetParticipationResultDto
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public Guid ResultId { get; set; }
    }
}

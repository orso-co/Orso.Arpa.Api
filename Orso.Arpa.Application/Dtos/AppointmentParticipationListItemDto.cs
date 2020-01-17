using System.Collections.Generic;

namespace Orso.Arpa.Application.Dtos
{
    public class AppointmentParticipationListItemDto
    {
        public PersonDto Person { get; set; }
        public AppointmentParticipationDto Participation { get; set; }
        public IList<MusicianProfileDto> MusicianProfiles { get; set; } = new List<MusicianProfileDto>();
    }
}

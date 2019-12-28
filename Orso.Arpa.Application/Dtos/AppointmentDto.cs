using System;
using System.Collections.Generic;

namespace Orso.Arpa.Application.Dtos
{
    public class AppointmentDto : BaseEntityDto
    {
        public Guid? CategoryId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Name { get; set; }
        public string PublicDetails { get; set; }
        public string InternalDetails { get; set; }
        public Guid? StatusId { get; set; }
        public Guid? EmolumentId { get; set; }
        public Guid? EmolumentPatternId { get; set; }
        public Guid? VenueId { get; set; }
        public IEnumerable<RoomDto> Rooms { get; set; } = new List<RoomDto>();
        public IEnumerable<ProjectDto> Projects { get; set; } = new List<ProjectDto>();
        public IEnumerable<SectionDto> Sections { get; set; } = new List<SectionDto>();
        public IEnumerable<AppointmentParticipationDto> Participations { get; set; } = new List<AppointmentParticipationDto>();
    }
}

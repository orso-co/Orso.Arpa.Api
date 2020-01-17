using System;

namespace Orso.Arpa.Application.Dtos
{
    public class AppointmentCreateDto
    {
        public Guid? CategoryId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Name { get; set; }
        public string PublicDetails { get; set; }
        public string InternalDetails { get; set; }
        public Guid? StatusId { get; set; }
        public Guid? EmolumentId { get; set; }
        public Guid? EmolumentPatternId { get; set; }
        public Guid? ExpectationId { get; set; }
    }
}

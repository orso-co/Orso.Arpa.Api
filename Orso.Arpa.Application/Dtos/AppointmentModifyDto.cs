using System;
using Orso.Arpa.Application.Interfaces;

namespace Orso.Arpa.Application.Dtos
{
    public class AppointmentModifyDto : IModifyDto
    {
        public Guid Id { get; set; }

        public Guid? CategoryId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Name { get; set; }
        public string PublicDetails { get; set; }
        public string InternalDetails { get; set; }
        public Guid? StatusId { get; set; }
        public Guid? EmolumentId { get; set; }
        public Guid? EmolumentPatternId { get; set; }
    }
}

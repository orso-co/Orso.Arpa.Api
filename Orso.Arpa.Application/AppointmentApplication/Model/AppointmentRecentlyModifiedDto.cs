using System;
using System.Collections.Generic;
using Orso.Arpa.Application.ProjectApplication.Model;
using Orso.Arpa.Domain.AppointmentDomain.Enums;

namespace Orso.Arpa.Application.AppointmentApplication.Model
{
    public class AppointmentRecentlyModifiedDto
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string VenueName { get; set; }
        public AppointmentStatus? Status { get; set; }
        public AppointmentType Type { get; set; }
        public string Category { get; set; }
        public IEnumerable<ReducedProjectDto> Projects { get; set; } = [];
        public string PublicDetails { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }
        public IList<AppointmentChangeDto> Changes { get; set; } = [];
    }
}

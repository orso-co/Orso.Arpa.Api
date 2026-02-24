using System;

namespace Orso.Arpa.Application.AppointmentApplication.Model
{
    public class CalendarFeedTokenDto
    {
        public Guid Id { get; set; }
        public string FeedType { get; set; }
        public Guid? ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string FeedUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastAccessedAt { get; set; }
        public bool IsActive { get; set; }
    }
}

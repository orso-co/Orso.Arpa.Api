using System;

namespace Orso.Arpa.Application.AppointmentApplication.Model
{
    public class CreateCalendarFeedTokenDto
    {
        public string FeedType { get; set; }
        public Guid? ProjectId { get; set; }
    }
}

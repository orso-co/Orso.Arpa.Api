using System;
using Microsoft.EntityFrameworkCore;

namespace Orso.Arpa.Domain.Views
{
    [Keyless]
    public class AppointmentForUser
    {
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public Guid? PersonId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PublicDetails { get; set; }
        public string InternalDetails { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}

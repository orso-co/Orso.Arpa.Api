using System;

namespace Orso.Arpa.Domain.General.Configuration
{
    public class ClubConfiguration
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string ContactEmail { get; set; }
        public string SupportEmail { get; set; }
        public Uri Url { get; set; }
    }
}

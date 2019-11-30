using System;

namespace Orso.Arpa.Application.Dtos
{
    public class RegionDto
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedAt { get; set; }
    }
}

using System;

namespace Orso.Arpa.Application.Dtos
{
    public class SelectValueDto
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedAt { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

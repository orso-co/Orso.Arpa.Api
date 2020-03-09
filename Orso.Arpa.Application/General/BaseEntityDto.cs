using System;

namespace Orso.Arpa.Application.General
{
    public abstract class BaseEntityDto
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedAt { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedAt { get; set; }
    }
}

using System;

namespace Orso.Arpa.Application.Dtos
{
    public class RemoveProjectDto
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
    }
}

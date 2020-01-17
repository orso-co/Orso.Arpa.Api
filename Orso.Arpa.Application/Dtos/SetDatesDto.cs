using System;
using Orso.Arpa.Application.Interfaces;

namespace Orso.Arpa.Application.Dtos
{
    public class SetDatesDto : IModifyDto
    {
        public Guid Id { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}

using System;
using Orso.Arpa.Application.Interfaces;

namespace Orso.Arpa.Application.Dtos
{
    public class RegionModifyDto : IModifyDto
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
    }
}

using System;
using Orso.Arpa.Domain.Regions;

namespace Orso.Arpa.Domain.Entities
{
    public class Region : BaseEntity
    {
        public Region(Guid id, Create.Command command) : base(id)
        {
            Name = command.Name;
        }

        public string Name { get; private set; }
    }
}

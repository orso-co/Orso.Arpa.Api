using System;
using Orso.Arpa.Domain.SelectValues;

namespace Orso.Arpa.Domain.Entities
{
    public class SelectValue : BaseEntity
    {
        public SelectValue(Guid id, Create.Command command) : base(id)
        {
            Name = command.Name;
            Description = command.Description;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
    }
}

using System;
using Orso.Arpa.Domain.Logic.Regions;

namespace Orso.Arpa.Domain.Entities
{
    public class Region : BaseEntity
    {
        internal Region(Guid? id, Create.Command command) : base(id)
        {
            Name = command.Name;
        }

        protected Region()
        {
        }

        public string Name { get; private set; }
    }
}

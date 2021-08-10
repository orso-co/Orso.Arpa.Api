using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Logic.Regions;

namespace Orso.Arpa.Domain.Entities
{
    public class Region : BaseEntity
    {
        public Region(Guid? id, Create.Command command) : base(id)
        {
            Name = command.Name;
            IsForPerformance = command.IsForPerformance;
            IsForRehearsal = command.IsForRehearsal;
        }

        protected Region()
        {
        }

        public string Name { get; private set; }
        public bool IsForRehearsal { get; private set; }
        public bool IsForPerformance { get; private set; }

        public virtual ICollection<RegionPreference> RegionPreferences { get; private set; } = new HashSet<RegionPreference>();
    }
}

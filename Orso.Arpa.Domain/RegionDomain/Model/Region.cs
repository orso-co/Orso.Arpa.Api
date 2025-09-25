using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.General.Attributes;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.RegionDomain.Commands;

namespace Orso.Arpa.Domain.RegionDomain.Model
{
    public class Region : BaseEntity
    {
        public Region(Guid id, CreateRegion.Command command) : base(id)
        {
            Name = command.Name;
            IsForPerformance = command.IsForPerformance;
            IsForRehearsal = command.IsForRehearsal;
        }

        protected Region()
        {
        }

        public void Update(ModifyRegion.Command command)
        {
            Name = command.Name;
            IsForPerformance = command.IsForPerformance;
            IsForRehearsal = command.IsForRehearsal;
        }

        public string Name { get; private set; }
        public bool IsForRehearsal { get; private set; }
        public bool IsForPerformance { get; private set; }

        [CascadingSoftDelete]
        public virtual ICollection<RegionPreference> RegionPreferences { get; private set; } = new HashSet<RegionPreference>();
    }
}

using System;
using System.Collections.Generic;
using Orso.Arpa.Application.Tranlation;

namespace Orso.Arpa.Domain.Entities
{
    public class Section : BaseEntity
    {
        public Section(Guid? id, string name, Guid? parentId) : base(id)
        {
            Name = name;
            ParentId = parentId;
        }

        protected Section()
        {
        }

        public string Name { get; private set; }
        public Guid? ParentId { get; private set; }
        public virtual Section Parent { get; private set; }
        public virtual ICollection<Section> Children { get; private set; } = new HashSet<Section>();
        public virtual ICollection<SectionAppointment> SectionAppointments { get; private set; } = new HashSet<SectionAppointment>();
        public virtual ICollection<MusicianProfile> MusicianProfiles { get; private set; } = new HashSet<MusicianProfile>();
    }
}

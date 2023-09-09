using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.General.Attributes;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;
using Orso.Arpa.Domain.PersonDomain.Model;
using Orso.Arpa.Domain.SelectValueDomain.Model;

namespace Orso.Arpa.Domain.SectionDomain.Model
{
    public class Section : BaseEntity
    {
        public Section(Guid? id, string name, Guid? parentId, bool isInstrument, byte instrumentPartCount = 0) : base(id)
        {
            Name = name;
            ParentId = parentId;
            IsInstrument = isInstrument;
            InstrumentPartCount = instrumentPartCount;
        }

        protected Section()
        {
        }

        public override string ToString()
        {
            return Name;
        }

        public string Name { get; private set; }
        public bool IsInstrument { get; private set; }
        public byte InstrumentPartCount { get; private set; }
        public Guid? ParentId { get; private set; }
        public virtual Section Parent { get; private set; }

        [CascadingSoftDelete]
        public virtual ICollection<Section> Children { get; private set; } = new HashSet<Section>();

        [CascadingSoftDelete]
        public virtual ICollection<SectionAppointment> SectionAppointments { get; private set; } = new HashSet<SectionAppointment>();

        [CascadingSoftDelete]
        public virtual ICollection<MusicianProfile> MusicianProfiles { get; private set; } = new HashSet<MusicianProfile>();

        [CascadingSoftDelete]
        public virtual ICollection<PersonSection> StakeholderGroups { get; private set; } = new HashSet<PersonSection>();

        [CascadingSoftDelete]
        public virtual ICollection<MusicianProfileSection> MusicianProfileSections { get; private set; } = new HashSet<MusicianProfileSection>();

        [CascadingSoftDelete]
        public virtual ICollection<SelectValueSection> SelectValueSections { get; private set; } = new HashSet<SelectValueSection>();
    }
}

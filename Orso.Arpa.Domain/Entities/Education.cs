using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Orso.Arpa.Domain.Entities
{
    public class Education : BaseEntity
    {
        internal Education(Guid? id) : base(id)
        {
        }

        [JsonConstructor]
        protected Education()
        {
        }

        public string Timespan { get; private set; }
        public string Institution { get; private set; }

        public string Comment { get; private set; }
        public byte SortOrder { get; private set; }

        public virtual ICollection<MusicianProfileEducation> MusicianProfileEducations { get; private set; } = new HashSet<MusicianProfileEducation>();
    }
}

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Orso.Arpa.Domain.Entities
{
    public class Reference : BaseEntity
    {
        internal Reference(Guid? id) : base(id)
        {
        }

        [JsonConstructor]
        protected Reference()
        {
        }

        public string Timespan { get; private set; }
        public string Keyword { get; private set; }

        public string Details { get; private set; }
        public byte SortOrder { get; private set; }

        public virtual ICollection<MusicianProfileReference> MusicianProfileReferences { get; private set; } = new HashSet<MusicianProfileReference>();
    }
}

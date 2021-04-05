using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Orso.Arpa.Domain.Entities
{
    public class Credential : BaseEntity
    {
        internal Credential(Guid? id) : base(id)
        {
        }

        [JsonConstructor]
        protected Credential()
        {
        }

        public string Timespan { get; private set; }
        public string Keyword { get; private set; }

        public string Details { get; private set; }
        public byte SortOrder { get; private set; }

        public virtual ICollection<MusicianProfileCredential> MusicianProfileCredentials { get; private set; } = new HashSet<MusicianProfileCredential>();
    }
}

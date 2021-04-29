using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Orso.Arpa.Domain.Entities
{
    public class CurriculumVitaeReference : BaseEntity
    {
        internal CurriculumVitaeReference(Guid? id) : base(id)
        {
        }

        [JsonConstructor]
        protected CurriculumVitaeReference()
        {
        }

        public string Timespan { get; private set; }
        public string Keyword { get; private set; }

        public string Details { get; private set; }
        public byte SortOrder { get; private set; }

        public virtual ICollection<MusicianProfileCurriculumVitaeReference> MusicianProfileCurriculumVitaeReferences { get; private set; } = new HashSet<MusicianProfileCurriculumVitaeReference>();
    }
}

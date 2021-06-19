using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.Logic.Educations;

namespace Orso.Arpa.Domain.Entities
{
    public class Education : BaseEntity
    {
        public Education(Guid? id, Create.Command command) : base(id)
        {
            TimeSpan = command.TimeSpan;
            Institution = command.Institution;
            TypeId = command.TypeId;
            Description = command.Description;
            SortOrder = command.SortOrder;
        }

        internal Education(Guid? id) : base(id)
        {
        }

        [JsonConstructor]
        protected Education()
        {
        }

        public string TimeSpan { get; private set; }
        public string Institution { get; private set; }
        public Guid TypeId { get; private set; }
        public virtual SelectValueMapping Type { get; private set; }
        public string Description { get; private set; }

        public byte SortOrder { get; private set; }

        public virtual ICollection<MusicianProfileEducation> MusicianProfileEducations { get; private set; } = new HashSet<MusicianProfileEducation>();
    }
}

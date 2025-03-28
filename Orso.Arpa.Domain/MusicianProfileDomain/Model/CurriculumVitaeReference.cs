using System;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.MusicianProfileDomain.Commands;
using Orso.Arpa.Domain.SelectValueDomain.Model;

namespace Orso.Arpa.Domain.MusicianProfileDomain.Model
{
    public class CurriculumVitaeReference : BaseEntity
    {
        public CurriculumVitaeReference(Guid? id, CreateCurriculumVitaeReference.Command command) : base(id)
        {
            TimeSpan = command.TimeSpan;
            Institution = command.Institution;
            TypeId = command.TypeId;
            Description = command.Description;
            SortOrder = command.SortOrder;
            MusicianProfileId = command.MusicianProfileId;
        }

        internal CurriculumVitaeReference(Guid? id) : base(id)
        {
        }

        [JsonConstructor]
        protected CurriculumVitaeReference()
        {
        }

        public void Update(ModifyCurriculumVitaeReference.Command command)
        {
            TimeSpan = command.TimeSpan;
            Institution = command.Institution;
            TypeId = command.TypeId;
            Description = command.Description;
            SortOrder = command.SortOrder;
        }

        public string TimeSpan { get; private set; }
        public string Institution { get; private set; }
        public Guid? TypeId { get; private set; }
        public virtual SelectValueMapping Type { get; private set; }
        public string Description { get; private set; }

        public byte? SortOrder { get; private set; }
        public Guid MusicianProfileId { get; private set; }
        public virtual MusicianProfile MusicianProfile { get; private set; }
    }
}

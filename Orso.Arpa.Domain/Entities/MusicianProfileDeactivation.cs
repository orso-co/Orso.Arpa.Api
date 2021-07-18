using System;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.Logic.MusicianProfileDeactivations;

namespace Orso.Arpa.Domain.Entities
{
    public class MusicianProfileDeactivation : BaseEntity
    {
        public MusicianProfileDeactivation(Guid? id, Create.Command command) : base(id)
        {
            DeactivationStart = command.DeactivationStart;
            Purpose = command.Purpose;
            MusicianProfileId = command.MusicianProfileId;
        }

        [JsonConstructor]
        protected MusicianProfileDeactivation()
        {
        }

        public DateTime DeactivationStart { get; private set; }
        public string Purpose { get; private set; }
        public Guid MusicianProfileId { get; private set; }
        public virtual MusicianProfile MusicianProfile { get; private set; }
    }
}

using System;
using Orso.Arpa.Domain.Logic.ProjectParticipations;

namespace Orso.Arpa.Domain.Entities
{
    public class ProjectParticipation : BaseEntity
    {
        public ProjectParticipation(Guid? id, Create.Command command) : base(id)
        {
            ProjectId = command.ProjectId;
            MusicianProfileId = command.MusicianProfileId;
        }

        protected ProjectParticipation()
        {
        }

        public Guid ProjectId { get; private set; }
        public virtual Project Project { get; private set; }
        public Guid MusicianProfileId { get; private set; }
        public virtual MusicianProfile MusicianProfile { get; private set; }
    }
}

using System;
using Orso.Arpa.Domain.Entities;
using static Orso.Arpa.Domain.GenericHandlers.Create;

namespace Orso.Arpa.Domain.Logic.ProjectParticipations
{
    public static class Create
    {
        public class Command : ICreateCommand<ProjectParticipation>
        {
            public Guid ProjectId { get; set; }
            public Guid MusicianProfileId { get; set; }
        }
    }
}

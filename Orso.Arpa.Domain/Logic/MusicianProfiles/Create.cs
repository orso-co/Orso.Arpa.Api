using System;
using Orso.Arpa.Domain.Entities;
using static Orso.Arpa.Domain.GenericHandlers.Create;

namespace Orso.Arpa.Domain.Logic.MusicianProfiles
{
    public static class Create
    {
        public class Command : ICreateCommand<MusicianProfile>
        {
            public bool IsProfessional { get; set; }
            public Guid PersonId { get; set; }
            public Guid SectionId { get; set; }
        }
    }
}

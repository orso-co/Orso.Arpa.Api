using System;
using Orso.Arpa.Domain.Entities;
using static Orso.Arpa.Domain.GenericHandlers.Modify;

namespace Orso.Arpa.Domain.Logic.MusicianProfileSections
{
    public static class Modify
    {
        public class Command : IModifyCommand<MusicianProfileSection>
        {
            public Guid Id { get; set; }
        }
    }
}

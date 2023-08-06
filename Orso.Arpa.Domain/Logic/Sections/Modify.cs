using System;
using Orso.Arpa.Domain.Entities;
using static Orso.Arpa.Domain.GenericHandlers.Modify;

namespace Orso.Arpa.Domain.Logic.Sections
{
    public static class Modify
    {
        public class Command : IModifyCommand<Section>
        {
            public Guid Id { get; set; }
        }
    }
}

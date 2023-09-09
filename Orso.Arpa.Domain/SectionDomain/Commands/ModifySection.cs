using System;
using Orso.Arpa.Domain.SectionDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Modify;

namespace Orso.Arpa.Domain.SectionDomain.Commands
{
    public static class ModifySection
    {
        public class Command : IModifyCommand<Section>
        {
            public Guid Id { get; set; }
        }
    }
}

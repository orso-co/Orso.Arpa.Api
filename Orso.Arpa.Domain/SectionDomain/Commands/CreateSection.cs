using Orso.Arpa.Domain.SectionDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Create;

namespace Orso.Arpa.Domain.SectionDomain.Commands
{
    public static class CreateSection
    {
        public class Command : ICreateCommand<Section>
        {
        }
    }
}

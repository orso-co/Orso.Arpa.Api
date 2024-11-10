
using MediatR;
using Orso.Arpa.Domain.AddressDomain.Commands;
using Orso.Arpa.Domain.ClubDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Create;

namespace Orso.Arpa.Domain.ClubDomain.Commands
{
    public static class CreateClub
    {
        public class Command : ICreateCommand<Club> {
            public string Name { get; set; }
        }
    }
}

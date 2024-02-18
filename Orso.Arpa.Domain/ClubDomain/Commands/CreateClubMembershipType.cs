using System;
using Orso.Arpa.Domain.ClubDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Create;

namespace Orso.Arpa.Domain.ClubDomain.Commands
{
    public static class CreateClubMembershipType
    {
        public class Command : ICreateCommand<ClubMembershipType>
        {
            public Guid ClubId { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public int TerminationPeriodInMonths { get; set; }
        }
    }
}
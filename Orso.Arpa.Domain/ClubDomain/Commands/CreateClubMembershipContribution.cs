using System;
using Orso.Arpa.Domain.ClubDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Create;

namespace Orso.Arpa.Domain.ClubDomain.Commands
{
    public static class CreateClubMembershipContribution
    {
        public class Command : ICreateCommand<ClubMembershipContribution> {
            public decimal ContributionPerYearInEuro { get; set; }
            public DateTime ValidFrom { get; set; }
            public int? DeviatingVoucherPerConcertForParticipantsInPercent { get; set; }
            public int VoucherPerConcertInPercent { get; set; }
            public Guid MembershipSubTypeId { get; set; }

        }
    }
}

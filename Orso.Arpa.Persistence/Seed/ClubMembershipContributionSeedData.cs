using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.ClubDomain.Commands;
using Orso.Arpa.Domain.ClubDomain.Model;

namespace Orso.Arpa.Persistence.Seed
{
    public static class ClubMembershipContributionSeedData
    {
        public static List<ClubMembershipContribution> ClubMembershipContributions => new()
        {
            ChoirFreiburgFullContribution,
            ChoirFreiburgDiscountedContribution,
            ChoirFreiburgPassiveContribution,
            OrchestraFreiburgFullContribution,
            OrchestraFreiburgDiscountedContribution,
            SponsoringFreiburgSonataContribution,
            SponsoringFreiburgConcertoContribution,
            SponsoringFreiburgSymphonyContribution,
            SponsoringFreiburgOperaContribution
        };

        public static ClubMembershipContribution ChoirFreiburgFullContribution => new ClubMembershipContribution(
            Guid.Parse("891839d0-7d63-4ead-97a9-14b59deb40a6"),
            new CreateClubMembershipContribution.Command
            {
                ContributionPerYearInEuro = 150,
                ValidFrom = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                VoucherPerConcertInPercent = 20,
                MembershipSubTypeId = ClubMembershipSubTypeSeedData.ChoirFreiburgFull.Id
            });
        public static ClubMembershipContribution ChoirFreiburgDiscountedContribution => new ClubMembershipContribution(
            Guid.Parse("6dd65f56-175f-42cf-b01a-87ed60699244"),
            new CreateClubMembershipContribution.Command
            {
                ContributionPerYearInEuro = 90,
                ValidFrom = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                VoucherPerConcertInPercent = 20,
                MembershipSubTypeId = ClubMembershipSubTypeSeedData.ChoirFreiburgDiscounted.Id
            });
        public static ClubMembershipContribution ChoirFreiburgPassiveContribution => new ClubMembershipContribution(
            Guid.Parse("46895e9e-0a64-4ba5-99b9-58a48db7d061"),
            new CreateClubMembershipContribution.Command
            {

                ContributionPerYearInEuro = 30,
                ValidFrom = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                VoucherPerConcertInPercent = 10,
                MembershipSubTypeId = ClubMembershipSubTypeSeedData.ChoirFreiburgPassive.Id
            });
        public static ClubMembershipContribution OrchestraFreiburgFullContribution => new ClubMembershipContribution(
            Guid.Parse("c770da21-7b1b-483c-9411-6f5a6f54b3e5"),
            new CreateClubMembershipContribution.Command
            {
                ContributionPerYearInEuro = 60,
                ValidFrom = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                VoucherPerConcertInPercent = 20,
                MembershipSubTypeId = ClubMembershipSubTypeSeedData.OrchestraFreiburgFull.Id
            });
        public static ClubMembershipContribution OrchestraFreiburgDiscountedContribution => new ClubMembershipContribution(
            Guid.Parse("cceffa0d-07fd-4e37-aafd-dfb70e3f0de3"),
            new CreateClubMembershipContribution.Command
            {
                ContributionPerYearInEuro = 45,
                ValidFrom = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                VoucherPerConcertInPercent = 20,
                MembershipSubTypeId = ClubMembershipSubTypeSeedData.OrchestraFreiburgDiscounted.Id
            });
        public static ClubMembershipContribution SponsoringFreiburgSonataContribution => new ClubMembershipContribution(
            Guid.Parse("c4edb43f-9d42-4791-9bb9-cf6f7c35cb9d"),
            new CreateClubMembershipContribution.Command
            {
                ContributionPerYearInEuro = 50,
                ValidFrom = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                VoucherPerConcertInPercent = 5,
                MembershipSubTypeId = ClubMembershipSubTypeSeedData.SponsoringFreiburgSonata.Id
            });
        public static ClubMembershipContribution SponsoringFreiburgConcertoContribution => new ClubMembershipContribution(
            Guid.Parse("c3c9cac7-2d96-46bb-a9a0-90d331aa69a1"),
            new CreateClubMembershipContribution.Command
            {
                ContributionPerYearInEuro = 100,
                ValidFrom = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                VoucherPerConcertInPercent = 10,
                MembershipSubTypeId = ClubMembershipSubTypeSeedData.SponsoringFreiburgConcerto.Id
            });
        public static ClubMembershipContribution SponsoringFreiburgSymphonyContribution => new ClubMembershipContribution(
            Guid.Parse("573d5fab-4d19-47c7-8442-bffb18bd5111"),
            new CreateClubMembershipContribution.Command
            {
                ContributionPerYearInEuro = 150,
                ValidFrom = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                VoucherPerConcertInPercent = 15,
                MembershipSubTypeId = ClubMembershipSubTypeSeedData.SponsoringFreiburgSymphony.Id
            });
        public static ClubMembershipContribution SponsoringFreiburgOperaContribution => new ClubMembershipContribution(
            Guid.Parse("08db5cb9-29a3-4d1f-834c-6e7bd51c9bee"),
            new CreateClubMembershipContribution.Command
            {
                ContributionPerYearInEuro = 300,
                ValidFrom = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                VoucherPerConcertInPercent = 30,
                MembershipSubTypeId = ClubMembershipSubTypeSeedData.SponsoringFreiburgOpera.Id
            });
    }
}
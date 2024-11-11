using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.ClubDomain.Commands;
using Orso.Arpa.Domain.ClubDomain.Model;

namespace Orso.Arpa.Persistence.Seed
{
    public static class ClubMembershipContributionSeedData
    {
        public static List<ClubMembershipContribution> ClubMembershipContributions =
        [
            .. ClubMembershipContributionsFreiburg,
            .. ClubMembershipContributionsStuttgart,
            .. ClubMembershipContributionsBerlin
        ];

        public static List<ClubMembershipContribution> ClubMembershipContributionsFreiburg =>
        [
            ChoirFreiburgFullContribution,
            ChoirFreiburgDiscountedContribution,
            ChoirFreiburgPassiveContribution,
            OrchestraFreiburgFullContribution,
            OrchestraFreiburgDiscountedContribution,
            SponsoringFreiburgSonataContribution,
            SponsoringFreiburgConcertoContribution,
            SponsoringFreiburgSymphonyContribution,
            SponsoringFreiburgOperaContribution
        ];

        public static List<ClubMembershipContribution> ClubMembershipContributionsStuttgart =>
        [
            StuttgartFullContribution,
            StuttgartDiscountedContribution,
            SponsoringStuttgartConcertoContribution,
            SponsoringStuttgartSymphonyContribution,
            SponsoringStuttgartOperaContribution
        ];

        public static List<ClubMembershipContribution> ClubMembershipContributionsBerlin =>
        [
            ChoirBerlinFullContribution,
            ChoirBerlinDiscountedContribution,
            OrchestraBerlinFullContribution,
            OrchestraBerlinDiscountedContribution,
            SponsoringBerlinSonataContribution,
            SponsoringBerlinConcertoContribution,
            SponsoringBerlinSymphonyContribution,
            SponsoringBerlinOperaContribution
        ];

        #region Freiburg
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
        #endregion

        #region Stuttgart
        public static ClubMembershipContribution StuttgartFullContribution => new ClubMembershipContribution(
            Guid.Parse("00b78bd4-196e-433a-ae10-e99003350e83"),
            new CreateClubMembershipContribution.Command
            {
                ContributionPerYearInEuro = 150,
                ValidFrom = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                DeviatingVoucherPerConcertForParticipantsInPercent = 20,
                VoucherPerConcertInPercent = 10,
                MembershipSubTypeId = ClubMembershipSubTypeSeedData.StuttgartFull.Id
            });
        public static ClubMembershipContribution StuttgartDiscountedContribution => new ClubMembershipContribution(
            Guid.Parse("a7c27fe2-c830-4974-b4ac-60726b9d0eba"),
            new CreateClubMembershipContribution.Command
            {
                ContributionPerYearInEuro = 90,
                ValidFrom = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                DeviatingVoucherPerConcertForParticipantsInPercent = 20,
                VoucherPerConcertInPercent = 10,
                MembershipSubTypeId = ClubMembershipSubTypeSeedData.StuttgartDiscounted.Id
            });

        public static ClubMembershipContribution SponsoringStuttgartConcertoContribution => new ClubMembershipContribution(
            Guid.Parse("0381a852-c83b-4b7e-a804-4be46851f696"),
            new CreateClubMembershipContribution.Command
            {
                ContributionPerYearInEuro = 100,
                ValidFrom = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                DeviatingVoucherPerConcertForParticipantsInPercent = 10,
                VoucherPerConcertInPercent = 10,
                MembershipSubTypeId = ClubMembershipSubTypeSeedData.SponsoringStuttgartConcerto.Id
            });
        public static ClubMembershipContribution SponsoringStuttgartSymphonyContribution => new ClubMembershipContribution(
            Guid.Parse("a7cff67e-89dd-4300-8b31-a475c12f6d43"),
            new CreateClubMembershipContribution.Command
            {
                ContributionPerYearInEuro = 150,
                ValidFrom = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                VoucherPerConcertInPercent = 15,
                MembershipSubTypeId = ClubMembershipSubTypeSeedData.SponsoringStuttgartSymphony.Id
            });
        public static ClubMembershipContribution SponsoringStuttgartOperaContribution => new ClubMembershipContribution(
            Guid.Parse("1edfa391-8fbd-41ab-b88b-c3867547cd87"),
            new CreateClubMembershipContribution.Command
            {
                ContributionPerYearInEuro = 300,
                ValidFrom = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                VoucherPerConcertInPercent = 30,
                MembershipSubTypeId = ClubMembershipSubTypeSeedData.SponsoringStuttgartOpera.Id
            });
        #endregion

        #region Berlin
        public static ClubMembershipContribution ChoirBerlinFullContribution => new ClubMembershipContribution(
            Guid.Parse("a3cc890f-ca0f-4c9b-bea3-b95180f40338"),
            new CreateClubMembershipContribution.Command
            {
                ContributionPerYearInEuro = 240,
                ValidFrom = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                VoucherPerConcertInPercent = 10,
                DeviatingVoucherPerConcertForParticipantsInPercent = 20,
                MembershipSubTypeId = ClubMembershipSubTypeSeedData.ChoirBerlinFull.Id
            });
        public static ClubMembershipContribution ChoirBerlinDiscountedContribution => new ClubMembershipContribution(
            Guid.Parse("725207a8-8087-4aff-8057-550cf88158cc"),
            new CreateClubMembershipContribution.Command
            {
                ContributionPerYearInEuro = 120,
                ValidFrom = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                VoucherPerConcertInPercent = 10,
                DeviatingVoucherPerConcertForParticipantsInPercent = 20,
                MembershipSubTypeId = ClubMembershipSubTypeSeedData.ChoirBerlinDiscounted.Id
            });
        public static ClubMembershipContribution OrchestraBerlinFullContribution => new ClubMembershipContribution(
            Guid.Parse("d8bdbb3a-5123-4385-a81e-789d3a0e3488"),
            new CreateClubMembershipContribution.Command
            {
                ContributionPerYearInEuro = 120,
                ValidFrom = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                VoucherPerConcertInPercent = 10,
                DeviatingVoucherPerConcertForParticipantsInPercent = 20,
                MembershipSubTypeId = ClubMembershipSubTypeSeedData.OrchestraBerlinFull.Id
            });
        public static ClubMembershipContribution OrchestraBerlinDiscountedContribution => new ClubMembershipContribution(
            Guid.Parse("13e48948-cd21-4d50-93e9-34411713f572"),
            new CreateClubMembershipContribution.Command
            {
                ContributionPerYearInEuro = 60,
                ValidFrom = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                VoucherPerConcertInPercent = 10,
                DeviatingVoucherPerConcertForParticipantsInPercent = 20,
                MembershipSubTypeId = ClubMembershipSubTypeSeedData.OrchestraBerlinDiscounted.Id
            });
        public static ClubMembershipContribution SponsoringBerlinSonataContribution => new ClubMembershipContribution(
            Guid.Parse("6e26fdb2-2ed8-438f-94c1-4e08876898f6"),
            new CreateClubMembershipContribution.Command
            {
                ContributionPerYearInEuro = 100,
                ValidFrom = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                VoucherPerConcertInPercent = 5,
                MembershipSubTypeId = ClubMembershipSubTypeSeedData.SponsoringBerlinSonata.Id
            });
        public static ClubMembershipContribution SponsoringBerlinConcertoContribution => new ClubMembershipContribution(
            Guid.Parse("0a8c5f09-419d-44c6-a717-92a7534eb192"),
            new CreateClubMembershipContribution.Command
            {
                ContributionPerYearInEuro = 150,
                ValidFrom = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                VoucherPerConcertInPercent = 10,
                MembershipSubTypeId = ClubMembershipSubTypeSeedData.SponsoringBerlinConcerto.Id
            });
        public static ClubMembershipContribution SponsoringBerlinSymphonyContribution => new ClubMembershipContribution(
            Guid.Parse("aa0bc024-4a19-4b39-9140-b2a2446d59f3"),
            new CreateClubMembershipContribution.Command
            {
                ContributionPerYearInEuro = 200,
                ValidFrom = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                VoucherPerConcertInPercent = 20,
                MembershipSubTypeId = ClubMembershipSubTypeSeedData.SponsoringBerlinSymphony.Id
            });
        public static ClubMembershipContribution SponsoringBerlinOperaContribution => new ClubMembershipContribution(
            Guid.Parse("fca5b166-de29-4c55-bbea-56143df3119f"),
            new CreateClubMembershipContribution.Command
            {
                ContributionPerYearInEuro = 300,
                ValidFrom = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                VoucherPerConcertInPercent = 30,
                MembershipSubTypeId = ClubMembershipSubTypeSeedData.SponsoringBerlinOpera.Id
            });
        #endregion
    }
}

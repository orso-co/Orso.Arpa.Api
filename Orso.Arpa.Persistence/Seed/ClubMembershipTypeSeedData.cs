using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.ClubDomain.Commands;
using Orso.Arpa.Domain.ClubDomain.Model;

namespace Orso.Arpa.Persistence.Seed
{
    public static class ClubMembershipTypeSeedData
    {
        public static List<ClubMembershipType> ClubMembershipTypes =
        [
            .. ClubMembershipTypesFreiburg,
            .. ClubMembershipTypesStuttgart,
            .. ClubMembershipTypesBerlin
        ];

        public static List<ClubMembershipType> ClubMembershipTypesFreiburg => [
            ChoirFreiburg,
            OrchestraFreiburg,
            SponsoringFreiburg
        ];

        public static List<ClubMembershipType> ClubMembershipTypesStuttgart => [
            Stuttgart,
            SponsoringStuttgart
        ];

        public static List<ClubMembershipType> ClubMembershipTypesBerlin => [
            ChoirBerlin,
            OrchestraBerlin,
            SponsoringBerlin
        ];

        #region Freiburg
        public static ClubMembershipType ChoirFreiburg => new ClubMembershipType(
            Guid.Parse("d573fb50-03e5-4ca0-8cd7-babb88e9e23b"),
            new CreateClubMembershipType.Command
            {
                Name = "Chormitgliedschaft",
                Description = "Chor Freiburg",
                TerminationPeriodInMonths = 3,
                ClubId = ClubSeedData.OrsoFreiburg.Id
            }
        );

        public static ClubMembershipType OrchestraFreiburg => new ClubMembershipType(
            Guid.Parse("a553eb8b-543c-4aa6-b50d-ec6e100f9532"),
            new CreateClubMembershipType.Command
            {
                Name = "Orchestermitgliedschaft",
                Description = "Orchester Freiburg",
                TerminationPeriodInMonths = 3,
                ClubId = ClubSeedData.OrsoFreiburg.Id
            }
        );

        public static ClubMembershipType SponsoringFreiburg => new ClubMembershipType(
            Guid.Parse("7e831185-d348-42ed-a0af-5f5f3c470391"),
            new CreateClubMembershipType.Command
            {
                Name = "Fördermitgliedschaft",
                Description = "Fördermitgliedschaft Freiburg",
                TerminationPeriodInMonths = 3,
                ClubId = ClubSeedData.OrsoFreiburg.Id
            }
        );
        #endregion

        #region Stuttgart
        public static ClubMembershipType Stuttgart => new ClubMembershipType(
            Guid.Parse("d573fb50-03e5-4ca0-8cd7-babb88e9e23b"),
            new CreateClubMembershipType.Command
            {
                Name = "Ordentliche Mitgliedschaft",
                Description = "Ordentliche Mitgliedschaft Stuttgart",
                TerminationPeriodInMonths = 3,
                ClubId = ClubSeedData.OrsoStuttgart.Id
            }
        );

        public static ClubMembershipType SponsoringStuttgart => new ClubMembershipType(
            Guid.Parse("7e831185-d348-42ed-a0af-5f5f3c470391"),
            new CreateClubMembershipType.Command
            {
                Name = "Fördermitgliedschaft",
                Description = "Fördermitgliedschaft Stuttgart",
                TerminationPeriodInMonths = 3,
                ClubId = ClubSeedData.OrsoStuttgart.Id
            }
        );
        #endregion

        #region Berlin
        public static ClubMembershipType ChoirBerlin => new ClubMembershipType(
            Guid.Parse("d573fb50-03e5-4ca0-8cd7-babb88e9e23b"),
            new CreateClubMembershipType.Command
            {
                Name = "Chormitgliedschaft",
                Description = "Chor Berlin",
                TerminationPeriodInMonths = 3,
                ClubId = ClubSeedData.OrsoBerlin.Id
            }
        );

        public static ClubMembershipType OrchestraBerlin => new ClubMembershipType(
            Guid.Parse("a553eb8b-543c-4aa6-b50d-ec6e100f9532"),
            new CreateClubMembershipType.Command
            {
                Name = "Orchestermitgliedschaft",
                Description = "Orchester Berlin",
                TerminationPeriodInMonths = 3,
                ClubId = ClubSeedData.OrsoBerlin.Id
            }
        );

        public static ClubMembershipType SponsoringBerlin => new ClubMembershipType(
            Guid.Parse("7e831185-d348-42ed-a0af-5f5f3c470391"),
            new CreateClubMembershipType.Command
            {
                Name = "Fördermitgliedschaft",
                Description = "Fördermitgliedschaft Berlin",
                TerminationPeriodInMonths = 3,
                ClubId = ClubSeedData.OrsoBerlin.Id
            }
        );
        #endregion
    }
}

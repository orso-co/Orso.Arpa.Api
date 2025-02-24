
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Orso.Arpa.Domain.AppointmentDomain.Queries;
using Orso.Arpa.Domain.ClubDomain.Commands;
using Orso.Arpa.Domain.ClubDomain.Model;

namespace Orso.Arpa.Persistence.Seed
{
    public static class ClubMembershipSubTypeSeedData
    {
        public static List<ClubMembershipSubType> ClubMembershipSubTypes
        {
            get
            {
                return [
                            .. ClubMembershipSubTypesFreiburg,
                        .. ClubMembershipSubTypesStuttgart,
                        .. ClubMembershipSubTypeBerlin
                        ];
            }
        }

        private static List<ClubMembershipSubType> ClubMembershipSubTypesFreiburg => [
                ChoirFreiburgFull,
                ChoirFreiburgDiscounted,
                ChoirFreiburgPassive,
                OrchestraFreiburgFull,
                OrchestraFreiburgDiscounted,
                SponsoringFreiburgSonata,
                SponsoringFreiburgConcerto,
                SponsoringFreiburgSymphony,
                SponsoringFreiburgOpera
        ];

        private static List<ClubMembershipSubType> ClubMembershipSubTypesStuttgart => [
                StuttgartFull,
                StuttgartDiscounted,
                SponsoringStuttgartConcerto,
                SponsoringStuttgartSymphony,
                SponsoringStuttgartOpera
        ];

        private static List<ClubMembershipSubType> ClubMembershipSubTypeBerlin => [
                ChoirBerlinFull,
                ChoirBerlinDiscounted,
                OrchestraBerlinFull,
                OrchestraBerlinDiscounted,
                SponsoringBerlinSonata,
                SponsoringBerlinConcerto,
                SponsoringBerlinSymphony,
                SponsoringBerlinOpera
        ];

        #region Freiburg
        public static ClubMembershipSubType ChoirFreiburgFull => new ClubMembershipSubType(
            Guid.Parse("7eebc473-2555-434d-ae90-08f3e0e3d835"),
            new CreateClubMembershipSubType.Command
            {
                Name = "Vollmitgliedschaft",
                MemberhsipTypeId = ClubMembershipTypeSeedData.ChoirFreiburg.Id
            }
        );

        public static ClubMembershipSubType ChoirFreiburgDiscounted => new ClubMembershipSubType(
            Guid.Parse("b3aa7334-2198-4f74-b033-08aec1714762"),
            new CreateClubMembershipSubType.Command
            {
                Name = "Ermäßigte Mitgliedschaft",
                MemberhsipTypeId = ClubMembershipTypeSeedData.ChoirFreiburg.Id
            }
        );

        public static ClubMembershipSubType ChoirFreiburgPassive => new ClubMembershipSubType(
            Guid.Parse("58a5d420-316b-4999-9e35-d1df11e9b7d9"),
            new CreateClubMembershipSubType.Command
            {
                Name = "Passive Mitgliedschaft",
                MemberhsipTypeId = ClubMembershipTypeSeedData.ChoirFreiburg.Id
            }
        );

        public static ClubMembershipSubType OrchestraFreiburgFull => new ClubMembershipSubType(
            Guid.Parse("45fd4efa-247a-4bcd-af3d-68d9e8b09f3b"),
            new CreateClubMembershipSubType.Command
            {
                Name = "Vollmitgliedschaft",
                MemberhsipTypeId = ClubMembershipTypeSeedData.OrchestraFreiburg.Id
            }
        );

        public static ClubMembershipSubType OrchestraFreiburgDiscounted => new ClubMembershipSubType(
            Guid.Parse("4fc69809-3530-4fa3-b740-91ed627067ae"),
            new CreateClubMembershipSubType.Command
            {
                Name = "Ermäßigte Mitgliedschaft",
                MemberhsipTypeId = ClubMembershipTypeSeedData.OrchestraFreiburg.Id
            });

        public static ClubMembershipSubType SponsoringFreiburgSonata => new ClubMembershipSubType(
            Guid.Parse("157bb518-2a4f-4594-88c4-19c8a4564913"),
            new CreateClubMembershipSubType.Command
            {
                Name = "Sonata",
                MemberhsipTypeId = ClubMembershipTypeSeedData.SponsoringFreiburg.Id
            });

        public static ClubMembershipSubType SponsoringFreiburgConcerto => new ClubMembershipSubType(
            Guid.Parse("d2a26311-37c9-4262-9c42-d5422791bf93"),
            new CreateClubMembershipSubType.Command
            {
                Name = "Concerto",
                MemberhsipTypeId = ClubMembershipTypeSeedData.SponsoringFreiburg.Id
            });

        public static ClubMembershipSubType SponsoringFreiburgSymphony => new ClubMembershipSubType(
            Guid.Parse("0733f733-aa41-4b44-a844-5fb5918f1e1e"),
            new CreateClubMembershipSubType.Command
            {
                Name = "Symphony",
                MemberhsipTypeId = ClubMembershipTypeSeedData.SponsoringFreiburg.Id
            });

        public static ClubMembershipSubType SponsoringFreiburgOpera => new ClubMembershipSubType(
            Guid.Parse("1fbbfbc8-b54f-4c3c-ade9-c22f646b1b7a"),
            new CreateClubMembershipSubType.Command
            {
                Name = "Opera",
                MemberhsipTypeId = ClubMembershipTypeSeedData.SponsoringFreiburg.Id
            });
        #endregion

        #region Stuttgart
        public static ClubMembershipSubType StuttgartFull => new ClubMembershipSubType(
            Guid.Parse("80f328c9-48c9-4d72-9811-e3b93afae405"),
            new CreateClubMembershipSubType.Command
            {
                Name = "Vollmitgliedschaft",
                MemberhsipTypeId = ClubMembershipTypeSeedData.Stuttgart.Id
            }
        );

        public static ClubMembershipSubType StuttgartDiscounted => new ClubMembershipSubType(
            Guid.Parse("1435c3c5-6aaa-4b81-a269-f2b6f0e831b2"),
            new CreateClubMembershipSubType.Command
            {
                Name = "Ermäßigte Mitgliedschaft",
                MemberhsipTypeId = ClubMembershipTypeSeedData.Stuttgart.Id
            }
        );

        public static ClubMembershipSubType SponsoringStuttgartConcerto => new ClubMembershipSubType(
            Guid.Parse("378b5352-8d96-412c-af28-e48b296cb360"),
            new CreateClubMembershipSubType.Command
            {
                Name = "Concerto",
                MemberhsipTypeId = ClubMembershipTypeSeedData.SponsoringStuttgart.Id
            });

        public static ClubMembershipSubType SponsoringStuttgartSymphony => new ClubMembershipSubType(
            Guid.Parse("38e971b6-6592-4efe-8919-8b19a2c365d7"),
            new CreateClubMembershipSubType.Command
            {
                Name = "Symphony",
                MemberhsipTypeId = ClubMembershipTypeSeedData.SponsoringStuttgart.Id
            });

        public static ClubMembershipSubType SponsoringStuttgartOpera => new ClubMembershipSubType(
            Guid.Parse("9ceedd14-edaa-4c59-b25d-6187a07fac7d"),
            new CreateClubMembershipSubType.Command
            {
                Name = "Opera",
                MemberhsipTypeId = ClubMembershipTypeSeedData.SponsoringStuttgart.Id
            });
        #endregion

        #region Berlin
        public static ClubMembershipSubType ChoirBerlinFull => new ClubMembershipSubType(
            Guid.Parse("fd21006f-ec6b-41fb-b383-0a11f6623707"),
            new CreateClubMembershipSubType.Command
            {
                Name = "Vollmitgliedschaft",
                MemberhsipTypeId = ClubMembershipTypeSeedData.ChoirBerlin.Id
            }
        );

        public static ClubMembershipSubType ChoirBerlinDiscounted => new ClubMembershipSubType(
            Guid.Parse("7cf3b7b1-c9df-49ea-b7d4-f1063900fb48"),
            new CreateClubMembershipSubType.Command
            {
                Name = "Ermäßigte Mitgliedschaft",
                MemberhsipTypeId = ClubMembershipTypeSeedData.ChoirBerlin.Id
            }
        );

        public static ClubMembershipSubType OrchestraBerlinFull => new ClubMembershipSubType(
            Guid.Parse("6df7f8fd-08cc-4188-8b65-a5ecbfadac49"),
            new CreateClubMembershipSubType.Command
            {
                Name = "Vollmitgliedschaft",
                MemberhsipTypeId = ClubMembershipTypeSeedData.OrchestraBerlin.Id
            }
        );

        public static ClubMembershipSubType OrchestraBerlinDiscounted => new ClubMembershipSubType(
            Guid.Parse("644547ec-330b-4b70-8987-94622074b514"),
            new CreateClubMembershipSubType.Command
            {
                Name = "Ermäßigte Mitgliedschaft",
                MemberhsipTypeId = ClubMembershipTypeSeedData.OrchestraBerlin.Id
            });

        public static ClubMembershipSubType SponsoringBerlinSonata => new ClubMembershipSubType(
            Guid.Parse("2b3c4a88-39d7-49a2-8dfb-f4be156c06ed"),
            new CreateClubMembershipSubType.Command
            {
                Name = "Sonata",
                MemberhsipTypeId = ClubMembershipTypeSeedData.SponsoringBerlin.Id
            });

        public static ClubMembershipSubType SponsoringBerlinConcerto => new ClubMembershipSubType(
            Guid.Parse("9ac25c3c-af53-4aa7-a1fb-13bb899f5dba"),
            new CreateClubMembershipSubType.Command
            {
                Name = "Concerto",
                MemberhsipTypeId = ClubMembershipTypeSeedData.SponsoringBerlin.Id
            });

        public static ClubMembershipSubType SponsoringBerlinSymphony => new ClubMembershipSubType(
            Guid.Parse("f472ae22-e9ba-4091-9635-e63b4eebcec1"),
            new CreateClubMembershipSubType.Command
            {
                Name = "Symphony",
                MemberhsipTypeId = ClubMembershipTypeSeedData.SponsoringBerlin.Id
            });

        public static ClubMembershipSubType SponsoringBerlinOpera => new ClubMembershipSubType(
            Guid.Parse("55f4067d-f88e-4f51-a33e-d6e53597d275"),
            new CreateClubMembershipSubType.Command
            {
                Name = "Opera",
                MemberhsipTypeId = ClubMembershipTypeSeedData.SponsoringBerlin.Id
            });
        #endregion
    }
}

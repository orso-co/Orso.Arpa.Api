using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.ClubDomain.Commands;
using Orso.Arpa.Domain.ClubDomain.Model;

namespace Orso.Arpa.Persistence.Seed
{
    public static class ClubMembershipSubTypeSeedData
    {
        public static List<ClubMembershipSubType> ClubMembershipSubTypes => [
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
    }
}
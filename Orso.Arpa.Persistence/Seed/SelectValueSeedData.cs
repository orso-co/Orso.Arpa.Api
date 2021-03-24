using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.Seed
{
    public static class SelectValueSeedData
    {
        public static IList<SelectValue> SelectValues
        {
            get
            {
                return new List<SelectValue>
                {
                    Partly,
                    Absent,
                    Ambiguous,
                    Assembly,
                    Audition,
                    AwaitingPoll,
                    ChamberMusic,
                    ChoreographyRehearsal,
                    ClassicalMusic,
                    Concert,
                    Confirmed,
                    Crossover,
                    DontKnowYet,
                    AwaitingScan,
                    Gloeckner2018,
                    Inapplicable,
                    Mandatory,
                    Meeting,
                    No,
                    Optional,
                    OrchestraConcertLumpSum10h,
                    OrchestraConcertLumpSum12h,
                    OrchestraConcertLumpSum1808,
                    OrchestraRehearsalHourlyRate,
                    Other,
                    Party,
                    Pending,
                    PhotoSession,
                    Present,
                    Refused,
                    Rehearsal,
                    RehearsalWeekendChoir,
                    Scheduled,
                    SectionalRehearsal,
                    SeeComment,
                    Show,
                    SpecialCase,
                    StageBriefing,
                    Transfer,
                    VoiceFormation,
                    WatchShow,
                    Workshop,
                    Yes,
                    Private,
                    Work,
                    CDRecording,
                    ConcertTour,
                    Contest,
                    RehearsalWeekend,
                    SpecialProject,
                };
            }
        }

        /// <summary>
        /// Anwesend
        /// </summary>
        public static SelectValue Present
        {
            get
            {
                return new SelectValue(Guid.Parse("313445ca-57fa-45f0-8515-325949d60726"), "Present", string.Empty);
            }
        }

        /// <summary>
        /// Nicht anwesend
        /// </summary>
        public static SelectValue Absent
        {
            get
            {
                return new SelectValue(Guid.Parse("f0f26735-b796-4a70-a20c-92e0e2910bb4"), "Absent", string.Empty);
            }
        }

        /// <summary>
        /// Unzutreffend
        /// </summary>
        public static SelectValue Inapplicable
        {
            get
            {
                return new SelectValue(Guid.Parse("86bf6480-787a-4fe0-9d79-0f8d0d36acc4"), "Inapplicable", string.Empty);
            }
        }

        /// <summary>
        /// Unklar
        /// </summary>
        public static SelectValue Ambiguous
        {
            get
            {
                return new SelectValue(Guid.Parse("66a6446a-7191-4f14-9c5d-052891b9c5a2"), "Ambiguous", string.Empty);
            }
        }

        /// <summary>
        /// Eintrag nach Scan
        /// </summary>
        public static SelectValue AwaitingScan
        {
            get
            {
                return new SelectValue(Guid.Parse("5d31f1f7-73fd-42a4-a429-33fab925b15d"), "Awaiting Scan", string.Empty);
            }
        }

        /// <summary>
        /// Ja
        /// </summary>
        public static SelectValue Yes
        {
            get
            {
                return new SelectValue(Guid.Parse("75a017d3-dca5-49ec-9bbd-3b01b159ba5b"), "Yes", string.Empty);
            }
        }

        /// <summary>
        /// Nein
        /// </summary>
        public static SelectValue No
        {
            get
            {
                return new SelectValue(Guid.Parse("88b763ac-8093-4c5d-a881-85be1fb8a24d"), "No", string.Empty);
            }
        }

        /// <summary>
        /// Teilweise
        /// </summary>
        public static SelectValue Partly
        {
            get
            {
                return new SelectValue(Guid.Parse("1e60dfdf-e7c9-4378-b1af-dcb53fe20022"), "Partly", string.Empty);
            }
        }

        /// <summary>
        /// Ich weiß es noch nicht
        /// </summary>
        public static SelectValue DontKnowYet
        {
            get
            {
                return new SelectValue(Guid.Parse("4ee7d317-6d71-4d6e-b45a-954c8c7dcf03"), "Don't know yet", string.Empty);
            }
        }

        public static SelectValue Pending
        {
            get
            {
                return new SelectValue(Guid.Parse("362efd25-e1d2-496d-b7fa-884371a58682"), "Pending", string.Empty);
            }
        }

        /// <summary>
        /// Bestätigt
        /// </summary>
        public static SelectValue Confirmed
        {
            get
            {
                return new SelectValue(Guid.Parse("34a52363-4a57-4019-abcf-0c9880246891"), "Confirmed", string.Empty);
            }
        }

        /// <summary>
        /// Teilnahme erwünscht
        /// </summary>
        public static SelectValue Mandatory
        {
            get
            {
                return new SelectValue(Guid.Parse("a4734d39-cbb9-4635-b3ae-f4e1192a6bd1"), "Mandatory", string.Empty);
            }
        }

        /// <summary>
        /// Teilnahme möglich
        /// </summary>
        public static SelectValue Optional
        {
            get
            {
                return new SelectValue(Guid.Parse("9c0295b7-1b16-4fd6-a7de-ecd724c823b3"), "Optional", string.Empty);
            }
        }

        /// <summary>
        /// Klassik
        /// </summary>
        public static SelectValue ClassicalMusic
        {
            get
            {
                return new SelectValue(Guid.Parse("87a541e7-706a-47f3-99b3-8b2d6de7a134"), "Classical Music", string.Empty);
            }
        }

        /// <summary>
        /// Crossover
        /// </summary>
        public static SelectValue Crossover
        {
            get
            {
                return new SelectValue(Guid.Parse("5b57a267-f331-41df-995a-93b60fc206ff"), "Crossover", string.Empty);
            }
        }

        /// <summary>
        /// Kammermusik
        /// </summary>
        public static SelectValue ChamberMusic
        {
            get
            {
                return new SelectValue(Guid.Parse("43d8eafa-ef3f-4034-8c88-9a0b68c33ab1"), "Chamber Music", string.Empty);
            }
        }

        /// <summary>
        /// Geplant
        /// </summary>
        public static SelectValue Scheduled
        {
            get
            {
                return new SelectValue(Guid.Parse("c76de830-3746-449a-8f1f-bd5d9233655c"), "Scheduled", string.Empty);
            }
        }

        /// <summary>
        /// Verworfen
        /// </summary>
        public static SelectValue Refused
        {
            get
            {
                return new SelectValue(Guid.Parse("99d192e1-332a-494e-b821-075be14211be"), "Refused", string.Empty);
            }
        }

        /// <summary>
        /// Umfrage abwarten
        /// </summary>
        public static SelectValue AwaitingPoll
        {
            get
            {
                return new SelectValue(Guid.Parse("5e3edcf4-863b-433b-ae72-b6bb7e4dfc95"), "Awaiting Poll", string.Empty);
            }
        }

        /// <summary>
        /// Besprechung
        /// </summary>
        public static SelectValue Meeting
        {
            get
            {
                return new SelectValue(Guid.Parse("ae6dc389-93eb-4d96-bd66-c61dd81155ea"), "Meeting", string.Empty);
            }
        }

        /// <summary>
        /// Bühneneinweisung
        /// </summary>
        public static SelectValue StageBriefing
        {
            get
            {
                return new SelectValue(Guid.Parse("61dd102e-d449-40e1-8c6b-4ead99403ac1"), "Stage Briefing", string.Empty);
            }
        }

        /// <summary>
        /// Choreoprobe
        /// </summary>
        public static SelectValue ChoreographyRehearsal
        {
            get
            {
                return new SelectValue(Guid.Parse("8f64e072-6523-4158-b92e-5c38c8ebca59"), "Choreography Rehearsal", string.Empty);
            }
        }

        /// <summary>
        /// Fototermin
        /// </summary>
        public static SelectValue PhotoSession
        {
            get
            {
                return new SelectValue(Guid.Parse("404f1bfd-2819-47c2-a78b-f3dbd4bc8953"), "Photo Session", string.Empty);
            }
        }

        /// <summary>
        /// Konzert
        /// </summary>
        public static SelectValue Concert
        {
            get
            {
                return new SelectValue(Guid.Parse("71779748-6d3c-496a-9842-8dc508de6676"), "Concert", string.Empty);
            }
        }

        /// <summary>
        /// Kurs
        /// </summary>
        public static SelectValue Workshop
        {
            get
            {
                return new SelectValue(Guid.Parse("5d50c5c3-e85a-4810-ac46-49572e1ca2f5"), "Workshop", string.Empty);
            }
        }

        /// <summary>
        /// Party
        /// </summary>
        public static SelectValue Party
        {
            get
            {
                return new SelectValue(Guid.Parse("79de43be-57cc-484f-bff2-57f3ba78dbe9"), "Photo Session", string.Empty);
            }
        }

        /// <summary>
        /// Probe
        /// </summary>
        public static SelectValue Rehearsal
        {
            get
            {
                return new SelectValue(Guid.Parse("130f63c3-9d2f-4301-b062-236c78663e3b"), "Rehearsal", string.Empty);
            }
        }

        /// <summary>
        /// Probewochenende Chor
        /// </summary>
        public static SelectValue RehearsalWeekendChoir
        {
            get
            {
                return new SelectValue(Guid.Parse("efb2b680-c904-481a-ba7c-9e6a64a998c3"), "Rehearsal Weekend Choir", string.Empty);
            }
        }

        /// <summary>
        /// Show
        /// </summary>
        public static SelectValue Show
        {
            get
            {
                return new SelectValue(Guid.Parse("52d67a48-e99f-4c2f-ac9b-0302d5d3e518"), "Show", string.Empty);
            }
        }

        /// <summary>
        /// Show schauen
        /// </summary>
        public static SelectValue WatchShow
        {
            get
            {
                return new SelectValue(Guid.Parse("d6848ef8-51c6-44e3-bc29-af1df87afcc1"), "Watch Show", string.Empty);
            }
        }

        /// <summary>
        /// Siehe Kommentar
        /// </summary>
        public static SelectValue SeeComment
        {
            get
            {
                return new SelectValue(Guid.Parse("dfe6e73e-9a15-4094-80a5-151a64f3b4db"), "See Comment", string.Empty);
            }
        }

        /// <summary>
        /// Stimmbildung
        /// </summary>
        public static SelectValue VoiceFormation
        {
            get
            {
                return new SelectValue(Guid.Parse("a0b98a79-7c74-4093-8f5f-79003cad219a"), "Voice Formation", string.Empty);
            }
        }

        /// <summary>
        /// Stimmprobe
        /// </summary>
        public static SelectValue SectionalRehearsal
        {
            get
            {
                return new SelectValue(Guid.Parse("4418bfea-0e79-4f76-9e20-527644f654e0"), "Sectional Rehearsal", string.Empty);
            }
        }

        /// <summary>
        /// Transfer
        /// </summary>
        public static SelectValue Transfer
        {
            get
            {
                return new SelectValue(Guid.Parse("3a6218de-6dfc-4aa9-a2a7-f1da20fd61cb"), "Transfer", string.Empty);
            }
        }

        /// <summary>
        /// Versammlung
        /// </summary>
        public static SelectValue Assembly
        {
            get
            {
                return new SelectValue(Guid.Parse("7c894293-82c2-4320-82f5-f77955feae5a"), "Assembly", string.Empty);
            }
        }

        /// <summary>
        /// Vorsingen
        /// </summary>
        public static SelectValue Audition
        {
            get
            {
                return new SelectValue(Guid.Parse("a85738d9-e68e-4584-bac8-ccca8d539636"), "Audition", string.Empty);
            }
        }

        /// <summary>
        /// Sonstiges
        /// </summary>
        public static SelectValue Other
        {
            get
            {
                return new SelectValue(Guid.Parse("e030b53e-3615-4cd6-9fe6-0d818632a4b0"), "Other", string.Empty);
            }
        }

        /// <summary>
        /// Sonderfall
        /// </summary>
        public static SelectValue SpecialCase
        {
            get
            {
                return new SelectValue(Guid.Parse("2567e7be-5a5a-4671-b5ad-765c1e80fd41"), "Special Case", string.Empty);
            }
        }

        /// <summary>
        /// Glöckner 2018
        /// </summary>
        public static SelectValue Gloeckner2018
        {
            get
            {
                return new SelectValue(Guid.Parse("b60d04e0-9841-41c9-9d24-976c8363a33d"), "Glöckner 2018", string.Empty);
            }
        }

        /// <summary>
        /// Orchester Konzertpauschale 1808
        /// </summary>
        public static SelectValue OrchestraConcertLumpSum1808
        {
            get
            {
                return new SelectValue(Guid.Parse("ddb23793-af96-4ea6-9b27-5e2dcfc90b65"), "Orchestra Concert Lump Sum 1808", string.Empty);
            }
        }

        /// <summary>
        /// Orchester Konzertpauschale 9€/11€ bei 10h
        /// </summary>
        public static SelectValue OrchestraConcertLumpSum10h
        {
            get
            {
                return new SelectValue(Guid.Parse("d91def3e-4c55-42c7-ac56-147846be6bfa"), "Orchestra Concert Lump Sum 9€/11€ at 10h", string.Empty);
            }
        }

        /// <summary>
        /// Orchester Konzertpauschale 9€/11€ bei 12h
        /// </summary>
        public static SelectValue OrchestraConcertLumpSum12h
        {
            get
            {
                return new SelectValue(Guid.Parse("a10ce98a-b903-4dca-801d-3afb07711877"), "Orchestra Concert Lump Sum 9 €/11€ at 12h", string.Empty);
            }
        }

        /// <summary>
        /// Orchester Probe Stundensatz 9/11
        /// </summary>
        public static SelectValue OrchestraRehearsalHourlyRate
        {
            get
            {
                return new SelectValue(Guid.Parse("717a27d5-2ef3-4266-92a8-84b3600115eb"), "Orchestra Rehearsal Hourly Rate 9/11", string.Empty);
            }
        }

        /// <summary>
        /// Privat
        /// </summary>
        public static SelectValue Private
        {
            get
            {
                return new SelectValue(Guid.Parse("608b5583-a8dc-48d7-8afa-ef87ca0327f0"), "Private", string.Empty);
            }
        }

        /// <summary>
        /// Arbeit
        /// </summary>
        public static SelectValue Work
        {
            get
            {
                return new SelectValue(Guid.Parse("db1d2c88-a7b3-41c3-a17f-4fd7fe9faca5"), "Work", string.Empty);
            }
        }


        /// <summary>
        /// Konzertreise (Tour)
        /// </summary>
        public static SelectValue ConcertTour
        {
            get
            {
                return new SelectValue(Guid.Parse("7f6b69f3-4fe8-4b0c-a586-38a661c60af5"), "Concert tour", string.Empty);
            }
        }

        /// <summary>
        /// Probewochenende
        /// </summary>
        public static SelectValue RehearsalWeekend
        {
            get
            {
                return new SelectValue(Guid.Parse("63a6b9a9-30a8-4cdb-983b-336b587069cb"), "Rehearsal weekend", string.Empty);
            }
        }

        /// <summary>
        /// Sonderprojekt
        /// </summary>
        public static SelectValue SpecialProject
        {
            get
            {
                return new SelectValue(Guid.Parse("f2a6ef3d-bb32-4505-83a5-2cb9f611ce0f"), "Special project", string.Empty);
            }
        }

        /// <summary>
        /// CD-Aufnahme
        /// </summary>
        public static SelectValue CDRecording
        {
            get
            {
                return new SelectValue(Guid.Parse("52fad37d-23a7-4515-9b77-3ee3bda03b9a"), "CD recording", string.Empty);
            }
        }

        /// <summary>
        /// Wettbewerb
        /// </summary>
        public static SelectValue Contest
        {
            get
            {
                return new SelectValue(Guid.Parse("95de5380-4027-4b73-b4db-3697aba5ba38"), "Contest", string.Empty);
            }
        }
    }
}

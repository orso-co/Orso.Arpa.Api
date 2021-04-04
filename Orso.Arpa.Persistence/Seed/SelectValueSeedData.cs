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
                    Present,
                    Absent,
                    Inapplicable,
                    Ambiguous,
                    AwaitingScan,
                    Yes,
                    No,
                    Partly,
                    DontKnowYet,
                    Pending,
                    Confirmed,
                    Cancelled,
                    Postponed,
                    Archived,
                    Mandatory,
                    Optional,
                    ClassicalMusic,
                    Crossover,
                    ChamberMusic,
                    Scheduled,
                    Refused,
                    AwaitingPoll,
                    Meeting,
                    StageBriefing,
                    ChoreographyRehearsal,
                    PhotoSession,
                    Concert,
                    Workshop,
                    Party,
                    Rehearsal,
                    RehearsalWeekendChoir,
                    Show,
                    WatchShow,
                    SeeComment,
                    VoiceFormation,
                    SectionalRehearsal,
                    Transfer,
                    Assembly,
                    Audition,
                    Other,
                    SpecialCase,
                    Gloeckner2018,
                    OrchestraConcertLumpSum1808,
                    OrchestraConcertLumpSum10h,
                    OrchestraConcertLumpSum12h,
                    OrchestraRehearsalHourlyRate,
                    Private,
                    Work,
                    ConcertTour,
                    RehearsalWeekend,
                    SpecialProject,
                    CDRecording,
                    Contest,
                    Amateur,
                    Student,
                    SemiProfessional,
                    Professional,
                    Unknown,
                    Without,
                    WithStrict,
                    WithNegotiable,
                    Gladly,
                    EmergencyOnly,
                    NeverAgain,
                    ForContactsOnly,
                    FilmMusic,
                    DancePerformance,
                    ContemporaryMusic,
                    Passed,
                    Failed,
                    Awaiting,
                    IsAskingForPianist,
                    NoPianistNeeded,
                    BringsPianist,
                    Unnecessary
                };
            }
        }

        /// <summary>
        /// Anwesend
        /// </summary>
        public static SelectValue Present => new(Guid.Parse("313445ca-57fa-45f0-8515-325949d60726"), "Present", string.Empty);

        /// <summary>
        /// Nicht anwesend
        /// </summary>
        public static SelectValue Absent => new(Guid.Parse("f0f26735-b796-4a70-a20c-92e0e2910bb4"), "Absent", string.Empty);

        /// <summary>
        /// Unzutreffend
        /// </summary>
        public static SelectValue Inapplicable => new(Guid.Parse("86bf6480-787a-4fe0-9d79-0f8d0d36acc4"), "Inapplicable", string.Empty);

        /// <summary>
        /// Unklar
        /// </summary>
        public static SelectValue Ambiguous => new(Guid.Parse("66a6446a-7191-4f14-9c5d-052891b9c5a2"), "Ambiguous", string.Empty);

        /// <summary>
        /// Eintrag nach Scan
        /// </summary>
        public static SelectValue AwaitingScan => new(Guid.Parse("5d31f1f7-73fd-42a4-a429-33fab925b15d"), "Awaiting Scan", string.Empty);

        /// <summary>
        /// Ja
        /// </summary>
        public static SelectValue Yes => new(Guid.Parse("75a017d3-dca5-49ec-9bbd-3b01b159ba5b"), "Yes", string.Empty);

        /// <summary>
        /// Nein
        /// </summary>
        public static SelectValue No => new(Guid.Parse("88b763ac-8093-4c5d-a881-85be1fb8a24d"), "No", string.Empty);

        /// <summary>
        /// Teilweise
        /// </summary>
        public static SelectValue Partly => new(Guid.Parse("1e60dfdf-e7c9-4378-b1af-dcb53fe20022"), "Partly", string.Empty);

        /// <summary>
        /// Ich weiß es noch nicht
        /// </summary>
        public static SelectValue DontKnowYet => new(Guid.Parse("4ee7d317-6d71-4d6e-b45a-954c8c7dcf03"), "Don't know yet", string.Empty);

        public static SelectValue Pending => new(Guid.Parse("362efd25-e1d2-496d-b7fa-884371a58682"), "Pending", string.Empty);

        /// <summary>
        /// Bestätigt
        /// </summary>
        public static SelectValue Confirmed => new(Guid.Parse("34a52363-4a57-4019-abcf-0c9880246891"), "Confirmed", string.Empty);

        /// <summary>
        /// Abgesagt
        /// </summary>
        public static SelectValue Cancelled => new(Guid.Parse("33bbdccf-59a9-4b05-bdac-af50137cecb3"), "Cancelled", string.Empty);

        /// <summary>
        /// Verschoben
        /// </summary>
        public static SelectValue Postponed => new(Guid.Parse("bd0f37e1-ec14-4d87-8380-117b4480d7a4"), "Postponed", string.Empty);

        /// <summary>
        /// Archiviert
        /// </summary>
        public static SelectValue Archived => new(Guid.Parse("425f1526-0513-4535-bdd8-47632d82956f"), "Archived", string.Empty);

        /// <summary>
        /// Teilnahme erwünscht
        /// </summary>
        public static SelectValue Mandatory => new(Guid.Parse("a4734d39-cbb9-4635-b3ae-f4e1192a6bd1"), "Mandatory", string.Empty);

        /// <summary>
        /// Teilnahme möglich
        /// </summary>
        public static SelectValue Optional => new(Guid.Parse("9c0295b7-1b16-4fd6-a7de-ecd724c823b3"), "Optional", string.Empty);

        /// <summary>
        /// Klassik
        /// </summary>
        public static SelectValue ClassicalMusic => new(Guid.Parse("87a541e7-706a-47f3-99b3-8b2d6de7a134"), "Classical Music", string.Empty);

        /// <summary>
        /// Crossover
        /// </summary>
        public static SelectValue Crossover => new(Guid.Parse("5b57a267-f331-41df-995a-93b60fc206ff"), "Crossover", string.Empty);

        /// <summary>
        /// Kammermusik
        /// </summary>
        public static SelectValue ChamberMusic => new(Guid.Parse("43d8eafa-ef3f-4034-8c88-9a0b68c33ab1"), "Chamber Music", string.Empty);

        /// <summary>
        /// Geplant
        /// </summary>
        public static SelectValue Scheduled => new(Guid.Parse("c76de830-3746-449a-8f1f-bd5d9233655c"), "Scheduled", string.Empty);

        /// <summary>
        /// Verworfen
        /// </summary>
        public static SelectValue Refused => new(Guid.Parse("99d192e1-332a-494e-b821-075be14211be"), "Refused", string.Empty);

        /// <summary>
        /// Umfrage abwarten
        /// </summary>
        public static SelectValue AwaitingPoll => new(Guid.Parse("5e3edcf4-863b-433b-ae72-b6bb7e4dfc95"), "Awaiting Poll", string.Empty);

        /// <summary>
        /// Besprechung
        /// </summary>
        public static SelectValue Meeting => new(Guid.Parse("ae6dc389-93eb-4d96-bd66-c61dd81155ea"), "Meeting", string.Empty);

        /// <summary>
        /// Bühneneinweisung
        /// </summary>
        public static SelectValue StageBriefing => new(Guid.Parse("61dd102e-d449-40e1-8c6b-4ead99403ac1"), "Stage Briefing", string.Empty);

        /// <summary>
        /// Choreoprobe
        /// </summary>
        public static SelectValue ChoreographyRehearsal => new(Guid.Parse("8f64e072-6523-4158-b92e-5c38c8ebca59"), "Choreography Rehearsal", string.Empty);

        /// <summary>
        /// Fototermin
        /// </summary>
        public static SelectValue PhotoSession => new(Guid.Parse("404f1bfd-2819-47c2-a78b-f3dbd4bc8953"), "Photo Session", string.Empty);

        /// <summary>
        /// Konzert
        /// </summary>
        public static SelectValue Concert => new(Guid.Parse("71779748-6d3c-496a-9842-8dc508de6676"), "Concert", string.Empty);

        /// <summary>
        /// Kurs
        /// </summary>
        public static SelectValue Workshop => new(Guid.Parse("5d50c5c3-e85a-4810-ac46-49572e1ca2f5"), "Workshop", string.Empty);

        /// <summary>
        /// Party
        /// </summary>
        public static SelectValue Party => new(Guid.Parse("79de43be-57cc-484f-bff2-57f3ba78dbe9"), "Photo Session", string.Empty);

        /// <summary>
        /// Probe
        /// </summary>
        public static SelectValue Rehearsal => new(Guid.Parse("130f63c3-9d2f-4301-b062-236c78663e3b"), "Rehearsal", string.Empty);

        /// <summary>
        /// Probewochenende Chor
        /// </summary>
        public static SelectValue RehearsalWeekendChoir => new(Guid.Parse("efb2b680-c904-481a-ba7c-9e6a64a998c3"), "Rehearsal Weekend Choir", string.Empty);

        /// <summary>
        /// Show
        /// </summary>
        public static SelectValue Show => new(Guid.Parse("52d67a48-e99f-4c2f-ac9b-0302d5d3e518"), "Show", string.Empty);

        /// <summary>
        /// Show schauen
        /// </summary>
        public static SelectValue WatchShow => new(Guid.Parse("d6848ef8-51c6-44e3-bc29-af1df87afcc1"), "Watch Show", string.Empty);

        /// <summary>
        /// Siehe Kommentar
        /// </summary>
        public static SelectValue SeeComment => new(Guid.Parse("dfe6e73e-9a15-4094-80a5-151a64f3b4db"), "See Comment", string.Empty);

        /// <summary>
        /// Stimmbildung
        /// </summary>
        public static SelectValue VoiceFormation => new(Guid.Parse("a0b98a79-7c74-4093-8f5f-79003cad219a"), "Voice Formation", string.Empty);

        /// <summary>
        /// Stimmprobe
        /// </summary>
        public static SelectValue SectionalRehearsal => new(Guid.Parse("4418bfea-0e79-4f76-9e20-527644f654e0"), "Sectional Rehearsal", string.Empty);

        /// <summary>
        /// Transfer
        /// </summary>
        public static SelectValue Transfer => new(Guid.Parse("3a6218de-6dfc-4aa9-a2a7-f1da20fd61cb"), "Transfer", string.Empty);

        /// <summary>
        /// Versammlung
        /// </summary>
        public static SelectValue Assembly => new(Guid.Parse("7c894293-82c2-4320-82f5-f77955feae5a"), "Assembly", string.Empty);

        /// <summary>
        /// Vorsingen
        /// </summary>
        public static SelectValue Audition => new(Guid.Parse("a85738d9-e68e-4584-bac8-ccca8d539636"), "Audition", string.Empty);

        /// <summary>
        /// Sonstiges
        /// </summary>
        public static SelectValue Other => new(Guid.Parse("e030b53e-3615-4cd6-9fe6-0d818632a4b0"), "Other", string.Empty);

        /// <summary>
        /// Sonderfall
        /// </summary>
        public static SelectValue SpecialCase => new(Guid.Parse("2567e7be-5a5a-4671-b5ad-765c1e80fd41"), "Special Case", string.Empty);

        /// <summary>
        /// Glöckner 2018
        /// </summary>
        public static SelectValue Gloeckner2018 => new(Guid.Parse("b60d04e0-9841-41c9-9d24-976c8363a33d"), "Glöckner 2018", string.Empty);

        /// <summary>
        /// Orchester Konzertpauschale 1808
        /// </summary>
        public static SelectValue OrchestraConcertLumpSum1808 => new(Guid.Parse("ddb23793-af96-4ea6-9b27-5e2dcfc90b65"), "Orchestra Concert Lump Sum 1808", string.Empty);

        /// <summary>
        /// Orchester Konzertpauschale 9€/11€ bei 10h
        /// </summary>
        public static SelectValue OrchestraConcertLumpSum10h => new(Guid.Parse("d91def3e-4c55-42c7-ac56-147846be6bfa"), "Orchestra Concert Lump Sum 9€/11€ at 10h", string.Empty);

        /// <summary>
        /// Orchester Konzertpauschale 9€/11€ bei 12h
        /// </summary>
        public static SelectValue OrchestraConcertLumpSum12h => new(Guid.Parse("a10ce98a-b903-4dca-801d-3afb07711877"), "Orchestra Concert Lump Sum 9 €/11€ at 12h", string.Empty);

        /// <summary>
        /// Orchester Probe Stundensatz 9/11
        /// </summary>
        public static SelectValue OrchestraRehearsalHourlyRate => new(Guid.Parse("717a27d5-2ef3-4266-92a8-84b3600115eb"), "Orchestra Rehearsal Hourly Rate 9/11", string.Empty);

        /// <summary>
        /// Privat
        /// </summary>
        public static SelectValue Private => new(Guid.Parse("608b5583-a8dc-48d7-8afa-ef87ca0327f0"), "Private", string.Empty);

        /// <summary>
        /// Arbeit
        /// </summary>
        public static SelectValue Work => new(Guid.Parse("db1d2c88-a7b3-41c3-a17f-4fd7fe9faca5"), "Work", string.Empty);

        /// <summary>
        /// Konzertreise (Tour)
        /// </summary>
        public static SelectValue ConcertTour => new(Guid.Parse("7f6b69f3-4fe8-4b0c-a586-38a661c60af5"), "Concert tour", string.Empty);

        /// <summary>
        /// Probewochenende
        /// </summary>
        public static SelectValue RehearsalWeekend => new(Guid.Parse("63a6b9a9-30a8-4cdb-983b-336b587069cb"), "Rehearsal weekend", string.Empty);

        /// <summary>
        /// Sonderprojekt
        /// </summary>
        public static SelectValue SpecialProject => new(Guid.Parse("f2a6ef3d-bb32-4505-83a5-2cb9f611ce0f"), "Special project", string.Empty);

        /// <summary>
        /// CD-Aufnahme
        /// </summary>
        public static SelectValue CDRecording => new(Guid.Parse("52fad37d-23a7-4515-9b77-3ee3bda03b9a"), "CD recording", string.Empty);

        /// <summary>
        /// Wettbewerb
        /// </summary>
        public static SelectValue Contest => new(Guid.Parse("95de5380-4027-4b73-b4db-3697aba5ba38"), "Contest", string.Empty);

        public static SelectValue Amateur => new(Guid.Parse("3f93768e-ac24-4741-9eb8-49d1e8e4a6e1"), "Amateur", string.Empty);

        public static SelectValue Student => new(Guid.Parse("e20ff004-aafc-4e28-87f9-0d9c6372951c"), "Student", string.Empty);

        public static SelectValue SemiProfessional => new(Guid.Parse("35d63f30-8704-47d5-865a-ee713a082433"), "Semi-Professional", string.Empty);

        public static SelectValue Professional => new(Guid.Parse("f52b9170-c6f6-4828-b96c-df5dfbe9bd73"), "Professional", string.Empty);

        public static SelectValue Unknown => new(Guid.Parse("b67d1ac5-80ec-4b7d-bcb8-72e3da55f201"), "Unknown", string.Empty);

        public static SelectValue Without => new(Guid.Parse("3c014654-b4c9-4c7a-a251-ae88ad504c8a"), "Without", string.Empty);

        public static SelectValue WithStrict => new(Guid.Parse("dec26aef-f0de-4c9f-a164-e23e2543c987"), "With - strict", string.Empty);

        public static SelectValue WithNegotiable => new(Guid.Parse("d53b4a35-f472-42a1-ab22-c7afb1e7d77e"), "With - negotiable", string.Empty);

        public static SelectValue Gladly => new(Guid.Parse("1f0e9a86-4641-4d7e-8413-a1beba0e8afb"), "Gladly", string.Empty);

        public static SelectValue EmergencyOnly => new(Guid.Parse("5850e103-4ac9-472e-85f2-cddc08732ccc"), "Emergency only", string.Empty);

        public static SelectValue NeverAgain => new(Guid.Parse("5db547d6-c115-4409-8db7-59374ca2af83"), "Never again", string.Empty);

        public static SelectValue ForContactsOnly => new(Guid.Parse("0d1073cd-f6d5-4572-87ac-98ab6f15c05a"), "For contacts only", string.Empty);

        public static SelectValue FilmMusic => new(Guid.Parse("a3be7b91-7548-492e-99dc-2788497f2930"), "Film Music", string.Empty);
        public static SelectValue DancePerformance => new(Guid.Parse("982a9947-c6f8-4c9a-b96f-2a4825a11496"), "Dance Performance", string.Empty);
        public static SelectValue ContemporaryMusic => new(Guid.Parse("2ecfb104-feb3-406a-b741-0ac9fdd3e8d7"), "Contemporary Music", string.Empty);
        public static SelectValue Passed => new(Guid.Parse("166edc65-9915-4836-b0a3-3c60ad0bcc04"), "Passed", string.Empty);
        public static SelectValue Failed => new(Guid.Parse("33e57595-2166-4cce-aa34-60d7148ae9f7"), "Failed", string.Empty);
        public static SelectValue Awaiting => new(Guid.Parse("42f546ab-1b96-4eab-88a4-753cad8392c1"), "Awaiting", string.Empty);
        public static SelectValue Unnecessary => new(Guid.Parse("6307ec0e-482a-4777-8b2e-4e8cd5d1f252"), "Unnecessary", string.Empty);
        public static SelectValue IsAskingForPianist => new(Guid.Parse("45d534e3-6605-42f0-ae57-1a943e18a9cd"), "Is asking for pianist", string.Empty);
        public static SelectValue BringsPianist => new(Guid.Parse("0141e712-7080-4e3d-8145-44a3080aa274"), "Brings pianist", string.Empty);
        public static SelectValue NoPianistNeeded => new(Guid.Parse("6bdf5666-65ef-475a-9c48-9a38f18de041"), "No pianist needed", string.Empty);
    }
}

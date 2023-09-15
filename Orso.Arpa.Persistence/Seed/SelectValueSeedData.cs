using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.SelectValueDomain.Model;

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
                    Ambiguous,
                    Yes,
                    No,
                    Pending,
                    Confirmed,
                    Cancelled,
                    Mandatory,
                    Optional,
                    ClassicalMusic,
                    Crossover,
                    ChamberMusic,
                    Meeting,
                    StageBriefing,
                    WarmUpRehearsal,
                    Soundcheck,
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
                    VocalCoaching,
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
                    Business,
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
                    FilmMusic,
                    DancePerformance,
                    ContemporaryMusic,
                    Passed,
                    Failed,
                    Awaiting,
                    IsAskingForPianist,
                    NoPianistNeeded,
                    BringsPianist,
                    Unnecessary,
                    CV,
                    LetterOfRecommendation,
                    Diploma,
                    Photo,
                    Video,
                    Audio,
                    PrivateOwnership,
                    NeedToBorrow,
                    ProvisionByStaff,
                    Solo,
                    High,
                    Low,
                    Coach,
                    Tutti,
                    SectionLead,
                    ConcertMaster,
                    SecondConcertMaster,
                    OrchestraPiano,
                    PrivateLesson,
                    MusicSchool,
                    University,
                    Conservatory,
                    MasterClass,
                    EnsemblePosition,
                    SoloPerformance,
                    CompetitionPrize,
                    Recommendation,
                    Male,
                    Female,
                    Diverse,
                    BankAccountExpired,
                    IncorrectBankDetails,
                    ReturnDebitReceived,
                    OtherSeeCommentField
                };
            }
        }

        /// <summary>
        /// Unklar
        /// </summary>
        public static SelectValue Ambiguous => new(Guid.Parse("66a6446a-7191-4f14-9c5d-052891b9c5a2"), "Ambiguous", string.Empty);

        /// <summary>
        /// Ja
        /// </summary>
        public static SelectValue Yes => new(Guid.Parse("75a017d3-dca5-49ec-9bbd-3b01b159ba5b"), "Yes", string.Empty);

        /// <summary>
        /// Nein
        /// </summary>
        public static SelectValue No => new(Guid.Parse("88b763ac-8093-4c5d-a881-85be1fb8a24d"), "No", string.Empty);

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
        /// Teilnahme erwünscht
        /// </summary>
        public static SelectValue Mandatory => new(Guid.Parse("a4734d39-cbb9-4635-b3ae-f4e1192a6bd1"), "Mandatory", string.Empty);

        /// <summary>
        /// Teilnahme möglich
        /// </summary>
        public static SelectValue Optional => new(Guid.Parse("9c0295b7-1b16-4fd6-a7de-ecd724c823b3"), "Optional", string.Empty);

        // TERMINTYPUS (PROBEN)

        /// <summary>
        /// Probe
        /// </summary>
        public static SelectValue Rehearsal => new(Guid.Parse("130f63c3-9d2f-4301-b062-236c78663e3b"), "Rehearsal", string.Empty);

        /// <summary>
        /// Stimmprobe
        /// </summary>
        public static SelectValue SectionalRehearsal => new(Guid.Parse("4418bfea-0e79-4f76-9e20-527644f654e0"), "Sectional Rehearsal", string.Empty);

        /// <summary>
        /// Probewochenende allgemein
        /// </summary>
        public static SelectValue RehearsalWeekend => new(Guid.Parse("63a6b9a9-30a8-4cdb-983b-336b587069cb"), "Rehearsal Weekend", string.Empty);

        /// <summary>
        /// Probewochenende Chor
        /// </summary>
        public static SelectValue RehearsalWeekendChoir => new(Guid.Parse("efb2b680-c904-481a-ba7c-9e6a64a998c3"), "Rehearsal Weekend Choir", string.Empty);

        /// <summary>
        /// Stimmbildung
        /// </summary>
        public static SelectValue VocalCoaching => new(Guid.Parse("a0b98a79-7c74-4093-8f5f-79003cad219a"), "Vocal Coaching", string.Empty);

        /// <summary>
        /// Choreoprobe
        /// </summary>
        public static SelectValue ChoreographyRehearsal => new(Guid.Parse("8f64e072-6523-4158-b92e-5c38c8ebca59"), "Choreography Rehearsal", string.Empty);

        /// <summary>
        /// Soundcheck
        /// </summary>
        public static SelectValue Soundcheck => new(Guid.Parse("b83d5412-65c1-49fe-a53c-d13a01063438"), "Soundcheck", string.Empty);

        /// <summary>
        /// Anspielprobe
        /// </summary>
        public static SelectValue WarmUpRehearsal => new(Guid.Parse("3f89bf0b-f17d-4439-b64f-ae7eee660ac4"), "Warm-Up Rehearsal", string.Empty);

        /// <summary>
        /// Konzert
        /// </summary>
        public static SelectValue Concert => new(Guid.Parse("71779748-6d3c-496a-9842-8dc508de6676"), "Concert", string.Empty);

        /// <summary>
        /// Konzertreise (Tour)
        /// </summary>
        public static SelectValue ConcertTour => new(Guid.Parse("7f6b69f3-4fe8-4b0c-a586-38a661c60af5"), "Concert Tour", string.Empty);

        /// <summary>
        /// Sonderprojekt
        /// </summary>
        public static SelectValue SpecialProject => new(Guid.Parse("f2a6ef3d-bb32-4505-83a5-2cb9f611ce0f"), "Special Project", string.Empty);

        /// <summary>
        /// CD-Aufnahme
        /// </summary>
        public static SelectValue CDRecording => new(Guid.Parse("52fad37d-23a7-4515-9b77-3ee3bda03b9a"), "CD Recording", string.Empty);

        /// <summary>
        /// Wettbewerb
        /// </summary>
        public static SelectValue Contest => new(Guid.Parse("95de5380-4027-4b73-b4db-3697aba5ba38"), "Contest", string.Empty);

        /// <summary>
        /// Besprechung
        /// </summary>
        public static SelectValue Meeting => new(Guid.Parse("ae6dc389-93eb-4d96-bd66-c61dd81155ea"), "Meeting", string.Empty);

        /// <summary>
        /// Bühneneinweisung
        /// </summary>
        public static SelectValue StageBriefing => new(Guid.Parse("61dd102e-d449-40e1-8c6b-4ead99403ac1"), "Stage Briefing", string.Empty);

        /// <summary>
        /// Fototermin
        /// </summary>
        public static SelectValue PhotoSession => new(Guid.Parse("404f1bfd-2819-47c2-a78b-f3dbd4bc8953"), "Photo Session", string.Empty);

        /// <summary>
        /// Kurs
        /// </summary>
        public static SelectValue Workshop => new(Guid.Parse("5d50c5c3-e85a-4810-ac46-49572e1ca2f5"), "Workshop", string.Empty);

        /// <summary>
        /// Party
        /// </summary>
        public static SelectValue Party => new(Guid.Parse("79de43be-57cc-484f-bff2-57f3ba78dbe9"), "Photo Session", string.Empty);

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

        // VERGÜTUNGSSÄTZE

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
        public static SelectValue OrchestraConcertLumpSum1808 => new(Guid.Parse("ddb23793-af96-4ea6-9b27-5e2dcfc90b65"), "Orchestra Concert Rate: 1808", string.Empty);

        /// <summary>
        /// Orchester Konzertpauschale 9€/11€ bei 10h
        /// </summary>
        public static SelectValue OrchestraConcertLumpSum10h => new(
            Guid.Parse("d91def3e-4c55-42c7-ac56-147846be6bfa"),
            "Orchestra Concert Rate: 9€/11€ at 10h",
            string.Empty);

        /// <summary>
        /// Orchester Konzertpauschale 9€/11€ bei 12h
        /// </summary>
        public static SelectValue OrchestraConcertLumpSum12h => new(
            Guid.Parse("a10ce98a-b903-4dca-801d-3afb07711877"),
            "Orchestra Concert Rate: 9€/11€ at 12h",
            string.Empty);

        /// <summary>
        /// Orchester Probe Stundensatz 9/11
        /// </summary>
        public static SelectValue OrchestraRehearsalHourlyRate => new(
            Guid.Parse("717a27d5-2ef3-4266-92a8-84b3600115eb"),
            "Orchestra Rehearsal Hourly Rate 9/11",
            string.Empty);

        // KONTAKTKONTEXT

        /// <summary>
        /// Privat
        /// </summary>
        public static SelectValue Private => new(Guid.Parse("608b5583-a8dc-48d7-8afa-ef87ca0327f0"), "Private", string.Empty);

        /// <summary>
        /// Arbeit
        /// </summary>
        public static SelectValue Business => new(Guid.Parse("db1d2c88-a7b3-41c3-a17f-4fd7fe9faca5"), "Business", string.Empty);

        // QUALIFIKATION
        public static SelectValue Amateur => new(Guid.Parse("3f93768e-ac24-4741-9eb8-49d1e8e4a6e1"), "Amateur", string.Empty);
        public static SelectValue Student => new(Guid.Parse("e20ff004-aafc-4e28-87f9-0d9c6372951c"), "Student", string.Empty);
        public static SelectValue SemiProfessional => new(Guid.Parse("35d63f30-8704-47d5-865a-ee713a082433"), "Semi-Professional", string.Empty);
        public static SelectValue Professional => new(Guid.Parse("f52b9170-c6f6-4828-b96c-df5dfbe9bd73"), "Professional", string.Empty);
        public static SelectValue Unknown => new(Guid.Parse("b67d1ac5-80ec-4b7d-bcb8-72e3da55f201"), "Unknown", string.Empty);

        // VERGÜTUNGSANSPRUCH
        public static SelectValue Without => new(Guid.Parse("3c014654-b4c9-4c7a-a251-ae88ad504c8a"), "Without", string.Empty);
        public static SelectValue WithStrict => new(Guid.Parse("dec26aef-f0de-4c9f-a164-e23e2543c987"), "With - strict", string.Empty);
        public static SelectValue WithNegotiable => new(Guid.Parse("d53b4a35-f472-42a1-ab22-c7afb1e7d77e"), "With - negotiable", string.Empty);

        // GENRES

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

        public static SelectValue FilmMusic => new(Guid.Parse("a3be7b91-7548-492e-99dc-2788497f2930"), "Film Music", string.Empty);
        public static SelectValue DancePerformance => new(Guid.Parse("982a9947-c6f8-4c9a-b96f-2a4825a11496"), "Dance Performance", string.Empty);
        public static SelectValue ContemporaryMusic => new(Guid.Parse("2ecfb104-feb3-406a-b741-0ac9fdd3e8d7"), "Contemporary Music", string.Empty);

        // AUDITION
        public static SelectValue Passed => new(Guid.Parse("166edc65-9915-4836-b0a3-3c60ad0bcc04"), "Passed", string.Empty);
        public static SelectValue Failed => new(Guid.Parse("33e57595-2166-4cce-aa34-60d7148ae9f7"), "Failed", string.Empty);
        public static SelectValue Awaiting => new(Guid.Parse("42f546ab-1b96-4eab-88a4-753cad8392c1"), "Awaiting", string.Empty);
        public static SelectValue Unnecessary => new(Guid.Parse("6307ec0e-482a-4777-8b2e-4e8cd5d1f252"), "Unnecessary", string.Empty);
        public static SelectValue IsAskingForPianist => new(Guid.Parse("45d534e3-6605-42f0-ae57-1a943e18a9cd"), "Is asking for pianist", string.Empty);
        public static SelectValue BringsPianist => new(Guid.Parse("0141e712-7080-4e3d-8145-44a3080aa274"), "Brings pianist", string.Empty);
        public static SelectValue NoPianistNeeded => new(Guid.Parse("6bdf5666-65ef-475a-9c48-9a38f18de041"), "No pianist needed", string.Empty);
        public static SelectValue CV => new(Guid.Parse("c0911d95-0c6d-4834-840c-43cddf3c51a0"), "CV", string.Empty);
        public static SelectValue LetterOfRecommendation => new(Guid.Parse("0cf5b2e2-4f01-441a-adc8-a975c7494fd7"), "Letter of recommendation", string.Empty);
        public static SelectValue Diploma => new(Guid.Parse("c1951202-0e6e-41f7-bf07-5cefe47efade"), "Diploma", string.Empty);
        public static SelectValue Audio => new(Guid.Parse("3550443d-5acf-4159-bd59-d7da04dd9434"), "Audio", string.Empty);
        public static SelectValue Video => new(Guid.Parse("d075dda3-ba29-472b-a699-1f92c1af13a9"), "Video", string.Empty);
        public static SelectValue Photo => new(Guid.Parse("e340f76d-074b-40e8-85b0-1bb66a596a06"), "Photo", string.Empty);

        // WECHSELINSTRUMENT VERFÜGBARKEITSSTATUS
        public static SelectValue PrivateOwnership => new(Guid.Parse("6fbab698-993f-4268-a28e-b1f1599771c5"), "Private ownership", string.Empty);
        public static SelectValue NeedToBorrow => new(Guid.Parse("e7442e9b-8c54-41ed-8607-accba2d04f61"), "Need to borrow", string.Empty);
        public static SelectValue ProvisionByStaff => new(Guid.Parse("28927b59-a999-4f84-abca-4f146888457f"), "Provision by staff", string.Empty);

        // POSITIONSPRÄFERENZEN
        public static SelectValue Solo => new(Guid.Parse("9353f2ee-f074-488b-a359-f2fc6f66da51"), "Solo", string.Empty);
        public static SelectValue High => new(Guid.Parse("a0e02d9f-03b5-49e0-9ae8-b34a419bc203"), "High", string.Empty);
        public static SelectValue Low => new(Guid.Parse("959e5b30-6ad1-4102-8dce-f1395b8ae73e"), "Low", string.Empty);
        public static SelectValue Coach => new(Guid.Parse("a89a8323-4c82-4e55-8ef1-6d7150f564e9"), "Coach", string.Empty);
        public static SelectValue Tutti => new(Guid.Parse("5a4a1318-2f23-45b0-8329-3eec0f446389"), "Tutti", string.Empty);
        public static SelectValue SectionLead => new(Guid.Parse("36c6963d-a08c-4394-823a-1d24ba8330b4"), "Section lead", string.Empty);
        public static SelectValue ConcertMaster => new(Guid.Parse("fc2c8cf2-3189-44de-a124-2debe1d7b057"), "Concert master", string.Empty);
        public static SelectValue SecondConcertMaster => new(Guid.Parse("9ed94828-9deb-49a9-9a65-ecb83620c82e"), "2nd concert master", string.Empty);
        public static SelectValue OrchestraPiano => new(Guid.Parse("ebae975b-d9a3-4d2f-b0a3-beff554e7041"), "Orchestra piano", string.Empty);
        public static SelectValue PrivateLesson => new(Guid.Parse("d73cba63-f92e-4c17-b416-59f8e021fbf2"), "Private lesson", string.Empty);
        public static SelectValue MusicSchool => new(Guid.Parse("d45ac8a2-f17c-49ca-9525-99473771a340"), "Music school", string.Empty);
        public static SelectValue University => new(Guid.Parse("371ee51d-3612-4eb4-b169-25eae26c382f"), "University", string.Empty);
        public static SelectValue Conservatory => new(Guid.Parse("fcad4595-cea8-4339-bc48-312d43d7d4a0"), "Conservatory", string.Empty);
        public static SelectValue MasterClass => new(Guid.Parse("bfdf244d-6d85-41e8-a10f-6f309abe9ffe"), "Master class", string.Empty);
        public static SelectValue EnsemblePosition => new(Guid.Parse("57bf8f44-d6f5-4551-a571-a42565e5861a"), "Ensemble position", string.Empty);
        public static SelectValue SoloPerformance => new(Guid.Parse("8cf0c997-33bd-431b-a28c-7d22c00d8d87"), "Solo performance", string.Empty);
        public static SelectValue CompetitionPrize => new(Guid.Parse("674abb4f-89d1-4802-bfee-8eb0d61bed80"), "Competition / Prize", string.Empty);
        public static SelectValue Recommendation => new(Guid.Parse("64db8d53-128b-4b3d-85ac-23292fad29e9"), "Recommendation", string.Empty);
        public static SelectValue Male => new(Guid.Parse("9c0e9810-f177-43af-9915-9ae4bb962a24"), "Male", string.Empty);
        public static SelectValue Female => new(Guid.Parse("44f40ffd-6afa-4de1-a033-027f59f1bb7e"), "Female", string.Empty);
        public static SelectValue Diverse => new(Guid.Parse("037d90a2-4819-44ca-9089-e0cd5d01af40"), "Diverse", string.Empty);

        /// <summary>
        /// Bankkonto erloschen
        /// </summary>
        public static SelectValue BankAccountExpired => new(Guid.Parse("597bf9bc-4fad-433f-810d-ae4de4ac3bde"), "Bank Account Expired", string.Empty);

        /// <summary>
        /// Rücklastschrift erhalten
        /// </summary>
        public static SelectValue ReturnDebitReceived => new(Guid.Parse("c36e8662-2740-49c7-87dd-3c301ef86909"), "Return Debit Received", string.Empty);

        /// <summary>
        /// Fehlerhafte Bankverbindung
        /// </summary>
        public static SelectValue IncorrectBankDetails => new(Guid.Parse("7efd1bdd-67b5-4706-a1f4-9d67eea05e5d"), "Incorrect Bank Details", string.Empty);

        /// <summary>
        /// Other (see comment field)
        /// </summary>
        public static SelectValue OtherSeeCommentField => new(Guid.Parse("b0f67138-7488-4c68-ad4c-63fce6f862cc"), "Other (see comment field)", string.Empty);
    }
}

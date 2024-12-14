using System;
using System.Collections.Generic;
using Orso.Arpa.Application.SelectValueApplication.Model;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class SelectValueDtoData
    {
        public static IList<SelectValueDto> ProjectGenres
        {
            get
            {
                return
                [
                    ClassicalMusic,
                    Crossover,
                    ChamberMusic,
                    FilmMusic,
                    DancePerformance,
                    ContemporaryMusic,
                ];
            }
        }

        public static SelectValueDto ClassicalMusic => new()
        {
            Id = Guid.Parse("d733e38d-1d80-4054-b654-4ea4a128b0a8"),
            Name = "Classical Music",
            Description = "",
        };

        public static SelectValueDto ChamberMusic => new()
        {
            Id = Guid.Parse("29e1142f-aa9e-4b94-ae21-9a63f7b65c15"),
            Name = "Chamber Music",
            Description = "",
        };

        public static SelectValueDto ContemporaryMusic => new()
        {
            Id = Guid.Parse("4ef47024-d8a5-4b2d-8584-aeb29263dddb"),
            Name = "Contemporary Music",
            Description = "",
        };

        public static SelectValueDto Crossover => new()
        {
            Id = Guid.Parse("e7e78e76-3850-4eb5-892f-d90be6c256a4"),
            Name = "Crossover",
            Description = "",
        };

        public static SelectValueDto DancePerformance => new()
        {
            Id = Guid.Parse("8daa5ae4-3885-4739-803a-693c7cfdf314"),
            Name = "Dance Performance",
            Description = "",
        };

        public static SelectValueDto FilmMusic => new()
        {
            Id = Guid.Parse("5578f637-14b7-4c11-85a8-0b94d83da678"),
            Name = "Film Music",
            Description = "",
        };

        public static SelectValueDto Female => new()
        {
            Id = Guid.Parse("32761c45-e481-4eb9-a23e-d73330482572"),
            Name = "Female",
            Description = "",
        };

        public static SelectValueDto Diverse => new()
        {
            Id = Guid.Parse("88d680fe-b6cc-486f-8f79-2525189b8b13"),
            Name = "Diverse",
            Description = "",
        };

        public static SelectValueDto Male => new()
        {
            Id = Guid.Parse("1c16a5fe-6ac6-4e94-be6e-82a0a0fbe1c9"),
            Name = "Male",
            Description = "",
        };

        public static SelectValueDto PrivateContactDetail => new()
        {
            Id = Guid.Parse("f0bf8326-623e-4caa-bd92-bc05c721a6cf"),
            Name = "Private",
            Description = "",
        };

        public static SelectValueDto PrivateAddress => new()
        {
            Id = Guid.Parse("fb44b625-7086-48e6-bcc6-a004dd472012"),
            Name = "Private",
            Description = "",
        };

        public static SelectValueDto Acceptance => new()
        {
            Id = Guid.Parse("eef4a4d1-796b-4b37-96f6-f31dbccf0aeb"),
            Name = "Acceptance",
            Description = "",
        };

        public static SelectValueDto Candidate => new()
        {
            Id = Guid.Parse("b0dcb5e9-bbc6-4004-b9d7-0f6723416b9b"),
            Name = "Candidate",
            Description = "",
        };

        public static SelectValueDto Concert => new()
        {
            Id = Guid.Parse("34f05f05-ef23-4f36-94e7-73b917530c51"),
            Name = "Concert",
            Description = ""
        };

        public static SelectValueDto ConcertTour => new()
        {
            Id = Guid.Parse("7f76d426-cab7-4f4f-aba3-bd430bcec003"),
            Name = "Concert Tour",
            Description = ""
        };

        public static SelectValueDto Pending => new()
        {
            Id = Guid.Parse("725a4f4a-37cb-46ba-93a3-7b9cc2b015cb"),
            Name = "Pending",
            Description = ""
        };

        public static SelectValueDto ProjectConfirmed => new()
        {
            Id = Guid.Parse("b793fa86-2025-4258-8993-8045f4c312d7"),
            Name = "Confirmed",
            Description = ""
        };

        public static SelectValueDto Workshop => new()
        {
            Id = Guid.Parse("ae2f10ff-39ae-427e-a5e8-ddcd89422d44"),
            Name = "Workshop",
            Description = ""
        };

        public static SelectValueDto Cacnelled => new()
        {
            Id = Guid.Parse("65975857-ab27-480d-87c3-dba74d45cb63"),
            Name = "Cancelled",
            Description = ""
        };

        public static SelectValueDto WarmUpRehearsal => new()
        {
            Id = Guid.Parse("694de886-8566-45d0-afc7-6ded18a2b6e6"),
            Name = "Warm-Up Rehearsal",
            Description = ""
        };

        public static SelectValueDto AppointmentConfirmed => new()
        {
            Id = Guid.Parse("36176b7e-0926-43d6-b19a-72838ccd2acd"),
            Name = "Confirmed",
            Description = ""
        };

        public static SelectValueDto RehearsalWeekendChoir => new()
        {
            Id = Guid.Parse("5b89cf6e-0194-4e01-bb32-8b1813a51e16"),
            Name = "Rehearsal Weekend Choir",
            Description = ""
        };

        public static SelectValueDto Refused => new()
        {
            Id = Guid.Parse("0126fded-0a82-4b53-85e4-1c04a4f79296"),
            Name = "Refused",
            Description = ""
        };

        public static SelectValueDto SectionalRehearsal => new()
        {
            Id = Guid.Parse("2634c0cc-31d2-4f61-813d-7ec60fc8ab34"),
            Name = "Sectional Rehearsal",
            Description = ""
        };

        public static SelectValueDto Scheduled => new()
        {
            Id = Guid.Parse("93033f7e-a3c1-45e3-8a17-021d0a4abe5a"),
            Name = "Scheduled",
            Description = ""
        };

        public static SelectValueDto Rehearsal => new()
        {
            Id = Guid.Parse("86672779-5e70-4965-b59c-032086d00914"),
            Name = "Rehearsal",
            Description = ""
        };

        public static SelectValueDto VoiceRehearsal => new() {
            Id = Guid.Parse("9814654f-b7af-42f9-a77c-434899714652"),
            Name = "Voice Rehearsal",
            Description = ""
        };

        public static SelectValueDto Tutti => new() {
            Id = Guid.Parse("4ed969a3-ba48-4116-b934-6ff1bb6719ac"),
            Name = "Tutti",
            Description = ""
        };

        public static SelectValueDto Choir => new() {
            Id = Guid.Parse("cb08b618-a2f0-4c5b-872c-6b6821453429"),
            Name = "Choir",
            Description = ""
        };

        public static SelectValueDto PrivateOwnership => new() {
            Id = Guid.Parse("d33ea034-0c5f-458d-bef5-26d2c12b6b03"),
            Name = "Private Ownership",
            Description = ""
        };
    }
}

using System;
using System.Collections.Generic;
using Orso.Arpa.Application.AppointmentApplication;
using Orso.Arpa.Application.AppointmentParticipationApplication;
using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class AppointmentDtoData
    {
        public static IList<AppointmentDto> AppointmentsForPerformer
        {
            get
            {
                return new List<AppointmentDto>
                {
                    RockingXMasRehearsalForPerformer,
                    AfterShowPartyForPerformer,
                    RockingXMasConcert,
                    StaffMeeting,
                    PhotoSession,
                    RehearsalWeekend,
                    AuditionDays
                };
            }
        }

        public static IList<AppointmentDto> AppointmentsForStaff
        {
            get
            {
                return new List<AppointmentDto>
                {
                    RockingXMasRehearsalForStaff,
                    AfterShowPartyForStaff,
                    RockingXMasConcert,
                    StaffMeeting,
                    PhotoSession,
                    RehearsalWeekend,
                    AuditionDays
                };
            }
        }

        public static AppointmentDto RockingXMasRehearsalForPerformer
        {
            get
            {
                AppointmentDto dto = RockingXMasRehearsalBase;
                dto.InternalDetails = null;
                dto.Projects.Add(ProjectDtoData.RockingXMasForPerformer);
                return dto;
            }
        }

        public static AppointmentDto RockingXMasRehearsalForStaff
        {
            get
            {
                AppointmentDto dto = RockingXMasRehearsalBase;
                dto.Projects.Add(ProjectDtoData.RockingXMasForStaff);
                return dto;
            }
        }

        private static AppointmentDto RockingXMasRehearsalBase
        {
            get
            {
                var dto = new AppointmentDto
                {
                    Id = Guid.Parse("41579f23-d545-4b10-96ab-842f9893a2d3"),
                    CategoryId = Guid.Parse("c1b6d08b-f31e-4f38-a8c0-761e42fbd2b7"),
                    SalaryId = Guid.Parse("88da1c17-9efc-4f69-ba0f-39c76592845b"),
                    SalaryPatternId = Guid.Parse("8b51c75f-d597-48ef-8451-5f5fc32d57d1"),
                    EndTime = new DateTime(2019, 12, 21, 18, 30, 0),
                    StartTime = new DateTime(2019, 12, 21, 10, 0, 0),
                    InternalDetails = "I need more coffee",
                    PublicDetails = "Let's rock",
                    Name = "Rocking X-mas Dress Rehearsal",
                    StatusId = Guid.Parse("36176b7e-0926-43d6-b19a-72838ccd2acd"),
                    CreatedBy = "anonymous",
                    ExpectationId = Guid.Parse("b09bc4a6-06ab-4d45-8b82-7971e662ccb5"),
                    VenueId = Guid.Parse("54eb30ff-6ea3-4026-8a49-5f149c8ec7e1"),
                    CreatedAt = FakeDateTime.UtcNow
                };
                dto.Participations.Add(PerformerParticipation);
                dto.Participations.Add(StaffParticipation);
                dto.Participations.Add(AdminParticipation);
                return dto;
            }
        }

        public static AppointmentParticipationListItemDto PerformerParticipation
        {
            get
            {
                return new AppointmentParticipationListItemDto
                {
                    Person = PersonDtoData.Performer,
                    Participation = AppointmentParticipationDtoData.PerformerParticipation,

                    MusicianProfiles = new List<ReducedMusicianProfileDto>
                    {
                        ReducedMusicianProfileDtoData.PerformerProfile,
                    }
                };
            }
        }

        public static AppointmentParticipationListItemDto StaffParticipation
        {
            get
            {
                return new AppointmentParticipationListItemDto
                {
                    Person = PersonDtoData.Staff,
                    Participation = AppointmentParticipationDtoData.StaffParticipation,
                    MusicianProfiles = new List<ReducedMusicianProfileDto>
                    {
                        ReducedMusicianProfileDtoData.StaffProfile1,
                        ReducedMusicianProfileDtoData.StaffProfile2,
                    }
                };
            }
        }

        public static AppointmentParticipationListItemDto AdminParticipation
        {
            get
            {
                return new AppointmentParticipationListItemDto
                {
                    Person = PersonDtoData.Admin,
                    Participation = null,
                    MusicianProfiles = new List<ReducedMusicianProfileDto>
                    {
                        ReducedMusicianProfileDtoData.AdminProfile1,
                    }
                };
            }
        }

        public static AppointmentParticipationListItemDto WithoutRoleParticipation
        {
            get
            {
                return new AppointmentParticipationListItemDto
                {
                    Person = PersonDtoData.WithoutRole,
                    Participation = null,
                    MusicianProfiles = new List<ReducedMusicianProfileDto>
                    {
                        ReducedMusicianProfileDtoData.WithoutRoleProfile,
                    }
                };
            }
        }

        public static AppointmentDto RockingXMasConcert
        {
            get
            {
                return new AppointmentDto
                {
                    Id = Guid.Parse("bcf930c0-18d5-48b4-ab10-d477a8cb822f"),
                    CategoryId = Guid.Parse("dd4556b3-d8b3-4002-8bde-68e327945916"),
                    SalaryId = Guid.Parse("aedc27f3-e2e8-4368-ad69-1ab1c3dd7974"),
                    SalaryPatternId = Guid.Parse("104fc525-bb0b-49dc-b2b2-9a8f63e45c92"),
                    EndTime = new DateTime(2019, 12, 22, 23, 30, 00),
                    StartTime = new DateTime(2019, 12, 22, 20, 00, 00),
                    InternalDetails = "Where is my jacket?",
                    PublicDetails = "Sold out :-)",
                    Name = "Rocking X-mas Concert",
                    StatusId = Guid.Parse("93033f7e-a3c1-45e3-8a17-021d0a4abe5a"),
                    CreatedBy = "anonymous",
                    ExpectationId = Guid.Parse("647f674a-bc2f-4d3a-9ce4-f0aefa98cd9d"),
                    VenueId = Guid.Parse("54eb30ff-6ea3-4026-8a49-5f149c8ec7e1"),
                    CreatedAt = FakeDateTime.UtcNow
                };
            }
        }

        public static AppointmentDto RockingXMasConcertForPerformer
        {
            get
            {
                AppointmentDto dto = RockingXMasConcert;
                dto.InternalDetails = null;
                return dto;
            }
        }

        public static AppointmentDto AfterShowPartyForPerformer
        {
            get
            {
                AppointmentDto dto = AfterShowPartyBase;
                dto.Projects.Add(ProjectDtoData.RockingXMasForPerformer);
                return dto;
            }
        }

        public static AppointmentDto AfterShowPartyForStaff
        {
            get
            {
                AppointmentDto dto = AfterShowPartyBase;
                dto.Projects.Add(ProjectDtoData.RockingXMasForStaff);
                return dto;
            }
        }

        private static AppointmentDto AfterShowPartyBase
        {
            get
            {
                var dto = new AppointmentDto
                {
                    Id = Guid.Parse("2aeb552b-81db-4989-9578-35e1616a4345"),
                    CategoryId = Guid.Parse("ac1ccdd4-39aa-4767-95ea-099a829f275b"),
                    SalaryId = Guid.Parse("5b936e5f-3743-4cc3-91af-0cc8742c846e"),
                    SalaryPatternId = Guid.Parse("f15b88b2-395d-4195-af25-8b8879640baf"),
                    EndTime = new DateTime(2019, 12, 24, 06, 00, 00),
                    StartTime = new DateTime(2019, 12, 24),
                    InternalDetails = "Shake it, baby",
                    PublicDetails = "Get the party started",
                    Name = "Rocking X-mas After Show Party",
                    StatusId = Guid.Parse("0126fded-0a82-4b53-85e4-1c04a4f79296"),
                    CreatedBy = "anonymous",
                    ExpectationId = Guid.Parse("867622fa-7aa5-43f3-b3ef-5290d1f61734"),
                    VenueId = Guid.Parse("54eb30ff-6ea3-4026-8a49-5f149c8ec7e1"),
                    CreatedAt = FakeDateTime.UtcNow
                };
                dto.Sections.Add(SectionDtoData.Alto);
                dto.Rooms.Add(RoomDtoData.AulaWeiherhofSchule);
                AppointmentParticipationListItemDto performerParticipation = PerformerParticipation;
                performerParticipation.Participation = null;
                dto.Participations.Add(performerParticipation);
                AppointmentParticipationListItemDto staffParticipation = StaffParticipation;
                staffParticipation.Participation = null;
                dto.Participations.Add(staffParticipation);
                AppointmentParticipationListItemDto adminParticipation = AdminParticipation;
                adminParticipation.Participation = null;
                dto.Participations.Add(adminParticipation);
                return dto;
            }
        }

        public static AppointmentDto StaffMeeting
        {
            get
            {
                var dto = new AppointmentDto
                {
                    Id = Guid.Parse("cab05507-489c-4f18-aad5-f1c393626860"),
                    CategoryId = Guid.Parse("c1b6d08b-f31e-4f38-a8c0-761e42fbd2b7"),
                    SalaryId = Guid.Parse("aedc27f3-e2e8-4368-ad69-1ab1c3dd7974"),
                    SalaryPatternId = null,
                    EndTime = new DateTime(2020, 12, 22, 23, 30, 00),
                    StartTime = new DateTime(2020, 12, 22, 20, 00, 00),
                    InternalDetails = "Reminder: Don't forget to talk about the summer holidays",
                    PublicDetails = "Meet and greet",
                    Name = "Team Meeting",
                    StatusId = Guid.Parse("36176b7e-0926-43d6-b19a-72838ccd2acd"),
                    CreatedBy = "anonymous",
                    ExpectationId = Guid.Parse("b09bc4a6-06ab-4d45-8b82-7971e662ccb5"),
                    VenueId = Guid.Parse("54eb30ff-6ea3-4026-8a49-5f149c8ec7e1"),
                    CreatedAt = FakeDateTime.UtcNow
                };
                dto.Projects.Add(ProjectDtoData.HoorayForHollywood);
                return dto;
            }
        }

        public static AppointmentDto StaffMeetingForPerformer
        {
            get
            {
                AppointmentDto dto = StaffMeeting;
                dto.InternalDetails = null;
                return dto;
            }
        }

        public static AppointmentDto PhotoSession
        {
            get
            {
                var dto = new AppointmentDto
                {
                    Id = Guid.Parse("6197d4ae-cb53-48db-b407-937b3857c621"),
                    CategoryId = Guid.Parse("e9c79ae9-5498-459d-8852-9f135da7afae"),
                    SalaryId = Guid.Parse("aedc27f3-e2e8-4368-ad69-1ab1c3dd7974"),
                    SalaryPatternId = null,
                    EndTime = new DateTime(2020, 12, 22, 16, 00, 00),
                    StartTime = new DateTime(2020, 12, 22, 15, 00, 00),
                    InternalDetails = null,
                    PublicDetails = "Photo session for season to come",
                    Name = "Photo session",
                    StatusId = Guid.Parse("36176b7e-0926-43d6-b19a-72838ccd2acd"),
                    CreatedBy = "anonymous",
                    ExpectationId = Guid.Parse("647f674a-bc2f-4d3a-9ce4-f0aefa98cd9d"),
                    VenueId = null,
                    CreatedAt = FakeDateTime.UtcNow
                };
                dto.Sections.Add(SectionDtoData.Choir);
                return dto;
            }
        }

        public static AppointmentDto RehearsalWeekend
        {
            get
            {
                var dto = new AppointmentDto
                {
                    Id = Guid.Parse("f14e47d8-110f-4346-87d2-9a9bc0e2120c"),
                    CategoryId = Guid.Parse("e9c79ae9-5498-459d-8852-9f135da7afae"),
                    SalaryId = Guid.Parse("aedc27f3-e2e8-4368-ad69-1ab1c3dd7974"),
                    SalaryPatternId = null,
                    EndTime = new DateTime(2019, 12, 24, 16, 00, 00),
                    StartTime = new DateTime(2019, 12, 20, 15, 00, 00),
                    InternalDetails = null,
                    PublicDetails = "Accordion rehearsal weekend",
                    Name = "Rehearsal weekend",
                    StatusId = Guid.Parse("36176b7e-0926-43d6-b19a-72838ccd2acd"),
                    CreatedBy = "anonymous",
                    ExpectationId = Guid.Parse("647f674a-bc2f-4d3a-9ce4-f0aefa98cd9d"),
                    VenueId = null,
                    CreatedAt = FakeDateTime.UtcNow
                };
                dto.Sections.Add(SectionDtoData.Accordion);
                return dto;
            }
        }

        public static AppointmentDto AuditionDays
        {
            get
            {
                var dto = new AppointmentDto
                {
                    Id = Guid.Parse("51d24e3b-d258-4855-bc5a-3c05fb661636"),
                    CategoryId = Guid.Parse("e9c79ae9-5498-459d-8852-9f135da7afae"),
                    SalaryId = Guid.Parse("aedc27f3-e2e8-4368-ad69-1ab1c3dd7974"),
                    SalaryPatternId = null,
                    StartTime = new DateTime(2020, 11, 29, 8, 00, 00),
                    EndTime = new DateTime(2020, 12, 2, 17, 00, 00),
                    InternalDetails = null,
                    PublicDetails = "Audition days for piccolo flutes",
                    Name = "Audition days",
                    StatusId = Guid.Parse("36176b7e-0926-43d6-b19a-72838ccd2acd"),
                    CreatedBy = "anonymous",
                    ExpectationId = Guid.Parse("647f674a-bc2f-4d3a-9ce4-f0aefa98cd9d"),
                    VenueId = null,
                    CreatedAt = FakeDateTime.UtcNow
                };
                dto.Sections.Add(SectionDtoData.PiccoloFlute);
                dto.Projects.Add(ProjectDtoData.HoorayForHollywood);
                return dto;
            }
        }
    }
}

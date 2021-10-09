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
        public static IList<AppointmentDto> Appointments
        {
            get
            {
                return new List<AppointmentDto>
                {
                    RockingXMasRehearsal,
                    AfterShowParty,
                    RockingXMasConcert,
                    StaffMeeting,
                    PhotoSession,
                    RehearsalWeekend,
                    AuditionDays
                };
            }
        }

        public static AppointmentDto RockingXMasRehearsal
        {
            get
            {
                var dto = new AppointmentDto
                {
                    Id = Guid.Parse("41579f23-d545-4b10-96ab-842f9893a2d3"),
                    CategoryId = Guid.Parse("86672779-5e70-4965-b59c-032086d00914"),
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
                dto.Projects.Add(ProjectDtoData.RockingXMasForStaff);
                return dto;
            }
        }

        public static AppointmentParticipationListItemDto PerformerParticipation
        {
            get
            {
                return new AppointmentParticipationListItemDto
                {
                    Person = ReducedPersonDtoData.Performer,
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
                    Person = ReducedPersonDtoData.Staff,
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
                    Person = ReducedPersonDtoData.Admin,
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
                    Person = ReducedPersonDtoData.WithoutRole,
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
                var dto = new AppointmentDto
                {
                    Id = Guid.Parse("bcf930c0-18d5-48b4-ab10-d477a8cb822f"),
                    CategoryId = Guid.Parse("2634c0cc-31d2-4f61-813d-7ec60fc8ab34"),
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
                dto.Participations.Add(new AppointmentParticipationListItemDto
                {
                    Person = ReducedPersonDtoData.Performer,
                    MusicianProfiles = new List<ReducedMusicianProfileDto>
                    {
                        ReducedMusicianProfileDtoData.PerformerProfile,
                        ReducedMusicianProfileDtoData.PerformerDeactivatedTubaProfile,
                        ReducedMusicianProfileDtoData.PerformerHornProfile
                    }
                });
                dto.Participations.Add(new AppointmentParticipationListItemDto
                {
                    Person = ReducedPersonDtoData.Staff,
                    MusicianProfiles = new List<ReducedMusicianProfileDto>
                    {
                        ReducedMusicianProfileDtoData.StaffProfile1,
                        ReducedMusicianProfileDtoData.StaffProfile2
                    }
                });
                dto.Participations.Add(new AppointmentParticipationListItemDto
                {
                    Person = ReducedPersonDtoData.Admin,
                    MusicianProfiles = new List<ReducedMusicianProfileDto>
                    {
                        ReducedMusicianProfileDtoData.AdminProfile1,
                        ReducedMusicianProfileDtoData.AdminProfile2
                    }
                });
                dto.Participations.Add(new AppointmentParticipationListItemDto
                {
                    Person = ReducedPersonDtoData.WithoutRole,
                    MusicianProfiles = new List<ReducedMusicianProfileDto>
                    {
                        ReducedMusicianProfileDtoData.WithoutRoleProfile
                    }
                });

                return dto;
            }
        }

        public static AppointmentDto AfterShowParty
        {
            get
            {
                var dto = new AppointmentDto
                {
                    Id = Guid.Parse("2aeb552b-81db-4989-9578-35e1616a4345"),
                    CategoryId = Guid.Parse("5b89cf6e-0194-4e01-bb32-8b1813a51e16"),
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
                dto.Projects.Add(ProjectDtoData.RockingXMasForStaff);
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
                    CategoryId = Guid.Parse("86672779-5e70-4965-b59c-032086d00914"),
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

        public static AppointmentDto PhotoSession
        {
            get
            {
                var dto = new AppointmentDto
                {
                    Id = Guid.Parse("6197d4ae-cb53-48db-b407-937b3857c621"),
                    CategoryId = Guid.Parse("694de886-8566-45d0-afc7-6ded18a2b6e6"),
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
                dto.Participations.Add(new AppointmentParticipationListItemDto
                {
                    Person = ReducedPersonDtoData.Performer,
                    MusicianProfiles = new List<ReducedMusicianProfileDto>
                    {
                        ReducedMusicianProfileDtoData.PerformerProfile
                    }
                });
                dto.Participations.Add(new AppointmentParticipationListItemDto
                {
                    Person = ReducedPersonDtoData.Staff,
                    MusicianProfiles = new List<ReducedMusicianProfileDto>
                    {
                        ReducedMusicianProfileDtoData.StaffProfile1,
                        ReducedMusicianProfileDtoData.StaffProfile2
                    }
                });
                dto.Participations.Add(new AppointmentParticipationListItemDto
                {
                    Person = ReducedPersonDtoData.Admin,
                    MusicianProfiles = new List<ReducedMusicianProfileDto>
                    {
                        ReducedMusicianProfileDtoData.AdminProfile1
                    }
                });
                dto.Participations.Add(new AppointmentParticipationListItemDto
                {
                    Person = ReducedPersonDtoData.WithoutRole,
                    MusicianProfiles = new List<ReducedMusicianProfileDto>
                    {
                        ReducedMusicianProfileDtoData.WithoutRoleProfile
                    }
                });
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
                    CategoryId = Guid.Parse("694de886-8566-45d0-afc7-6ded18a2b6e6"),
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
                    CategoryId = Guid.Parse("694de886-8566-45d0-afc7-6ded18a2b6e6"),
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

        public static AppointmentDto SopranoRehearsal
        {
            get
            {
                var dto = new AppointmentDto
                {
                    Id = Guid.Parse("869cb371-e77e-4ffe-84a9-cdf852e31358"),
                    SalaryId = Guid.Parse("aedc27f3-e2e8-4368-ad69-1ab1c3dd7974"),
                    StatusId = Guid.Parse("36176b7e-0926-43d6-b19a-72838ccd2acd"),
                    CategoryId = Guid.Parse("694de886-8566-45d0-afc7-6ded18a2b6e6"),
                    PublicDetails = "Hooray for Hollywood rehearsal for soprano voices only",
                    CreatedAt = FakeDateTime.UtcNow,
                    CreatedBy = "anonymous",
                    Name = "Soprano rehearsal",
                    ExpectationId = Guid.Parse("647f674a-bc2f-4d3a-9ce4-f0aefa98cd9d"),
                    StartTime = new DateTime(2021, 12, 30, 8, 00, 00),
                    EndTime = new DateTime(2021, 12, 30, 17, 00, 00),
                };
                dto.Sections.Add(SectionDtoData.Soprano);
                dto.Projects.Add(ProjectDtoData.RockingXMasForStaff);
                dto.Participations.Add(new AppointmentParticipationListItemDto
                {
                    Person = ReducedPersonDtoData.Admin,
                    MusicianProfiles = new List<ReducedMusicianProfileDto>
                    {
                        ReducedMusicianProfileDtoData.AdminProfile1,
                    }
                });
                return dto;
            }
        }

        public static AppointmentDto AltoRehearsal
        {
            get
            {
                var dto = new AppointmentDto
                {
                    Id = Guid.Parse("af02e789-fb96-4d69-b252-e1c91c23c2fe"),
                    StatusId = Guid.Parse("36176b7e-0926-43d6-b19a-72838ccd2acd"),
                    SalaryId = Guid.Parse("aedc27f3-e2e8-4368-ad69-1ab1c3dd7974"),
                    StartTime = new DateTime(2021, 12, 29, 8, 00, 00),
                    EndTime = new DateTime(2021, 12, 29, 17, 00, 00),
                    PublicDetails = "Hooray for Hollywood rehearsal for alto voices only",
                    Name = "Alto rehearsal",
                    ExpectationId = Guid.Parse("647f674a-bc2f-4d3a-9ce4-f0aefa98cd9d"),
                    CategoryId = Guid.Parse("694de886-8566-45d0-afc7-6ded18a2b6e6"),
                    CreatedBy = "anonymous",
                    CreatedAt = FakeDateTime.UtcNow
                };
                dto.Sections.Add(SectionDtoData.Alto);
                dto.Projects.Add(ProjectDtoData.HoorayForHollywood);
                return dto;
            }
        }
    }
}

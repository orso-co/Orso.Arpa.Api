using System;
using System.Collections.Generic;
using Orso.Arpa.Application.AppointmentApplication;
using Orso.Arpa.Application.AppointmentParticipationApplication;
using Orso.Arpa.Application.MusicianProfileApplication;

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
                    RockingXMasRehearsal
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
                    CategoryId = Guid.Parse("c1b6d08b-f31e-4f38-a8c0-761e42fbd2b7"),
                    EmolumentId = Guid.Parse("88da1c17-9efc-4f69-ba0f-39c76592845b"),
                    EmolumentPatternId = Guid.Parse("8b51c75f-d597-48ef-8451-5f5fc32d57d1"),
                    EndTime = new DateTime(2019, 12, 21, 18, 30, 0),
                    StartTime = new DateTime(2019, 12, 21, 10, 0, 0),
                    InternalDetails = "I need more coffee",
                    PublicDetails = "Let's rock",
                    Name = "Rocking X-mas Dress Rehearsal",
                    StatusId = Guid.Parse("36176b7e-0926-43d6-b19a-72838ccd2acd"),
                    CreatedBy = "anonymous",
                    ExpectationId = Guid.Parse("b09bc4a6-06ab-4d45-8b82-7971e662ccb5"),
                    VenueId = Guid.Parse("54eb30ff-6ea3-4026-8a49-5f149c8ec7e1"),
                    CreatedAt = new DateTime(2021, 1, 1)
                };
                dto.Participations.Add(PerformerParticipation);
                dto.Participations.Add(StaffParticipation);
                dto.Participations.Add(AdminParticipation);
                dto.Projects.Add(ProjectDtoData.RockingXMas);
                return dto;
            }
        }

        private static AppointmentParticipationListItemDto PerformerParticipation
        {
            get
            {
                return new AppointmentParticipationListItemDto
                {
                    Person = PersonDtoData.Performer,
                    Participation = AppointmentParticipationDtoData.PerformerParticipation,
                    MusicianProfiles = new List<MusicianProfileDto>
                    {
                        MusicianProfileDtoData.PerformerProfile
                    }
                };
            }
        }

        private static AppointmentParticipationListItemDto StaffParticipation
        {
            get
            {
                return new AppointmentParticipationListItemDto
                {
                    Person = PersonDtoData.Staff,
                    Participation = AppointmentParticipationDtoData.StaffParticipation,
                    MusicianProfiles = new List<MusicianProfileDto>
                    {
                        MusicianProfileDtoData.StaffProfile1,
                        MusicianProfileDtoData.StaffProfile2
                    }
                };
            }
        }

        private static AppointmentParticipationListItemDto AdminParticipation
        {
            get
            {
                return new AppointmentParticipationListItemDto
                {
                    Person = PersonDtoData.Admin,
                    Participation = null,
                    MusicianProfiles = new List<MusicianProfileDto>
                    {
                        MusicianProfileDtoData.AdminProfile1
                    }
                };
            }
        }
    }
}

using System;
using System.Collections.Generic;
using Orso.Arpa.Application.MeApplication.Model;
using Orso.Arpa.Domain.AppointmentDomain.Enums;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class UserAppointmentDtoTestData
    {
        public static IList<MyAppointmentDto> PerformerUserAppointments
        {
            get
            {
                return new List<MyAppointmentDto>
                {
                    AltoRehearsal,
                    TeamMeeting,
                    PhotoSession,
                    RockingXMasAfterShowParty,
                    RockingXMasConcert,
                    RockingXMasDressRehearsal
                };
            }
        }

        /// <summary>
        /// Matching project
        /// </summary>
        public static MyAppointmentDto RockingXMasDressRehearsal
        {
            get
            {
                var dto = new MyAppointmentDto
                {
                    CreatedBy = "anonymous",
                    EndTime = new DateTime(2019, 12, 21, 18, 30, 0),
                    Expectation = "Mandatory",
                    Id = Guid.Parse("41579f23-d545-4b10-96ab-842f9893a2d3"),
                    Name = "Rocking X-mas Dress Rehearsal",
                    PublicDetails = "Let's rock",
                    StartTime = new DateTime(2019, 12, 21, 10, 0, 0),
                    Venue = VenueDtoData.WeiherhofSchule,
                    Prediction = AppointmentParticipationPrediction.Yes,
                    CreatedAt = FakeDateTime.UtcNow,
                    Category = SelectValueDtoData.Rehearsal,
                    Status = AppointmentStatus.Confirmed,
                    CommentByPerformerInner = "Werde wahrscheinlich etwas früher gehen müssen."
                };
                dto.Projects.Add(ProjectDtoData.RockingXMasForPerformer);

                return dto;
            }
        }

        /// <summary>
        /// Matching section
        /// </summary>
        public static MyAppointmentDto PhotoSession
        {
            get
            {
                return new MyAppointmentDto
                {
                    CreatedBy = "anonymous",
                    StartTime = new DateTime(2020, 12, 22, 15, 00, 00),
                    EndTime = new DateTime(2020, 12, 22, 16, 00, 00),
                    Expectation = "Confirmed",
                    Id = Guid.Parse("6197d4ae-cb53-48db-b407-937b3857c621"),
                    Name = "Photo session",
                    PublicDetails = "Photo session for season to come",
                    Venue = null,
                    Prediction = null,
                    CreatedAt = FakeDateTime.UtcNow,
                    Category = SelectValueDtoData.WarmUpRehearsal,
                    Status = AppointmentStatus.Confirmed
                };
            }
        }

        /// <summary>
        /// Matching section and project
        /// </summary>
        public static MyAppointmentDto RockingXMasAfterShowParty
        {
            get
            {
                var dto = new MyAppointmentDto
                {
                    CreatedBy = "anonymous",
                    StartTime = new DateTime(2019, 12, 24),
                    EndTime = new DateTime(2019, 12, 24, 06, 00, 00),
                    Expectation = "Pending",
                    Id = Guid.Parse("2aeb552b-81db-4989-9578-35e1616a4345"),
                    Name = "Rocking X-mas After Show Party",
                    PublicDetails = "Get the party started",
                    Venue = VenueDtoData.WeiherhofSchule,
                    Prediction = null,
                    CreatedAt = FakeDateTime.UtcNow
                };
                dto.Projects.Add(ProjectDtoData.RockingXMasForPerformer);
                dto.Rooms.Add(RoomDtoData.AulaWeiherhofSchule);
                dto.Category = SelectValueDtoData.RehearsalWeekendChoir;
                dto.Status = AppointmentStatus.Confirmed;
                return dto;
            }
        }

        /// <summary>
        /// Without section and project
        /// </summary>
        public static MyAppointmentDto RockingXMasConcert
        {
            get
            {
                return new MyAppointmentDto
                {
                    CreatedBy = "anonymous",
                    StartTime = new DateTime(2019, 12, 22, 20, 00, 00),
                    EndTime = new DateTime(2019, 12, 22, 23, 30, 00),
                    Expectation = "Confirmed",
                    Id = Guid.Parse("bcf930c0-18d5-48b4-ab10-d477a8cb822f"),
                    Name = "Rocking X-mas Concert",
                    PublicDetails = "Sold out :-)",
                    Venue = VenueDtoData.WeiherhofSchule,
                    Prediction = null,
                    CreatedAt = FakeDateTime.UtcNow,
                    Category = SelectValueDtoData.SectionalRehearsal,
                    Status = AppointmentStatus.Scheduled
                };
            }
        }

        public static MyAppointmentDto AltoRehearsal
        {
            get
            {
                var dto = new MyAppointmentDto
                {
                    CreatedBy = "anonymous",
                    StartTime = new DateTime(2021, 12, 29, 8, 00, 00),
                    EndTime = new DateTime(2021, 12, 29, 17, 00, 00),
                    Expectation = "Confirmed",
                    Id = Guid.Parse("af02e789-fb96-4d69-b252-e1c91c23c2fe"),
                    Name = "Alto rehearsal",
                    PublicDetails = "Hooray for Hollywood rehearsal for alto voices only",
                    Prediction = AppointmentParticipationPrediction.Partly,
                    Result = AppointmentParticipationResult.AwaitingScan,
                    CreatedAt = FakeDateTime.UtcNow,
                    Category = SelectValueDtoData.WarmUpRehearsal,
                    Status = AppointmentStatus.Confirmed
                };
                dto.Projects.Add(ProjectDtoData.HoorayForHollywood);
                return dto;
            }
        }

        public static MyAppointmentDto TeamMeeting
        {
            get
            {
                var dto = new MyAppointmentDto
                {
                    CreatedBy = "anonymous",
                    StartTime = new DateTime(2020, 12, 22, 20, 00, 00),
                    EndTime = new DateTime(2020, 12, 22, 23, 30, 00),
                    Expectation = "Mandatory",
                    Id = Guid.Parse("cab05507-489c-4f18-aad5-f1c393626860"),
                    Name = "Team Meeting",
                    PublicDetails = "Meet and greet",
                    Prediction = null,
                    Result = null,
                    CreatedAt = FakeDateTime.UtcNow,
                    Category = SelectValueDtoData.Rehearsal,
                    Status = AppointmentStatus.Confirmed,
                    Venue = VenueDtoData.WeiherhofSchule
                };
                dto.Projects.Add(ProjectDtoData.HoorayForHollywood);
                return dto;
            }
        }
    }
}

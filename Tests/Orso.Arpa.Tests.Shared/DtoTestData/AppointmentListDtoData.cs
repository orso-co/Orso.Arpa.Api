using System;
using System.Collections.Generic;
using Orso.Arpa.Application.AppointmentApplication;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class AppointmentListDtoData
    {
        public static IList<AppointmentListDto> Appointments
        {
            get
            {
                return new List<AppointmentListDto>
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

        public static AppointmentListDto RockingXMasRehearsal
        {
            get
            {
                return new AppointmentListDto
                {
                    Id = Guid.Parse("41579f23-d545-4b10-96ab-842f9893a2d3"),
                    EndTime = new DateTime(2019, 12, 21, 18, 30, 0),
                    StartTime = new DateTime(2019, 12, 21, 10, 0, 0),
                    Name = "Rocking X-mas Dress Rehearsal",
                    StatusId = Guid.Parse("36176b7e-0926-43d6-b19a-72838ccd2acd"),
                };
            }
        }


        public static AppointmentListDto RockingXMasConcert
        {
            get
            {
                return new AppointmentListDto
                {
                    Id = Guid.Parse("bcf930c0-18d5-48b4-ab10-d477a8cb822f"),
                    EndTime = new DateTime(2019, 12, 22, 23, 30, 00),
                    StartTime = new DateTime(2019, 12, 22, 20, 00, 00),
                    Name = "Rocking X-mas Concert",
                    StatusId = Guid.Parse("93033f7e-a3c1-45e3-8a17-021d0a4abe5a"),
                };
            }
        }

        public static AppointmentListDto AfterShowParty
        {
            get
            {
                return new AppointmentListDto
                {
                    Id = Guid.Parse("2aeb552b-81db-4989-9578-35e1616a4345"),
                    EndTime = new DateTime(2019, 12, 24, 06, 00, 00),
                    StartTime = new DateTime(2019, 12, 24),
                    Name = "Rocking X-mas After Show Party",
                    StatusId = Guid.Parse("0126fded-0a82-4b53-85e4-1c04a4f79296"),
                };
            }
        }

        public static AppointmentListDto StaffMeeting
        {
            get
            {
                return new AppointmentListDto
                {
                    Id = Guid.Parse("cab05507-489c-4f18-aad5-f1c393626860"),
                    EndTime = new DateTime(2020, 12, 22, 23, 30, 00),
                    StartTime = new DateTime(2020, 12, 22, 20, 00, 00),
                    Name = "Team Meeting",
                    StatusId = Guid.Parse("36176b7e-0926-43d6-b19a-72838ccd2acd"),
                };
            }
        }

        public static AppointmentListDto PhotoSession
        {
            get
            {
                return new AppointmentListDto
                {
                    Id = Guid.Parse("6197d4ae-cb53-48db-b407-937b3857c621"),
                    EndTime = new DateTime(2020, 12, 22, 16, 00, 00),
                    StartTime = new DateTime(2020, 12, 22, 15, 00, 00),
                    Name = "Photo session",
                    StatusId = Guid.Parse("36176b7e-0926-43d6-b19a-72838ccd2acd"),
                };
            }
        }

        public static AppointmentListDto RehearsalWeekend
        {
            get
            {
                return new AppointmentListDto
                {
                    Id = Guid.Parse("f14e47d8-110f-4346-87d2-9a9bc0e2120c"),
                    EndTime = new DateTime(2019, 12, 24, 16, 00, 00),
                    StartTime = new DateTime(2019, 12, 20, 15, 00, 00),
                    Name = "Rehearsal weekend",
                    StatusId = Guid.Parse("36176b7e-0926-43d6-b19a-72838ccd2acd"),
                };
            }
        }

        public static AppointmentListDto AuditionDays
        {
            get
            {
                return new AppointmentListDto
                {
                    Id = Guid.Parse("51d24e3b-d258-4855-bc5a-3c05fb661636"),
                    StartTime = new DateTime(2020, 11, 29, 8, 00, 00),
                    EndTime = new DateTime(2020, 12, 2, 17, 00, 00),
                    Name = "Audition days",
                    StatusId = Guid.Parse("36176b7e-0926-43d6-b19a-72838ccd2acd"),
                };
            }
        }

        public static AppointmentListDto SopranoRehearsal
        {
            get
            {
                return new AppointmentListDto
                {
                    Id = Guid.Parse("869cb371-e77e-4ffe-84a9-cdf852e31358"),
                    Name = "Soprano rehearsal",
                    StartTime = new DateTime(2021, 12, 30, 8, 00, 00),
                    EndTime = new DateTime(2021, 12, 30, 17, 00, 00),
                    StatusId = Guid.Parse("36176b7e-0926-43d6-b19a-72838ccd2acd"),
                };
            }
        }

        public static AppointmentListDto AltoRehearsal
        {
            get
            {
                return new AppointmentListDto
                {
                    Id = Guid.Parse("af02e789-fb96-4d69-b252-e1c91c23c2fe"),
                    StartTime = new DateTime(2021, 12, 29, 8, 00, 00),
                    EndTime = new DateTime(2021, 12, 29, 17, 00, 00),
                    Name = "Alto rehearsal",
                    StatusId = Guid.Parse("36176b7e-0926-43d6-b19a-72838ccd2acd"),
                };
            }
        }
    }
}

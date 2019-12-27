using System;
using System.Collections.Generic;
using Orso.Arpa.Application.Dtos;

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
                return new AppointmentDto
                {
                    Id = Guid.Parse("41579f23-d545-4b10-96ab-842f9893a2d3"),
                    CategoryId = Guid.Parse("c1b6d08b-f31e-4f38-a8c0-761e42fbd2b7"),
                    EmolumentId = Guid.Parse("88da1c17-9efc-4f69-ba0f-39c76592845b"),
                    EmolumentPatternId = Guid.Parse("8b51c75f-d597-48ef-8451-5f5fc32d57d1"),
                    EndTime = "2019-12-21T18:30:00Z",
                    StartTime = "2019-12-21T10:00:00Z",
                    InternalDetails = "I need more coffee",
                    PublicDetails = "Let's rock",
                    Name = "Rocking X-mas Dress Rehearsal",
                    StatusId = Guid.Parse("36176b7e-0926-43d6-b19a-72838ccd2acd"),
                    CreatedBy = "anonymous"
                };
            }
        }
    }
}

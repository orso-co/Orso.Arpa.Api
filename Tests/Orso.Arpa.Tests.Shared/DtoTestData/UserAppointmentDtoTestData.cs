using System;
using System.Collections.Generic;
using Orso.Arpa.Application.MeApplication;

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
                    PerformerUserAppointment
                };
            }
        }

        public static MyAppointmentDto PerformerUserAppointment
        {
            get
            {
                var dto = new MyAppointmentDto
                {
                    CreatedBy = "anonymous",
                    EndTime = new DateTime(2019,12,21,18,30,0),
                    Expectation = "Mandatory",
                    Id = Guid.Parse("41579f23-d545-4b10-96ab-842f9893a2d3"),
                    Name = "Rocking X-mas Dress Rehearsal",
                    PublicDetails = "Let's rock",
                    StartTime = new DateTime(2019,12,21,10,0,0),
                    Venue = VenueDtoData.WeiherhofSchule,
                    PredictionId = Guid.Parse("319d508e-a6e2-437e-b48b-6be51e3459bd"),
                    CreatedAt = new DateTime(2021, 1, 1)
                };
                dto.Projects.Add(ProjectDtoData.RockingXMas);

                return dto;
            }
        }
    }
}

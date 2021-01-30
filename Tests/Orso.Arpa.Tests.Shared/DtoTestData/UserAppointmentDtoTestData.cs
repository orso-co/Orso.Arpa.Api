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
                    EndTime = "2019-12-21T18:30:00Z",
                    Expectation = "Mandatory",
                    Id = Guid.Parse("41579f23-d545-4b10-96ab-842f9893a2d3"),
                    Name = "Rocking X-mas Dress Rehearsal",
                    PublicDetails = "Let's rock",
                    StartTime = "2019-12-21T10:00:00Z",
                    Venue = VenueDtoData.WeiherhofSchule,
                    PredictionId = Guid.Parse("319d508e-a6e2-437e-b48b-6be51e3459bd")
                };
                dto.Projects.Add(ProjectDtoData.RockingXMas);

                return dto;
            }
        }
    }
}

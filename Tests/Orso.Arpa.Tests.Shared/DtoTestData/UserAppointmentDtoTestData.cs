using System;
using System.Collections.Generic;
using Orso.Arpa.Application.Logic.Me;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class UserAppointmentDtoTestData
    {
        public static IList<UserAppointmentDto> OrsianerUserAppointments
        {
            get
            {
                return new List<UserAppointmentDto>
                {
                    OrsianerUserAppointment
                };
            }
        }

        public static UserAppointmentDto OrsianerUserAppointment
        {
            get
            {
                var dto = new UserAppointmentDto
                {
                    CreatedBy = "anonymous",
                    EndTime = "2019-12-21T18:30:00Z",
                    Expectation = "Mandatory",
                    Id = Guid.Parse("41579f23-d545-4b10-96ab-842f9893a2d3"),
                    Name = "Rocking X-mas Dress Rehearsal",
                    PublicDetails = "Let's rock",
                    StartTime = "2019-12-21T10:00:00Z",
                    Venue = VenueDtoData.WeiherhofSchule
                };
                dto.Projects.Add(ProjectDtoData.RockingXMas);

                return dto;
            }
        }
    }
}

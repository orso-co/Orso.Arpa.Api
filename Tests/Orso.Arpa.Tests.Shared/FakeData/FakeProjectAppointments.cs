using System;
using Orso.Arpa.Domain.ProjectDomain.Model;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Tests.Shared.FakeData
{
    public static class FakeProjectAppointments
    {
        public static ProjectAppointment RockingXMasRehearsal
        {
            get
            {
                var projectAppointment = new ProjectAppointment(
                    Guid.Parse("9aff7490-9c61-46c2-be8e-b9b802a95232"),
                    Guid.Parse("a19d84f1-4ac1-49c3-abfe-527092b80b6d"),
                    Guid.Parse("41579f23-d545-4b10-96ab-842f9893a2d3"));
                projectAppointment.SetProperty(nameof(projectAppointment.Appointment), AppointmentSeedData.RockingXMasRehearsal);
                return projectAppointment;
            }
        }
    }
}

using System.Linq;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Misc;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Tests.Shared.FakeData
{
    public static class FakeAppointments
    {
        public static Appointment RockingXMasRehearsal
        {
            get
            {
                Appointment appointment = AppointmentSeedData.RockingXMasRehearsal;
                ProjectAppointment projectAppointment = appointment.ProjectAppointments.First();
                projectAppointment.SetProperty(nameof(ProjectAppointment.Project), FakeProjects.RockingXMas);
                appointment.SetProperty(nameof(Appointment.Venue), FakeVenues.WeiherhofSchule);
                appointment.SetProperty(nameof(Appointment.Expectation), FakeSelectValueMappings.Mandatory);
                appointment.SetProperty(nameof(Appointment.CreatedBy), "anonymous");
                appointment.SetProperty(nameof(Appointment.CreatedAt), DateTimeProvider.Instance.GetUtcNow());
                return appointment;
            }
        }
    }
}

using System.Linq;
using Orso.Arpa.Domain.AppointmentDomain.Enums;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.ProjectDomain.Model;
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
                appointment.SetProperty(nameof(Appointment.CreatedAt), FakeDateTime.UtcNow);
                appointment.SetProperty(nameof(Appointment.Category), FakeSelectValueMappings.Rehearsal);
                appointment.SetProperty(nameof(Appointment.Status), AppointmentStatus.Confirmed);
                return appointment;
            }
        }
    }
}

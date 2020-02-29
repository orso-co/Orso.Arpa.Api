using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.Appointments;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.Extensions;

namespace Orso.Arpa.Tests.Shared.TestSeedData
{
    public static class AppointmentSeedData
    {
        public static IList<Appointment> Appointments
        {
            get
            {
                return new List<Appointment>
                {
                    RockingXMasRehearsal,
                    RockingXMasConcert,
                    AfterShowParty
                };
            }
        }

        public static Appointment RockingXMasRehearsal
        {
            get
            {
                var id = Guid.Parse("41579f23-d545-4b10-96ab-842f9893a2d3");
                var appointment = new Appointment
                (
                    id,
                    new Create.Command
                    {
                        CategoryId = SelectValueMappingSeedData.AppointmentCategoryMappings[0].Id,
                        StatusId = SelectValueMappingSeedData.AppointmentStatusMappings[0].Id,
                        EmolumentId = SelectValueMappingSeedData.AppointmentEmolumentMappings[0].Id,
                        EmolumentPatternId = SelectValueMappingSeedData.AppointmentEmolumentPatternMappings[0].Id,
                        StartTime = new DateTime(2019, 12, 21, 10, 00, 00),
                        EndTime = new DateTime(2019, 12, 21, 18, 30, 00),
                        PublicDetails = "Let's rock",
                        InternalDetails = "I need more coffee",
                        Name = "Rocking X-mas Dress Rehearsal",
                        ExpectationId = SelectValueMappingSeedData.AppointmentExpectationMappings[2].Id
                    }
                );
                appointment.ProjectAppointments.Add(new ProjectAppointment(ProjectSeedData.RockingXMas.Id, id));
                appointment.SetProperty(nameof(Appointment.VenueId), Guid.Parse("54eb30ff-6ea3-4026-8a49-5f149c8ec7e1"));
                return appointment;
            }
        }

        public static Appointment RockingXMasConcert
        {
            get
            {
                var id = Guid.Parse("bcf930c0-18d5-48b4-ab10-d477a8cb822f");
                var appointment = new Appointment
                (
                    id,
                    new Create.Command
                    {
                        CategoryId = SelectValueMappingSeedData.AppointmentCategoryMappings[1].Id,
                        StatusId = SelectValueMappingSeedData.AppointmentStatusMappings[1].Id,
                        EmolumentId = SelectValueMappingSeedData.AppointmentEmolumentMappings[1].Id,
                        EmolumentPatternId = SelectValueMappingSeedData.AppointmentEmolumentPatternMappings[1].Id,
                        StartTime = new DateTime(2019, 12, 22, 20, 00, 00),
                        EndTime = new DateTime(2019, 12, 22, 23, 30, 00),
                        PublicDetails = "Sold out :-)",
                        InternalDetails = "Where is my jacket?",
                        Name = "Rocking X-mas Concert",
                        ExpectationId = SelectValueMappingSeedData.AppointmentExpectationMappings[1].Id
                    }
                );
                appointment.SetProperty(nameof(Appointment.VenueId), Guid.Parse("54eb30ff-6ea3-4026-8a49-5f149c8ec7e1"));
                return appointment;
            }
        }

        public static Appointment AfterShowParty
        {
            get
            {
                var appointmentId = Guid.Parse("2aeb552b-81db-4989-9578-35e1616a4345");
                var appointment = new Appointment
                (
                    appointmentId,
                    new Create.Command
                    {
                        CategoryId = SelectValueMappingSeedData.AppointmentCategoryMappings[2].Id,
                        StatusId = SelectValueMappingSeedData.AppointmentStatusMappings[2].Id,
                        EmolumentId = SelectValueMappingSeedData.AppointmentEmolumentMappings[2].Id,
                        EmolumentPatternId = SelectValueMappingSeedData.AppointmentEmolumentPatternMappings[2].Id,
                        StartTime = new DateTime(2019, 12, 24),
                        EndTime = new DateTime(2019, 12, 24, 06, 00, 00),
                        PublicDetails = "Let the party started",
                        InternalDetails = "Shake it, baby",
                        Name = "Rocking X-mas After Show Party",
                        ExpectationId = SelectValueMappingSeedData.AppointmentExpectationMappings[0].Id
                    }
                );
                appointment.ProjectAppointments.Add(new ProjectAppointment(ProjectSeedData.RockingXMas.Id, appointmentId));
                appointment.AppointmentRooms.Add(new AppointmentRoom(appointmentId, RoomSeedData.AulaWeiherhofSchule.Id));
                appointment.SectionAppointments.Add(new SectionAppointment(SectionSeedData.Alto.Id, appointmentId));
                appointment.SetProperty(nameof(Appointment.VenueId), Guid.Parse("54eb30ff-6ea3-4026-8a49-5f149c8ec7e1"));
                return appointment;
            }
        }
    }
}

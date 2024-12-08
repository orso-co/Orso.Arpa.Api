using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.AppointmentDomain.Commands;
using Orso.Arpa.Domain.AppointmentDomain.Enums;
using Orso.Arpa.Domain.AppointmentDomain.Model;

namespace Orso.Arpa.Tests.Shared.TestSeedData
{
    public static class AppointmentParticipationSeedData
    {
        public static IList<AppointmentParticipation> AppointmentParticipations
        {
            get
            {
                return
                [
                    PerformerParticipationRockingXMasRehearsal,
                    PerformerParticipationAltoRehearsal,
                    StaffParticipation
                ];
            }
        }

        public static AppointmentParticipation PerformerParticipationRockingXMasRehearsal
        {
            get
            {
                return new AppointmentParticipation(Guid.Parse("9e230c76-759b-466f-8cff-6e77e53aa754"), new CreateAppointmentParticipation.Command
                {
                    AppointmentId = AppointmentSeedData.RockingXMasRehearsal.Id,
                    PersonId = PersonTestSeedData.Performer.Id,
                    Prediction = AppointmentParticipationPrediction.Yes,
                    Result = null,
                    CommentByPerformerInner = "Werde wahrscheinlich etwas früher gehen müssen."
                });
            }
        }

        public static AppointmentParticipation PerformerParticipationAltoRehearsal
        {
            get
            {
                return new AppointmentParticipation(Guid.Parse("fd2a48d5-9df0-41f1-86b6-1e5f9606ecab"), new CreateAppointmentParticipation.Command
                {
                    AppointmentId = AppointmentSeedData.AltoRehearsal.Id,
                    PersonId = PersonTestSeedData.Performer.Id,
                    Prediction = AppointmentParticipationPrediction.Partly,
                    Result = AppointmentParticipationResult.AwaitingScan
                });
            }
        }

        public static AppointmentParticipation StaffParticipation
        {
            get
            {
                return new AppointmentParticipation(Guid.Parse("16f63cc0-36c2-4a3f-93d1-2c2c3aa15ab4"), new CreateAppointmentParticipation.Command
                {
                    AppointmentId = AppointmentSeedData.RockingXMasRehearsal.Id,
                    PersonId = PersonTestSeedData.Staff.Id,
                    Prediction = AppointmentParticipationPrediction.Partly,
                    Result = AppointmentParticipationResult.Present
                });
            }
        }
    }
}

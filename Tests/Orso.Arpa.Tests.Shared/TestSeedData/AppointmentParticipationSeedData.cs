using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Domain.Logic.AppointmentParticipations;

namespace Orso.Arpa.Tests.Shared.TestSeedData
{
    public static class AppointmentParticipationSeedData
    {
        public static IList<AppointmentParticipation> AppointmentParticipations
        {
            get
            {
                return new List<AppointmentParticipation>
                {
                    PerformerParticipationRockingXMasRehearsal,
                    PerformerParticipationAltoRehearsal,
                    StaffParticipation
                };
            }
        }

        public static AppointmentParticipation PerformerParticipationRockingXMasRehearsal
        {
            get
            {
                return new AppointmentParticipation(Guid.Parse("9e230c76-759b-466f-8cff-6e77e53aa754"), new Create.Command
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
                return new AppointmentParticipation(Guid.Parse("fd2a48d5-9df0-41f1-86b6-1e5f9606ecab"), new Create.Command
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
                return new AppointmentParticipation(Guid.Parse("16f63cc0-36c2-4a3f-93d1-2c2c3aa15ab4"), new Create.Command
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

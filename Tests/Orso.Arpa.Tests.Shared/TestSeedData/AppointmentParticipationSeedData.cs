using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.AppointmentParticipations;
using Orso.Arpa.Domain.SelectValueMappings.Seed;

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
                    OrsianerParticipation,
                    OrsonautParticipation
                };
            }
        }

        public static AppointmentParticipation OrsianerParticipation
        {
            get
            {
                return new AppointmentParticipation(Guid.Parse("9e230c76-759b-466f-8cff-6e77e53aa754"), new Create.Command
                {
                    AppointmentId = AppointmentSeedData.RockingXMasRehearsal.Id,
                    PersonId = PersonSeedData.Orsianer.Id,
                    PredictionId = SelectValueMappingSeedData.AppointmentParticipationPredictionMappings[0].Id,
                    ResultId = null
                });
            }
        }

        public static AppointmentParticipation OrsonautParticipation
        {
            get
            {
                return new AppointmentParticipation(Guid.Parse("16f63cc0-36c2-4a3f-93d1-2c2c3aa15ab4"), new Create.Command
                {
                    AppointmentId = AppointmentSeedData.RockingXMasRehearsal.Id,
                    PersonId = PersonSeedData.Orsonaut.Id,
                    PredictionId = SelectValueMappingSeedData.AppointmentParticipationPredictionMappings[1].Id,
                    ResultId = SelectValueMappingSeedData.AppointmentParticipationResultMappings[0].Id
                });
            }
        }
    }
}

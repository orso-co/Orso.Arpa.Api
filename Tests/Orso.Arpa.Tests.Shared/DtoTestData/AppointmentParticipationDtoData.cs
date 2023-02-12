using Orso.Arpa.Application.AppointmentParticipationApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class AppointmentParticipationDtoData
    {
        public static AppointmentParticipationDto PerformerParticipationRockingXMasRehearsal
        {
            get
            {
                AppointmentParticipation appointment = AppointmentParticipationSeedData.PerformerParticipationRockingXMasRehearsal;
                return CreateDto(appointment);
            }
        }

        public static AppointmentParticipationDto PerformerParticipationAltoRehearsal
        {
            get
            {
                AppointmentParticipation appointment = AppointmentParticipationSeedData.PerformerParticipationAltoRehearsal;
                return CreateDto(appointment);
            }
        }

        public static AppointmentParticipationDto StaffParticipation
        {
            get
            {
                AppointmentParticipation appointment = AppointmentParticipationSeedData.StaffParticipation;
                return CreateDto(appointment);
            }
        }

        private static AppointmentParticipationDto CreateDto(AppointmentParticipation appointment)
        {
            return new AppointmentParticipationDto
            {
                CreatedBy = "anonymous",
                CreatedAt = FakeDateTime.UtcNow,
                Id = appointment.Id,
                ModifiedAt = null,
                ModifiedBy = appointment.ModifiedBy,
                Prediction = appointment.Prediction,
                Result = appointment.Result,
                CommentByPerformerInner = appointment.CommentByPerformerInner,
            };
        }
    }
}

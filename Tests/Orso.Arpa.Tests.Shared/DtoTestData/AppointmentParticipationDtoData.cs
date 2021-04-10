using Orso.Arpa.Application.AppointmentParticipationApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class AppointmentParticipationDtoData
    {
        public static AppointmentParticipationDto PerformerParticipation
        {
            get
            {
                AppointmentParticipation appointment = AppointmentParticipationSeedData.PerformerParticipation;
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
                PredictionId = appointment.PredictionId,
                ResultId = appointment.ResultId
            };
        }
    }
}

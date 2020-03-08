using Orso.Arpa.Application.AppointmentParticipationApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class AppointmentParticipationDtoData
    {
        public static AppointmentParticipationDto OrsianerParticipation
        {
            get
            {
                AppointmentParticipation appointment = AppointmentParticipationSeedData.OrsianerParticipation;
                return CreateDto(appointment);
            }
        }

        public static AppointmentParticipationDto OrsonautParticipation
        {
            get
            {
                AppointmentParticipation appointment = AppointmentParticipationSeedData.OrsonautParticipation;
                return CreateDto(appointment);
            }
        }

        private static AppointmentParticipationDto CreateDto(AppointmentParticipation appointment)
        {
            return new AppointmentParticipationDto
            {
                CreatedBy = "anonymous",
                Id = appointment.Id,
                ModifiedAt = null,
                ModifiedBy = appointment.ModifiedBy,
                PredictionId = appointment.PredictionId,
                ResultId = appointment.ResultId
            };
        }
    }
}

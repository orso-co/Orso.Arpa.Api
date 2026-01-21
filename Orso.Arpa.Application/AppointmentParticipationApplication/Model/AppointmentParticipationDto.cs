using AutoMapper;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.AppointmentDomain.Enums;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.General.Model;

namespace Orso.Arpa.Application.AppointmentParticipationApplication.Model
{
    public class AppointmentParticipationDto : BaseEntityDto
    {
        public AppointmentParticipationResult? Result { get; set; }
        public AppointmentParticipationPrediction? Prediction { get; set; }

        public string CommentByPerformerInner { get; set; }
        public string CommentByStaffInner { get; set; }
    }

    public class AppointmentParticipationDtoMappingProfile : Profile
    {
        public AppointmentParticipationDtoMappingProfile()
        {
            _ = CreateMap<AppointmentParticipation, AppointmentParticipationDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}

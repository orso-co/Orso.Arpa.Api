using AutoMapper;
using Orso.Arpa.Application.General;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Enums;

namespace Orso.Arpa.Application.AppointmentParticipationApplication
{
    public class AppointmentParticipationDto : BaseEntityDto
    {
        public AppointmentParticipationResult? Result { get; set; }
        public AppointmentParticipationPrediction? Prediction { get; set; }

        public string CommentByPerformerInner { get; set; }
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

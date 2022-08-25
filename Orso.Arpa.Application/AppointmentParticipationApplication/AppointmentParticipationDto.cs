using System;
using AutoMapper;
using Orso.Arpa.Application.General;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.AppointmentParticipationApplication
{
    public class AppointmentParticipationDto : BaseEntityDto
    {
        public Guid? ResultId { get; set; }
        public Guid? PredictionId { get; set; }

        public string CommentByPerformerInner { get; set; }
    }

    public class AppointmentParticipationDtoMappingProfile : Profile
    {
        public AppointmentParticipationDtoMappingProfile()
        {
            CreateMap<AppointmentParticipation, AppointmentParticipationDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}

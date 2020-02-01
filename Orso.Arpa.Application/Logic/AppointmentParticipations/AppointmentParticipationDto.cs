using System;
using AutoMapper;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.Logic.AppointmentParticipations
{
    public class AppointmentParticipationDto : BaseEntityDto
    {
        public Guid? ResultId { get; set; }
        public Guid? PredictionId { get; set; }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AppointmentParticipation, AppointmentParticipationDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}

using System;
using AutoMapper;
using Orso.Arpa.Application.General;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.MusicianProfileDeactivationApplication
{
    public class MusicianProfileDeactivationDto : BaseEntityDto
    {
        public DateTime DeactivationStart { get; set; }
        public string Purpose { get; set; }
    }

    public class MusicianProfileDeactivationDtoMappingProfile : Profile
    {
        public MusicianProfileDeactivationDtoMappingProfile()
        {
            CreateMap<MusicianProfileDeactivation, MusicianProfileDeactivationDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}

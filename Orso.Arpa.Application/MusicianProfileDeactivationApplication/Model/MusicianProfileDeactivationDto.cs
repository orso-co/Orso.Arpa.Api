using System;
using AutoMapper;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;

namespace Orso.Arpa.Application.MusicianProfileDeactivationApplication.Model
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

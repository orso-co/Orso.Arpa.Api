using AutoMapper;
using Orso.Arpa.Application.General;
using Orso.Arpa.Application.RegionApplication;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.MusicianProfileApplication
{
    public class RegionPreferenceDto : BaseEntityDto
    {
        public RegionDto Region { get; set; }
        public byte Rating { get; set; }

        public string Comment { get; set; }
    }

    public class RegionPreferenceDtoMappingProfile : Profile
    {
        public RegionPreferenceDtoMappingProfile()
        {
            CreateMap<RegionPreference, RegionPreferenceDto>();
        }
    }
}

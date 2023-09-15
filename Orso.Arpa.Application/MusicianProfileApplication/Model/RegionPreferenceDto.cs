using AutoMapper;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Application.RegionApplication.Model;
using Orso.Arpa.Domain.RegionDomain.Model;

namespace Orso.Arpa.Application.MusicianProfileApplication.Model
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

using AutoMapper;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.MappingProfiles
{
    public class VenueDtoMappingProfile : Profile
    {
        public VenueDtoMappingProfile()
        {
            CreateMap<Venue, VenueDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}

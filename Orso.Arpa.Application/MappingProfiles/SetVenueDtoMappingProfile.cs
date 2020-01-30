using AutoMapper;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Logic.Appointments;

namespace Orso.Arpa.Application.MappingProfiles
{
    public class SetVenueDtoMappingProfile : Profile
    {
        public SetVenueDtoMappingProfile()
        {
            CreateMap<SetVenueDto, SetVenue.Command>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.VenueId, opt => opt.MapFrom(src => src.VenueId));
        }
    }
}

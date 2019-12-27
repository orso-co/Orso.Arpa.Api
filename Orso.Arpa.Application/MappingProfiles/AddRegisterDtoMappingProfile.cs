using AutoMapper;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Appointments;

namespace Orso.Arpa.Application.MappingProfiles
{
    public class AddRegisterDtoMappingProfile : Profile
    {
        public AddRegisterDtoMappingProfile()
        {
            CreateMap<AddRegisterDto, AddRegister.Command>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.RegisterId, opt => opt.MapFrom(src => src.RegisterId));
        }
    }
}

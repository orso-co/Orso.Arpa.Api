using AutoMapper;
using Orso.Arpa.Application.Dtos;

namespace Orso.Arpa.Application.MappingProfiles
{
    public class TokenDtoMappingProfile : Profile
    {
        public TokenDtoMappingProfile()
        {
            CreateMap<string, TokenDto>()
                .ForMember(dest => dest.Token, opt => opt.MapFrom(src => src));
        }
    }
}

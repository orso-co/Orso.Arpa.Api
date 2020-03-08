using AutoMapper;

namespace Orso.Arpa.Application.AuthApplication
{
    public class TokenDto
    {
        public string Token { get; set; }
    }

    public class TokenDtoMappingProfile : Profile
    {
        public TokenDtoMappingProfile()
        {
            CreateMap<string, TokenDto>()
                .ForMember(dest => dest.Token, opt => opt.MapFrom(src => src));
        }
    }
}

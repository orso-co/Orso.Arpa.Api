using AutoMapper;

namespace Orso.Arpa.Application.Logic.Auth
{
    public class TokenDto
    {
        public string Token { get; set; }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<string, TokenDto>()
                .ForMember(dest => dest.Token, opt => opt.MapFrom(src => src));
        }
    }
}

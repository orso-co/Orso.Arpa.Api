using AutoMapper;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Logic.Auth;

namespace Orso.Arpa.Application.MappingProfiles
{
    public class LoginDtoMappingProfile : Profile
    {
        public LoginDtoMappingProfile()
        {
            CreateMap<LoginDto, Login.Query>();
        }
    }
}

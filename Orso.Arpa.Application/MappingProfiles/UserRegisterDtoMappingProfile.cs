using AutoMapper;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Auth;

namespace Orso.Arpa.Application.MappingProfiles
{
    public class UserRegisterDtoMappingProfile : Profile
    {
        public UserRegisterDtoMappingProfile()
        {
            CreateMap<UserRegisterDto, UserRegister.Command>();
        }
    }
}

using AutoMapper;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Auth;

namespace Orso.Arpa.Application.MappingProfiles
{
    public class RegisterDtoMappingProfile : Profile
    {
        public RegisterDtoMappingProfile()
        {
            CreateMap<RegisterDto, Register.Command>();
        }
    }
}

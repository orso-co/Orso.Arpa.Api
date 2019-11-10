using AutoMapper;
using Orso.Arpa.Domain.Auth;

namespace Orso.Arpa.Application.MappingProfiles
{
    public partial class LoginDto
    {
        public class LoginDtoMappingProfile : Profile
        {
            public LoginDtoMappingProfile()
            {
                CreateMap<LoginDto, Login.Query>();
            }
        }
    }
}

using AutoMapper;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.MappingProfiles
{
    public partial class UserDto
    {
        public class UserDtoMappingProfile : Profile
        {
            public UserDtoMappingProfile()
            {
                CreateMap<User, UserDto>();
            }
        }
    }
}

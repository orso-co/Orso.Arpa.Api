using AutoMapper;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.MappingProfiles
{

    public class UserProfileDtoMappingProfile : Profile
    {
        public UserProfileDtoMappingProfile()
        {
            CreateMap<User, UserProfileDto>();
        }
    }
}

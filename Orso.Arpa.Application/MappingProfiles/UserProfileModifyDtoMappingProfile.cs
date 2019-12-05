using AutoMapper;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Me;

namespace Orso.Arpa.Application.MappingProfiles
{
    public class UserProfileModifyDtoMappingProfile : Profile
    {
        public UserProfileModifyDtoMappingProfile()
        {
            CreateMap<UserProfileModifyDto, Modify.Command>();
        }
    }
}

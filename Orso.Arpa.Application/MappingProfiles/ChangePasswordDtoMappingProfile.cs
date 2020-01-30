using AutoMapper;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Logic.Auth;

namespace Orso.Arpa.Application.MappingProfiles
{
    public class ChangePasswordDtoMappingProfile : Profile
    {
        public ChangePasswordDtoMappingProfile()
        {
            CreateMap<ChangePasswordDto, ChangePassword.Command>();
        }
    }
}

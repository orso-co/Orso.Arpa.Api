using AutoMapper;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.MappingProfiles
{
    public class RegisterDtoMappingProfile : Profile
    {
        public RegisterDtoMappingProfile()
        {
            CreateMap<Register, RegisterDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>();

            CreateMap<RegisterDto, Register>();
        }
    }
}

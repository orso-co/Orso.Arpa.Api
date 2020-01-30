using AutoMapper;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Logic.Appointments;

namespace Orso.Arpa.Application.MappingProfiles
{
    public class RemoveProjectDtoMappingProfile : Profile
    {
        public RemoveProjectDtoMappingProfile()
        {
            CreateMap<RemoveProjectDto, RemoveProject.Command>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId));
        }
    }
}

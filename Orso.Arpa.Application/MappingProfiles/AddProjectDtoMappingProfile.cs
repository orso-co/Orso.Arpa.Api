using AutoMapper;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Appointments;

namespace Orso.Arpa.Application.MappingProfiles
{
    public class AddProjectDtoMappingProfile : Profile
    {
        public AddProjectDtoMappingProfile()
        {
            CreateMap<AddProjectDto, AddProject.Command>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId));
        }
    }
}

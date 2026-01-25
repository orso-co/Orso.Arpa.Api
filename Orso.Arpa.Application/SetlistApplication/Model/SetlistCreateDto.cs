using AutoMapper;
using Orso.Arpa.Domain.MusicLibraryDomain.Commands;

namespace Orso.Arpa.Application.SetlistApplication.Model
{
    public class SetlistCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsTemplate { get; set; }
    }

    public class SetlistCreateDtoMappingProfile : Profile
    {
        public SetlistCreateDtoMappingProfile()
        {
            CreateMap<SetlistCreateDto, CreateSetlist.Command>();
        }
    }
}

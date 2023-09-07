using System.Collections.Generic;
using AutoMapper;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;

namespace Orso.Arpa.Application.SectionApplication
{
    public class SectionTreeDto
    {
        public SectionDto Data { get; set; }
        public ICollection<SectionTreeDto> Children { get; set; }
        public bool IsRoot { get; set; }
        public bool IsLeaf { get; set; }
        public int Level { get; set; }
    }

    public class SectionTreeDtoMappingProfile : Profile
    {
        public SectionTreeDtoMappingProfile()
        {
            CreateMap<ITree<Section>, SectionTreeDto>();
        }
    }
}

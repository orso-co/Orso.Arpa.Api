using System.Collections.Generic;
using AutoMapper;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.SectionDomain.Model;

namespace Orso.Arpa.Application.SectionApplication.Model
{
    public class SectionTreeDto
    {
        public SectionDto Data { get; set; }
        public ICollection<SectionTreeDto> Children { get; set; }
        public bool IsRoot { get; set; }
        public bool IsLeaf { get; set; }
        public int Level { get; set; }
        public SectionTreeDto Parent { get; set; }
    }

    public class SectionTreeDtoMappingProfile : Profile
    {
        public SectionTreeDtoMappingProfile()
        {
            CreateMap<ITree<Section>, SectionTreeDto>();
        }
    }
}

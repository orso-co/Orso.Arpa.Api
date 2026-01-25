using System;
using System.Collections.Generic;
using AutoMapper;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.MusicLibraryDomain.Model;

namespace Orso.Arpa.Application.SetlistApplication.Model
{
    public class SetlistDto : BaseEntityDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsTemplate { get; set; }
        public IList<SetlistPieceDto> Pieces { get; set; } = [];
    }

    public class SetlistDtoMappingProfile : Profile
    {
        public SetlistDtoMappingProfile()
        {
            CreateMap<Setlist, SetlistDto>()
                .IncludeBase<Domain.General.Model.BaseEntity, BaseEntityDto>();
        }
    }
}

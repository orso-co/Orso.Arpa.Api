using System;
using System.Collections.Generic;
using AutoMapper;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Application.SectionApplication.Model;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.MusicLibraryDomain.Model;

namespace Orso.Arpa.Application.MusicPieceApplication.Model
{
    public class MusicPiecePartDto : BaseEntityDto
    {
        public Guid MusicPieceId { get; set; }
        public SectionDto Section { get; set; }
        public string PartName { get; set; }
        public int SortOrder { get; set; }
        public IList<MusicPieceFileDto> Files { get; set; } = [];
    }

    public class MusicPiecePartDtoMappingProfile : Profile
    {
        public MusicPiecePartDtoMappingProfile()
        {
            _ = CreateMap<MusicPiecePart, MusicPiecePartDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}

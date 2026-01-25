using System;
using System.Collections.Generic;
using AutoMapper;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.MusicLibraryDomain.Model;

namespace Orso.Arpa.Application.MusicPieceApplication.Model
{
    public class MusicPieceFileDto : BaseEntityDto
    {
        public Guid MusicPieceId { get; set; }
        public Guid? MusicPiecePartId { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public long FileSize { get; set; }
        public string Description { get; set; }
        public IList<MusicPieceFileSectionDto> Sections { get; set; } = new List<MusicPieceFileSectionDto>();
    }

    public class MusicPieceFileDtoMappingProfile : Profile
    {
        public MusicPieceFileDtoMappingProfile()
        {
            _ = CreateMap<MusicPieceFile, MusicPieceFileDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>()
                .ForMember(dest => dest.Sections, opt => opt.MapFrom(src => src.Sections));
        }
    }
}

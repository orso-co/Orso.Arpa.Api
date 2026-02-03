using System;
using AutoMapper;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Application.SelectValueApplication.Model;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.MusicLibraryDomain.Model;

namespace Orso.Arpa.Application.MusicPieceApplication.Model
{
    public class MusicPieceUrlDto : BaseEntityDto
    {
        public Guid MusicPieceId { get; set; }
        public string Href { get; set; }
        public string AnchorText { get; set; }
        public SelectValueDto UrlType { get; set; }
    }

    public class MusicPieceUrlDtoMappingProfile : Profile
    {
        public MusicPieceUrlDtoMappingProfile()
        {
            _ = CreateMap<MusicPieceUrl, MusicPieceUrlDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}

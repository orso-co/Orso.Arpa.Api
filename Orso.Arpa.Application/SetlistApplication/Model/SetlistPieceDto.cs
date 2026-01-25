using System;
using AutoMapper;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Application.MusicPieceApplication.Model;
using Orso.Arpa.Domain.MusicLibraryDomain.Model;

namespace Orso.Arpa.Application.SetlistApplication.Model
{
    public class SetlistPieceDto : BaseEntityDto
    {
        public Guid SetlistId { get; set; }
        public Guid MusicPieceId { get; set; }
        public MusicPieceDto MusicPiece { get; set; }
        public int SortOrder { get; set; }
        public string Notes { get; set; }
    }

    public class SetlistPieceDtoMappingProfile : Profile
    {
        public SetlistPieceDtoMappingProfile()
        {
            CreateMap<SetlistPiece, SetlistPieceDto>()
                .IncludeBase<Domain.General.Model.BaseEntity, BaseEntityDto>();
        }
    }
}

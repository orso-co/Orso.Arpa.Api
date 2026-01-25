using System.Collections.Generic;
using AutoMapper;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Application.SelectValueApplication.Model;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.MusicLibraryDomain.Model;

namespace Orso.Arpa.Application.MusicPieceApplication.Model
{
    public class MusicPieceDto : BaseEntityDto
    {
        public string Title { get; set; }
        public string Composer { get; set; }
        public string Arranger { get; set; }
        public string Subtitle { get; set; }
        public int? Duration { get; set; }
        public int? YearComposed { get; set; }
        public string Opus { get; set; }
        public string Instrumentation { get; set; }
        public SelectValueDto Epoch { get; set; }
        public SelectValueDto Genre { get; set; }
        public SelectValueDto DifficultyLevel { get; set; }
        public string PerformanceNotes { get; set; }
        public string InternalNotes { get; set; }
        public bool IsArchived { get; set; }
        public IList<MusicPiecePartDto> Parts { get; set; } = [];
        public IList<MusicPieceFileDto> Files { get; set; } = [];
    }

    public class MusicPieceDtoMappingProfile : Profile
    {
        public MusicPieceDtoMappingProfile()
        {
            _ = CreateMap<MusicPiece, MusicPieceDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}

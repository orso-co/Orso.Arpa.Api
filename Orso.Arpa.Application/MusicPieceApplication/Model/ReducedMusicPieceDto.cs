using System;
using AutoMapper;
using Orso.Arpa.Domain.MusicLibraryDomain.Model;

namespace Orso.Arpa.Application.MusicPieceApplication.Model
{
    public class ReducedMusicPieceDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Composer { get; set; }
    }

    public class ReducedMusicPieceDtoMappingProfile : Profile
    {
        public ReducedMusicPieceDtoMappingProfile()
        {
            _ = CreateMap<MusicPiece, ReducedMusicPieceDto>();
        }
    }
}

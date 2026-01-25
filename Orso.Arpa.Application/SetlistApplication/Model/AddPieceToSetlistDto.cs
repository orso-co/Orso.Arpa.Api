using System;
using AutoMapper;
using Orso.Arpa.Domain.MusicLibraryDomain.Commands;

namespace Orso.Arpa.Application.SetlistApplication.Model
{
    public class AddPieceToSetlistDto
    {
        public Guid MusicPieceId { get; set; }
        public string Notes { get; set; }
    }

    public class AddPieceToSetlistDtoMappingProfile : Profile
    {
        public AddPieceToSetlistDtoMappingProfile()
        {
            CreateMap<AddPieceToSetlistDto, AddPieceToSetlist.Command>();
        }
    }
}

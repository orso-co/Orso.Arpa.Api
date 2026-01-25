using System;
using AutoMapper;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.MusicLibraryDomain.Model;

namespace Orso.Arpa.Application.SetlistApplication.Model
{
    /// <summary>
    /// Reduced setlist DTO without pieces, for use in project listings
    /// </summary>
    public class ReducedSetlistDto : BaseEntityDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsTemplate { get; set; }
        public int PieceCount { get; set; }
    }

    public class ReducedSetlistDtoMappingProfile : Profile
    {
        public ReducedSetlistDtoMappingProfile()
        {
            CreateMap<Setlist, ReducedSetlistDto>()
                .IncludeBase<Domain.General.Model.BaseEntity, BaseEntityDto>()
                .ForMember(dest => dest.PieceCount, opt => opt.MapFrom(src => src.Pieces.Count));
        }
    }
}

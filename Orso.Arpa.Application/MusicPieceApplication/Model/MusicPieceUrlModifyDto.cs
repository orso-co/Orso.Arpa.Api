using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General.Extensions;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.MusicLibraryDomain.Commands;

namespace Orso.Arpa.Application.MusicPieceApplication.Model
{
    public class MusicPieceUrlModifyDto : IdFromRouteDto<MusicPieceUrlModifyBodyDto>
    {
    }

    public class MusicPieceUrlModifyBodyDto
    {
        public string Href { get; set; }
        public string AnchorText { get; set; }
        public Guid? UrlTypeId { get; set; }
    }

    public class MusicPieceUrlModifyDtoMappingProfile : Profile
    {
        public MusicPieceUrlModifyDtoMappingProfile()
        {
            _ = CreateMap<MusicPieceUrlModifyDto, ModifyMusicPieceUrl.Command>()
                .ForMember(dest => dest.Href, opt => opt.MapFrom(src => src.Body.Href))
                .ForMember(dest => dest.AnchorText, opt => opt.MapFrom(src => src.Body.AnchorText))
                .ForMember(dest => dest.UrlTypeId, opt => opt.MapFrom(src => src.Body.UrlTypeId));
        }
    }

    public class MusicPieceUrlModifyBodyDtoValidator : AbstractValidator<MusicPieceUrlModifyBodyDto>
    {
        public MusicPieceUrlModifyBodyDtoValidator()
        {
            _ = RuleFor(p => p.Href)
                .ValidUri(1000);

            _ = RuleFor(p => p.AnchorText)
                .FreeText(200);
        }
    }
}

using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General.Extensions;
using Orso.Arpa.Domain.MusicLibraryDomain.Commands;

namespace Orso.Arpa.Application.MusicPieceApplication.Model
{
    public class MusicPieceUrlCreateDto
    {
        public string Href { get; set; }
        public string AnchorText { get; set; }
        public Guid? UrlTypeId { get; set; }
    }

    public class MusicPieceUrlCreateDtoMappingProfile : Profile
    {
        public MusicPieceUrlCreateDtoMappingProfile()
        {
            _ = CreateMap<MusicPieceUrlCreateDto, CreateMusicPieceUrl.Command>()
                .ForMember(dest => dest.MusicPieceId, opt => opt.Ignore());
        }
    }

    public class MusicPieceUrlCreateDtoValidator : AbstractValidator<MusicPieceUrlCreateDto>
    {
        public MusicPieceUrlCreateDtoValidator()
        {
            _ = RuleFor(p => p.Href)
                .ValidUri(1000);

            _ = RuleFor(p => p.AnchorText)
                .FreeText(200);
        }
    }
}

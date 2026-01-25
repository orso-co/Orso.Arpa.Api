using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General.Extensions;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.MusicLibraryDomain.Commands;

namespace Orso.Arpa.Application.MusicPieceApplication.Model
{
    public class MusicPieceModifyBodyDto
    {
        public string Title { get; set; }
        public string Composer { get; set; }
        public string Arranger { get; set; }
        public string Subtitle { get; set; }
        public int? Duration { get; set; }
        public int? YearComposed { get; set; }
        public string Opus { get; set; }
        public string Instrumentation { get; set; }
        public Guid? EpochId { get; set; }
        public Guid? GenreId { get; set; }
        public Guid? DifficultyLevelId { get; set; }
        public string PerformanceNotes { get; set; }
        public string InternalNotes { get; set; }
    }

    public class MusicPieceModifyDto : IdFromRouteDto<MusicPieceModifyBodyDto>
    {
    }

    public class MusicPieceModifyDtoMappingProfile : Profile
    {
        public MusicPieceModifyDtoMappingProfile()
        {
            _ = CreateMap<MusicPieceModifyDto, ModifyMusicPiece.Command>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Body.Title))
                .ForMember(dest => dest.Composer, opt => opt.MapFrom(src => src.Body.Composer))
                .ForMember(dest => dest.Arranger, opt => opt.MapFrom(src => src.Body.Arranger))
                .ForMember(dest => dest.Subtitle, opt => opt.MapFrom(src => src.Body.Subtitle))
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Body.Duration))
                .ForMember(dest => dest.YearComposed, opt => opt.MapFrom(src => src.Body.YearComposed))
                .ForMember(dest => dest.Opus, opt => opt.MapFrom(src => src.Body.Opus))
                .ForMember(dest => dest.Instrumentation, opt => opt.MapFrom(src => src.Body.Instrumentation))
                .ForMember(dest => dest.EpochId, opt => opt.MapFrom(src => src.Body.EpochId))
                .ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.Body.GenreId))
                .ForMember(dest => dest.DifficultyLevelId, opt => opt.MapFrom(src => src.Body.DifficultyLevelId))
                .ForMember(dest => dest.PerformanceNotes, opt => opt.MapFrom(src => src.Body.PerformanceNotes))
                .ForMember(dest => dest.InternalNotes, opt => opt.MapFrom(src => src.Body.InternalNotes));
        }
    }

    public class MusicPieceModifyBodyDtoValidator : AbstractValidator<MusicPieceModifyBodyDto>
    {
        public MusicPieceModifyBodyDtoValidator()
        {
            _ = RuleFor(p => p.Title)
                .NotEmpty()
                .FreeText(500);

            _ = RuleFor(p => p.Composer)
                .PersonName(200);

            _ = RuleFor(p => p.Arranger)
                .PersonName(200);

            _ = RuleFor(p => p.Subtitle)
                .FreeText(500);

            _ = RuleFor(p => p.Opus)
                .PlaceName(100);

            _ = RuleFor(p => p.Instrumentation)
                .RestrictedFreeText(2000);

            _ = RuleFor(p => p.PerformanceNotes)
                .RestrictedFreeText(5000);

            _ = RuleFor(p => p.InternalNotes)
                .RestrictedFreeText(5000);

            _ = RuleFor(p => p.Duration)
                .GreaterThan(0)
                .When(p => p.Duration.HasValue);

            _ = RuleFor(p => p.YearComposed)
                .InclusiveBetween(1, 2100)
                .When(p => p.YearComposed.HasValue);
        }
    }
}

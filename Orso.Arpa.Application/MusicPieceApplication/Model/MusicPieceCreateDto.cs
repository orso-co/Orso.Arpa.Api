using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General.Extensions;
using Orso.Arpa.Domain.MusicLibraryDomain.Commands;

namespace Orso.Arpa.Application.MusicPieceApplication.Model
{
    public class MusicPieceCreateDto
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

    public class MusicPieceCreateDtoMappingProfile : Profile
    {
        public MusicPieceCreateDtoMappingProfile()
        {
            _ = CreateMap<MusicPieceCreateDto, CreateMusicPiece.Command>();
        }
    }

    public class MusicPieceCreateDtoValidator : AbstractValidator<MusicPieceCreateDto>
    {
        public MusicPieceCreateDtoValidator()
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

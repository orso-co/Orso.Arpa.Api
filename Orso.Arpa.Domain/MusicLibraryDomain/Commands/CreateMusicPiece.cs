using System;
using FluentValidation;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.MusicLibraryDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Create;

namespace Orso.Arpa.Domain.MusicLibraryDomain.Commands
{
    public static class CreateMusicPiece
    {
        public class Command : ICreateCommand<MusicPiece>
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

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.Title)
                    .NotEmpty()
                    .MaximumLength(500);

                RuleFor(c => c.Composer)
                    .MaximumLength(200);

                RuleFor(c => c.Arranger)
                    .MaximumLength(200);

                RuleFor(c => c.Subtitle)
                    .MaximumLength(500);

                RuleFor(c => c.Opus)
                    .MaximumLength(100);

                RuleFor(c => c.Duration)
                    .GreaterThan(0)
                    .When(c => c.Duration.HasValue);

                RuleFor(c => c.YearComposed)
                    .InclusiveBetween(1, 2100)
                    .When(c => c.YearComposed.HasValue);

                RuleFor(c => c.EpochId)
                    .SelectValueMapping<Command, MusicPiece>(arpaContext, m => m.Epoch);

                RuleFor(c => c.GenreId)
                    .SelectValueMapping<Command, MusicPiece>(arpaContext, m => m.Genre);

                RuleFor(c => c.DifficultyLevelId)
                    .SelectValueMapping<Command, MusicPiece>(arpaContext, m => m.DifficultyLevel);
            }
        }
    }
}

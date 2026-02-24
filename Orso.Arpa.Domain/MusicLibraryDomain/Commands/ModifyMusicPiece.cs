using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.MusicLibraryDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Modify;

namespace Orso.Arpa.Domain.MusicLibraryDomain.Commands
{
    public static class ModifyMusicPiece
    {
        public class Command : IModifyCommand<MusicPiece>
        {
            public Guid Id { get; set; }
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
            public Guid? ParentId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.Id)
                    .EntityExists<Command, MusicPiece>(arpaContext);

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

                RuleFor(c => c.ParentId)
                    .EntityExists<Command, MusicPiece>(arpaContext)
                    .MustAsync(async (command, parentId, context, cancellation) =>
                    {
                        // Max 3 hierarchy levels total
                        // Calculate: ancestor depth of parent + 1 (current) + descendant depth <= 3

                        // 1. Ancestor depth of selected parent
                        int ancestorDepth = 0;
                        var current = await arpaContext.Set<MusicPiece>()
                            .FirstOrDefaultAsync(p => p.Id == parentId.Value, cancellation);
                        while (current?.ParentId != null && ancestorDepth < 3)
                        {
                            current = await arpaContext.Set<MusicPiece>()
                                .FirstOrDefaultAsync(p => p.Id == current.ParentId.Value, cancellation);
                            ancestorDepth++;
                        }
                        int myLevel = ancestorDepth + 2; // parent level + 1

                        // 2. Descendant depth of current piece
                        int descendantDepth = await GetDescendantDepthAsync(arpaContext, command.Id, cancellation);

                        return myLevel + descendantDepth <= 3;
                    })
                    .WithMessage("Maximum hierarchy depth of 3 levels exceeded.")
                    .When(c => c.ParentId.HasValue);
            }

            private static async Task<int> GetDescendantDepthAsync(IArpaContext context, Guid pieceId, CancellationToken ct)
            {
                var children = await context.Set<MusicPiece>()
                    .Where(p => p.ParentId == pieceId)
                    .Select(p => p.Id)
                    .ToListAsync(ct);

                if (!children.Any()) return 0;

                int maxChildDepth = 0;
                foreach (var childId in children)
                {
                    int childDepth = await GetDescendantDepthAsync(context, childId, ct);
                    if (childDepth > maxChildDepth) maxChildDepth = childDepth;
                }
                return 1 + maxChildDepth;
            }
        }
    }
}

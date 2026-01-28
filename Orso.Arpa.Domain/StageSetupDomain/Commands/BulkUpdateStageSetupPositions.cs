using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;
using Orso.Arpa.Domain.StageSetupDomain.Model;

namespace Orso.Arpa.Domain.StageSetupDomain.Commands
{
    public static class BulkUpdateStageSetupPositions
    {
        public class PositionData
        {
            public Guid MusicianProfileId { get; set; }
            public double PositionX { get; set; }
            public double PositionY { get; set; }
            public int? Row { get; set; }
            public int? Stand { get; set; }
        }

        public class Command : IRequest<IEnumerable<StageSetupPosition>>
        {
            public Guid StageSetupId { get; set; }
            public List<PositionData> Positions { get; set; } = new();
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.StageSetupId)
                    .EntityExists<Command, StageSetup>(arpaContext);

                RuleFor(c => c.Positions)
                    .NotEmpty()
                    .WithMessage("At least one position is required");

                RuleForEach(c => c.Positions).ChildRules(position =>
                {
                    position.RuleFor(p => p.MusicianProfileId)
                        .NotEmpty();

                    position.RuleFor(p => p.PositionX)
                        .InclusiveBetween(0, 100);

                    position.RuleFor(p => p.PositionY)
                        .InclusiveBetween(0, 100);

                    position.RuleFor(p => p.Row)
                        .GreaterThan(0)
                        .When(p => p.Row.HasValue);

                    position.RuleFor(p => p.Stand)
                        .GreaterThan(0)
                        .When(p => p.Stand.HasValue);
                });
            }
        }

        public class Handler : IRequestHandler<Command, IEnumerable<StageSetupPosition>>
        {
            private readonly IArpaContext _arpaContext;

            public Handler(IArpaContext arpaContext)
            {
                _arpaContext = arpaContext;
            }

            public async Task<IEnumerable<StageSetupPosition>> Handle(Command request, CancellationToken cancellationToken)
            {
                // Get all existing positions for this setup
                var existingPositions = await _arpaContext.Set<StageSetupPosition>()
                    .Where(p => p.StageSetupId == request.StageSetupId)
                    .ToListAsync(cancellationToken);

                var existingDict = existingPositions.ToDictionary(p => p.MusicianProfileId);
                var requestedProfileIds = request.Positions.Select(p => p.MusicianProfileId).ToHashSet();

                var result = new List<StageSetupPosition>();

                foreach (var positionData in request.Positions)
                {
                    if (existingDict.TryGetValue(positionData.MusicianProfileId, out var existingPosition))
                    {
                        // Update existing position
                        existingPosition.UpdatePosition(positionData.PositionX, positionData.PositionY);
                        result.Add(existingPosition);
                    }
                    else
                    {
                        // Create new position
                        var createCommand = new CreateStageSetupPosition.Command
                        {
                            StageSetupId = request.StageSetupId,
                            MusicianProfileId = positionData.MusicianProfileId,
                            PositionX = positionData.PositionX,
                            PositionY = positionData.PositionY,
                            Row = positionData.Row,
                            Stand = positionData.Stand
                        };
                        var newPosition = new StageSetupPosition(Guid.NewGuid(), createCommand);
                        _arpaContext.Add(newPosition);
                        result.Add(newPosition);
                    }
                }

                // Remove positions that are no longer in the request (moved back to pool)
                var positionsToRemove = existingPositions
                    .Where(p => !requestedProfileIds.Contains(p.MusicianProfileId))
                    .ToList();

                foreach (var position in positionsToRemove)
                {
                    _arpaContext.Remove(position);
                }

                await _arpaContext.SaveChangesAsync(cancellationToken);

                return result;
            }
        }
    }
}

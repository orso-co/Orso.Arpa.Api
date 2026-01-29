using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Orso.Arpa.Application.StageSetupApplication.Interfaces;
using Orso.Arpa.Application.StageSetupApplication.Model;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.StageSetupDomain.Commands;
using Orso.Arpa.Domain.StageSetupDomain.Model;

namespace Orso.Arpa.Application.StageSetupApplication.Services
{
    public class StageSetupPositionService : IStageSetupPositionService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IArpaContext _arpaContext;
        private readonly ILogger<StageSetupPositionService> _logger;

        public StageSetupPositionService(
            IMediator mediator,
            IMapper mapper,
            IArpaContext arpaContext,
            ILogger<StageSetupPositionService> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _arpaContext = arpaContext;
            _logger = logger;
        }

        public async Task<IEnumerable<StageSetupPositionDto>> GetBySetupAsync(Guid stageSetupId)
        {
            var positions = await _arpaContext.Set<StageSetupPosition>()
                .Where(p => p.StageSetupId == stageSetupId)
                .Include(p => p.MusicianProfile)
                    .ThenInclude(mp => mp.Person)
                .Include(p => p.MusicianProfile)
                    .ThenInclude(mp => mp.Instrument)
                .Include(p => p.MusicianProfile)
                    .ThenInclude(mp => mp.Qualification)
                        .ThenInclude(q => q.SelectValue)
                .OrderBy(p => p.Row)
                .ThenBy(p => p.Stand)
                .ThenBy(p => p.PositionX)
                .ToListAsync();

            return _mapper.Map<IEnumerable<StageSetupPositionDto>>(positions);
        }

        public async Task<StageSetupPositionDto> CreateAsync(Guid stageSetupId, StageSetupPositionCreateDto createDto)
        {
            try
            {
                _logger.LogInformation("CreateAsync: Starting for setupId={SetupId}, musicianProfileId={MusicianProfileId}",
                    stageSetupId, createDto.MusicianProfileId);

                var command = _mapper.Map<CreateStageSetupPosition.Command>(createDto);
                command.StageSetupId = stageSetupId;

                _logger.LogInformation("CreateAsync: Sending command to mediator");
                var result = await _mediator.Send(command);
                _logger.LogInformation("CreateAsync: Mediator returned position with Id={PositionId}", result?.Id);

                // Reload with includes to get MusicianProfile data
                _logger.LogInformation("CreateAsync: Reloading position with includes");
                var positionWithIncludes = await _arpaContext.Set<StageSetupPosition>()
                    .Where(p => p.Id == result.Id)
                    .Include(p => p.MusicianProfile)
                        .ThenInclude(mp => mp.Person)
                    .Include(p => p.MusicianProfile)
                        .ThenInclude(mp => mp.Instrument)
                    .Include(p => p.MusicianProfile)
                        .ThenInclude(mp => mp.Qualification)
                            .ThenInclude(q => q.SelectValue)
                    .FirstAsync();

                _logger.LogInformation("CreateAsync: Mapping to DTO. MusicianProfile={HasProfile}, Person={HasPerson}",
                    positionWithIncludes.MusicianProfile != null,
                    positionWithIncludes.MusicianProfile?.Person != null);

                return _mapper.Map<StageSetupPositionDto>(positionWithIncludes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CreateAsync: Error creating position for setupId={SetupId}, musicianProfileId={MusicianProfileId}",
                    stageSetupId, createDto.MusicianProfileId);
                throw;
            }
        }

        public async Task ModifyAsync(StageSetupPositionModifyBodyDto modifyDto)
        {
            var command = _mapper.Map<ModifyStageSetupPosition.Command>(modifyDto);
            await _mediator.Send(command);
        }

        public async Task DeleteAsync(Guid stageSetupId, Guid musicianProfileId)
        {
            var position = await _arpaContext.Set<StageSetupPosition>()
                .FirstOrDefaultAsync(p => p.StageSetupId == stageSetupId && p.MusicianProfileId == musicianProfileId);

            if (position != null)
            {
                _arpaContext.Remove(position);
                await _arpaContext.SaveChangesAsync(default);
            }
        }

        public async Task<IEnumerable<StageSetupPositionDto>> BulkUpdateAsync(Guid stageSetupId, BulkUpdatePositionsDto bulkUpdateDto)
        {
            var command = _mapper.Map<BulkUpdateStageSetupPositions.Command>(bulkUpdateDto);
            command.StageSetupId = stageSetupId;

            await _mediator.Send(command);

            // Reload all positions with includes to get MusicianProfile data
            var positionsWithIncludes = await _arpaContext.Set<StageSetupPosition>()
                .Where(p => p.StageSetupId == stageSetupId)
                .Include(p => p.MusicianProfile)
                    .ThenInclude(mp => mp.Person)
                .Include(p => p.MusicianProfile)
                    .ThenInclude(mp => mp.Instrument)
                .Include(p => p.MusicianProfile)
                    .ThenInclude(mp => mp.Qualification)
                        .ThenInclude(q => q.SelectValue)
                .ToListAsync();

            return _mapper.Map<IEnumerable<StageSetupPositionDto>>(positionsWithIncludes);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Application.StageSetupApplication.Interfaces;
using Orso.Arpa.Application.StageSetupApplication.Model;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.StageSetupDomain.Commands;
using Orso.Arpa.Domain.StageSetupDomain.Model;

namespace Orso.Arpa.Application.StageSetupApplication.Services
{
    public class StageSetupEquipmentService : IStageSetupEquipmentService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IArpaContext _arpaContext;

        public StageSetupEquipmentService(
            IMediator mediator,
            IMapper mapper,
            IArpaContext arpaContext)
        {
            _mediator = mediator;
            _mapper = mapper;
            _arpaContext = arpaContext;
        }

        public async Task<IEnumerable<StageSetupEquipmentDto>> GetBySetupAsync(Guid stageSetupId)
        {
            var equipment = await _arpaContext.Set<StageSetupEquipment>()
                .Where(e => e.StageSetupId == stageSetupId)
                .OrderBy(e => e.CreatedAt)
                .ToListAsync();

            return _mapper.Map<IEnumerable<StageSetupEquipmentDto>>(equipment);
        }

        public async Task<StageSetupEquipmentDto> CreateAsync(Guid stageSetupId, StageSetupEquipmentCreateDto createDto)
        {
            var command = _mapper.Map<CreateStageSetupEquipment.Command>(createDto);
            command.StageSetupId = stageSetupId;

            var result = await _mediator.Send(command);
            return _mapper.Map<StageSetupEquipmentDto>(result);
        }

        public async Task ModifyAsync(Guid id, StageSetupEquipmentModifyDto modifyDto)
        {
            var equipment = await _arpaContext.Set<StageSetupEquipment>().FindAsync(id);
            if (equipment == null)
            {
                throw new InvalidOperationException($"Equipment with id {id} not found");
            }

            equipment.Update(modifyDto.PositionX, modifyDto.PositionY, modifyDto.Rotation);
            await _arpaContext.SaveChangesAsync(default);
        }

        public async Task DeleteAsync(Guid id)
        {
            var equipment = await _arpaContext.Set<StageSetupEquipment>().FindAsync(id);
            if (equipment != null)
            {
                _arpaContext.Remove(equipment);
                await _arpaContext.SaveChangesAsync(default);
            }
        }

        public async Task<IEnumerable<StageSetupEquipmentDto>> BulkUpdateAsync(Guid stageSetupId, BulkUpdateEquipmentDto bulkUpdateDto)
        {
            // Get existing equipment for this setup
            var existingEquipment = await _arpaContext.Set<StageSetupEquipment>()
                .Where(e => e.StageSetupId == stageSetupId)
                .ToListAsync();

            var existingIds = existingEquipment.Select(e => e.Id).ToHashSet();
            var incomingIds = bulkUpdateDto.Equipment.Where(e => e.Id != Guid.Empty).Select(e => e.Id).ToHashSet();

            // Delete equipment that's no longer in the list
            var toDelete = existingEquipment.Where(e => !incomingIds.Contains(e.Id)).ToList();
            foreach (var eq in toDelete)
            {
                _arpaContext.Remove(eq);
            }

            // Update existing or create new
            foreach (var item in bulkUpdateDto.Equipment)
            {
                if (item.Id != Guid.Empty && existingIds.Contains(item.Id))
                {
                    // Update existing
                    var eq = existingEquipment.First(e => e.Id == item.Id);
                    eq.Update(item.PositionX, item.PositionY, item.Rotation);
                }
                else
                {
                    // Create new
                    var command = new CreateStageSetupEquipment.Command
                    {
                        StageSetupId = stageSetupId,
                        EquipmentId = item.EquipmentId,
                        PositionX = item.PositionX,
                        PositionY = item.PositionY,
                        Rotation = item.Rotation
                    };
                    await _mediator.Send(command);
                }
            }

            await _arpaContext.SaveChangesAsync(default);

            // Return updated list
            return await GetBySetupAsync(stageSetupId);
        }
    }
}

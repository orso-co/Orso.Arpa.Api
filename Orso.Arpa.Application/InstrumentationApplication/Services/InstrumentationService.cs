using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Application.InstrumentationApplication.Model;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.InstrumentationDomain.Commands;
using Orso.Arpa.Domain.InstrumentationDomain.Model;
using Orso.Arpa.Domain.ProjectDomain.Enums;

namespace Orso.Arpa.Application.InstrumentationApplication.Services
{
    public interface IInstrumentationService
    {
        Task<IEnumerable<InstrumentationDto>> GetAsync(bool includeTemplates = true);
        Task<InstrumentationDto> GetByIdAsync(Guid id);
        Task<IEnumerable<InstrumentationDto>> GetByProjectIdAsync(Guid projectId);
        Task<InstrumentationDto> CreateAsync(InstrumentationCreateDto dto);
        Task ModifyAsync(InstrumentationModifyDto dto);
        Task DeleteAsync(Guid id);
        Task<InstrumentationDto> CopyAsync(Guid sourceId, InstrumentationCopyDto dto);

        Task<InstrumentationPositionDto> AddPositionAsync(Guid instrumentationId, InstrumentationPositionCreateDto dto);
        Task ModifyPositionAsync(Guid instrumentationId, Guid positionId, InstrumentationPositionModifyDto dto);
        Task RemovePositionAsync(Guid instrumentationId, Guid positionId);
        Task ReorderPositionsAsync(Guid instrumentationId, List<Guid> orderedPositionIds);

        Task<InstrumentationPositionDoublingDto> AddDoublingAsync(Guid positionId, Guid sectionId);
        Task RemoveDoublingAsync(Guid positionId, Guid doublingId);

        Task<InstrumentationStatusDto> GetStatusAsync(Guid instrumentationId);
    }

    public class InstrumentationService : IInstrumentationService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IArpaContext _arpaContext;

        public InstrumentationService(IMediator mediator, IMapper mapper, IArpaContext context)
        {
            _mediator = mediator;
            _mapper = mapper;
            _arpaContext = context;
        }

        public async Task<IEnumerable<InstrumentationDto>> GetAsync(bool includeTemplates = true)
        {
            IQueryable<Instrumentation> query = _arpaContext.Instrumentations
                .Include(i => i.Positions.OrderBy(p => p.SortOrder))
                    .ThenInclude(p => p.Section)
                .Include(i => i.Positions)
                    .ThenInclude(p => p.Doublings)
                        .ThenInclude(d => d.Section);

            if (!includeTemplates)
            {
                query = query.Where(i => !i.IsTemplate);
            }

            List<Instrumentation> instrumentations = await query
                .OrderBy(i => i.Name)
                .ToListAsync();

            return _mapper.Map<IEnumerable<InstrumentationDto>>(instrumentations);
        }

        public async Task<InstrumentationDto> GetByIdAsync(Guid id)
        {
            Instrumentation instrumentation = await _arpaContext.Instrumentations
                .Include(i => i.Positions.OrderBy(p => p.SortOrder))
                    .ThenInclude(p => p.Section)
                .Include(i => i.Positions)
                    .ThenInclude(p => p.Qualification)
                        .ThenInclude(q => q.SelectValue)
                .Include(i => i.Positions)
                    .ThenInclude(p => p.Doublings)
                        .ThenInclude(d => d.Section)
                .FirstOrDefaultAsync(i => i.Id == id);

            return _mapper.Map<InstrumentationDto>(instrumentation);
        }

        public async Task<IEnumerable<InstrumentationDto>> GetByProjectIdAsync(Guid projectId)
        {
            List<Instrumentation> instrumentations = await _arpaContext.Instrumentations
                .Where(i => i.ProjectId == projectId)
                .Include(i => i.Positions.OrderBy(p => p.SortOrder))
                    .ThenInclude(p => p.Section)
                .Include(i => i.Positions)
                    .ThenInclude(p => p.Doublings)
                        .ThenInclude(d => d.Section)
                .OrderBy(i => i.Name)
                .ToListAsync();

            return _mapper.Map<IEnumerable<InstrumentationDto>>(instrumentations);
        }

        public async Task<InstrumentationDto> CreateAsync(InstrumentationCreateDto dto)
        {
            var command = _mapper.Map<CreateInstrumentation.Command>(dto);
            Instrumentation created = await _mediator.Send(command);
            return _mapper.Map<InstrumentationDto>(created);
        }

        public async Task ModifyAsync(InstrumentationModifyDto dto)
        {
            var command = _mapper.Map<ModifyInstrumentation.Command>(dto);
            await _mediator.Send(command);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _mediator.Send(new DeleteInstrumentation.Command { Id = id });
        }

        public async Task<InstrumentationDto> CopyAsync(Guid sourceId, InstrumentationCopyDto dto)
        {
            var command = new CopyInstrumentation.Command
            {
                SourceId = sourceId,
                ProjectId = dto.ProjectId,
                Name = dto.Name
            };

            Instrumentation copy = await _mediator.Send(command);

            // Reload with includes
            return await GetByIdAsync(copy.Id);
        }

        public async Task<InstrumentationPositionDto> AddPositionAsync(Guid instrumentationId, InstrumentationPositionCreateDto dto)
        {
            var command = new AddInstrumentationPosition.Command
            {
                InstrumentationId = instrumentationId,
                SectionId = dto.SectionId,
                Quantity = dto.Quantity,
                QualificationId = dto.QualificationId,
                Label = dto.Label,
                Comment = dto.Comment
            };

            InstrumentationPosition position = await _mediator.Send(command);

            // Reload with Section included
            InstrumentationPosition reloaded = await _arpaContext.InstrumentationPositions
                .Include(p => p.Section)
                .Include(p => p.Doublings)
                    .ThenInclude(d => d.Section)
                .FirstOrDefaultAsync(p => p.Id == position.Id);

            return _mapper.Map<InstrumentationPositionDto>(reloaded);
        }

        public async Task ModifyPositionAsync(Guid instrumentationId, Guid positionId, InstrumentationPositionModifyDto dto)
        {
            var command = new ModifyInstrumentationPosition.Command
            {
                Id = positionId,
                InstrumentationId = instrumentationId,
                SectionId = dto.SectionId,
                Quantity = dto.Quantity,
                QualificationId = dto.QualificationId,
                Label = dto.Label,
                Comment = dto.Comment
            };

            await _mediator.Send(command);
        }

        public async Task RemovePositionAsync(Guid instrumentationId, Guid positionId)
        {
            await _mediator.Send(new RemoveInstrumentationPosition.Command
            {
                InstrumentationId = instrumentationId,
                PositionId = positionId
            });
        }

        public async Task ReorderPositionsAsync(Guid instrumentationId, List<Guid> orderedPositionIds)
        {
            await _mediator.Send(new ReorderInstrumentationPositions.Command
            {
                InstrumentationId = instrumentationId,
                OrderedPositionIds = orderedPositionIds
            });
        }

        public async Task<InstrumentationPositionDoublingDto> AddDoublingAsync(Guid positionId, Guid sectionId)
        {
            var command = new AddPositionDoubling.Command
            {
                PositionId = positionId,
                SectionId = sectionId
            };

            InstrumentationPositionDoubling doubling = await _mediator.Send(command);

            // Reload with Section included
            InstrumentationPositionDoubling reloaded = await _arpaContext.InstrumentationPositionDoublings
                .Include(d => d.Section)
                .FirstOrDefaultAsync(d => d.Id == doubling.Id);

            return _mapper.Map<InstrumentationPositionDoublingDto>(reloaded);
        }

        public async Task RemoveDoublingAsync(Guid positionId, Guid doublingId)
        {
            await _mediator.Send(new RemovePositionDoubling.Command
            {
                PositionId = positionId,
                DoublingId = doublingId
            });
        }

        public async Task<InstrumentationStatusDto> GetStatusAsync(Guid instrumentationId)
        {
            // Load instrumentation with positions and doublings
            Instrumentation instrumentation = await _arpaContext.Instrumentations
                .Include(i => i.Positions.OrderBy(p => p.SortOrder))
                    .ThenInclude(p => p.Section)
                .Include(i => i.Positions)
                    .ThenInclude(p => p.Qualification)
                        .ThenInclude(q => q.SelectValue)
                .Include(i => i.Positions)
                    .ThenInclude(p => p.Doublings)
                .FirstOrDefaultAsync(i => i.Id == instrumentationId);

            if (instrumentation == null || instrumentation.ProjectId == null)
            {
                return new InstrumentationStatusDto { InstrumentationId = instrumentationId };
            }

            // Load accepted project participations with musician profiles
            var acceptedParticipations = await _arpaContext.Set<Domain.ProjectDomain.Model.ProjectParticipation>()
                .Where(pp => pp.ProjectId == instrumentation.ProjectId
                    && pp.ParticipationStatusInner == ProjectParticipationStatusInner.Acceptance
                    && pp.ParticipationStatusInternal == ProjectParticipationStatusInternal.Acceptance)
                .Include(pp => pp.MusicianProfile)
                    .ThenInclude(mp => mp.Instrument)
                .Include(pp => pp.MusicianProfile)
                    .ThenInclude(mp => mp.DoublingInstruments)
                .Include(pp => pp.MusicianProfile)
                    .ThenInclude(mp => mp.Qualification)
                        .ThenInclude(q => q.SelectValue)
                .Where(pp => !pp.MusicianProfile.Deleted)
                .ToListAsync();

            // Build qualification SortOrder lookup for comparison
            var qualificationSortOrders = await _arpaContext.Set<Domain.SelectValueDomain.Model.SelectValueMapping>()
                .Include(m => m.SelectValue)
                .Include(m => m.SelectValueCategory)
                .Where(m => m.SelectValueCategory.Table == "MusicianProfile" && m.SelectValueCategory.Property == "Qualification")
                .ToDictionaryAsync(m => m.Id, m => m.SortOrder ?? 0);

            var positionStatuses = new List<InstrumentationPositionStatusDto>();
            var usedPersonIds = new HashSet<Guid>();

            foreach (InstrumentationPosition position in instrumentation.Positions)
            {
                // Find matching musicians for this position
                var matchingSectionIds = new HashSet<Guid> { position.SectionId };
                foreach (var doubling in position.Doublings)
                {
                    matchingSectionIds.Add(doubling.SectionId);
                }

                var matchingProfiles = acceptedParticipations
                    .Where(pp =>
                    {
                        var mp = pp.MusicianProfile;
                        // Match primary instrument
                        if (matchingSectionIds.Contains(mp.InstrumentId))
                            return true;
                        // Match doubling instruments
                        if (mp.DoublingInstruments != null && mp.DoublingInstruments.Any(d => matchingSectionIds.Contains(d.SectionId)))
                            return true;
                        return false;
                    })
                    .Select(pp => pp.MusicianProfile)
                    .Where(mp => !usedPersonIds.Contains(mp.PersonId))
                    .ToList();

                int filled = Math.Min(matchingProfiles.Count, position.Quantity);

                // Track used persons (up to quantity)
                foreach (var mp in matchingProfiles.Take(position.Quantity))
                {
                    usedPersonIds.Add(mp.PersonId);
                }

                // Determine qualification info
                string requiredQualification = null;
                bool qualificationMet = true;
                var actualQualifications = new List<string>();

                if (position.QualificationId.HasValue && position.Qualification?.SelectValue != null)
                {
                    requiredQualification = position.Qualification.SelectValue.Name;
                    int requiredSortOrder = qualificationSortOrders.GetValueOrDefault(position.QualificationId.Value, 0);

                    foreach (var mp in matchingProfiles.Take(position.Quantity))
                    {
                        string qualName = mp.Qualification?.SelectValue?.Name ?? "Unknown";
                        actualQualifications.Add(qualName);

                        if (mp.QualificationId.HasValue)
                        {
                            int actualSortOrder = qualificationSortOrders.GetValueOrDefault(mp.QualificationId.Value, 0);
                            if (actualSortOrder < requiredSortOrder)
                            {
                                qualificationMet = false;
                            }
                        }
                        else
                        {
                            qualificationMet = false;
                        }
                    }

                    if (filled < position.Quantity)
                    {
                        qualificationMet = false;
                    }
                }
                else
                {
                    foreach (var mp in matchingProfiles.Take(position.Quantity))
                    {
                        actualQualifications.Add(mp.Qualification?.SelectValue?.Name ?? "Unknown");
                    }
                }

                positionStatuses.Add(new InstrumentationPositionStatusDto
                {
                    PositionId = position.Id,
                    SectionId = position.SectionId,
                    SectionName = position.Section?.Name ?? "",
                    Label = position.Label,
                    Required = position.Quantity,
                    Filled = filled,
                    RequiredQualification = requiredQualification,
                    ActualQualifications = actualQualifications,
                    QualificationMet = qualificationMet,
                });
            }

            return new InstrumentationStatusDto
            {
                InstrumentationId = instrumentationId,
                Positions = positionStatuses,
                TotalRequired = positionStatuses.Sum(p => p.Required),
                TotalFilled = positionStatuses.Sum(p => p.Filled),
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Application.SetlistApplication.Model;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.MusicLibraryDomain.Commands;
using Orso.Arpa.Domain.MusicLibraryDomain.Model;

namespace Orso.Arpa.Application.SetlistApplication.Services
{
    public interface ISetlistService
    {
        Task<IEnumerable<SetlistDto>> GetAsync(bool includeTemplates = true);
        Task<SetlistDto> GetByIdAsync(Guid id);
        Task<SetlistDto> CreateAsync(SetlistCreateDto dto);
        Task ModifyAsync(SetlistModifyDto dto);
        Task DeleteAsync(Guid id);
        Task<SetlistPieceDto> AddPieceAsync(Guid setlistId, AddPieceToSetlistDto dto);
        Task RemovePieceAsync(Guid setlistId, Guid setlistPieceId);
        Task ReorderPiecesAsync(Guid setlistId, List<Guid> orderedPieceIds);
    }

    public class SetlistService : ISetlistService
    {
        protected readonly IMediator _mediator;
        protected readonly IMapper _mapper;
        protected readonly IArpaContext _arpaContext;

        public SetlistService(IMediator mediator, IMapper mapper, IArpaContext context)
        {
            _mediator = mediator;
            _mapper = mapper;
            _arpaContext = context;
        }

        public async Task<IEnumerable<SetlistDto>> GetAsync(bool includeTemplates = true)
        {
            IQueryable<Setlist> query = _arpaContext.Setlists
                .Include(s => s.Pieces.OrderBy(p => p.SortOrder))
                    .ThenInclude(p => p.MusicPiece);

            if (!includeTemplates)
            {
                query = query.Where(s => !s.IsTemplate);
            }

            List<Setlist> setlists = await query
                .OrderBy(s => s.Name)
                .ToListAsync();

            return _mapper.Map<IEnumerable<SetlistDto>>(setlists);
        }

        public async Task<SetlistDto> GetByIdAsync(Guid id)
        {
            Setlist setlist = await _arpaContext.Setlists
                .Include(s => s.Pieces.OrderBy(p => p.SortOrder))
                    .ThenInclude(p => p.MusicPiece)
                        .ThenInclude(mp => mp.Epoch)
                            .ThenInclude(e => e.SelectValue)
                .Include(s => s.Pieces)
                    .ThenInclude(p => p.MusicPiece)
                        .ThenInclude(mp => mp.Genre)
                            .ThenInclude(g => g.SelectValue)
                .FirstOrDefaultAsync(s => s.Id == id);

            return _mapper.Map<SetlistDto>(setlist);
        }

        public async Task<SetlistDto> CreateAsync(SetlistCreateDto dto)
        {
            CreateSetlist.Command command = _mapper.Map<CreateSetlist.Command>(dto);
            Setlist createdSetlist = await _mediator.Send(command);
            return _mapper.Map<SetlistDto>(createdSetlist);
        }

        public async Task ModifyAsync(SetlistModifyDto dto)
        {
            ModifySetlist.Command command = _mapper.Map<ModifySetlist.Command>(dto);
            await _mediator.Send(command);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _mediator.Send(new DeleteSetlist.Command { Id = id });
        }

        public async Task<SetlistPieceDto> AddPieceAsync(Guid setlistId, AddPieceToSetlistDto dto)
        {
            var command = _mapper.Map<AddPieceToSetlist.Command>(dto);
            command.SetlistId = setlistId;

            SetlistPiece piece = await _mediator.Send(command);

            // Reload with MusicPiece included
            SetlistPiece reloaded = await _arpaContext.SetlistPieces
                .Include(sp => sp.MusicPiece)
                .FirstOrDefaultAsync(sp => sp.Id == piece.Id);

            return _mapper.Map<SetlistPieceDto>(reloaded);
        }

        public async Task RemovePieceAsync(Guid setlistId, Guid setlistPieceId)
        {
            await _mediator.Send(new RemovePieceFromSetlist.Command
            {
                SetlistId = setlistId,
                SetlistPieceId = setlistPieceId
            });
        }

        public async Task ReorderPiecesAsync(Guid setlistId, List<Guid> orderedPieceIds)
        {
            await _mediator.Send(new ReorderSetlistPieces.Command
            {
                SetlistId = setlistId,
                OrderedPieceIds = orderedPieceIds
            });
        }
    }
}

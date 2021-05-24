using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.MusicianProfiles;

namespace Orso.Arpa.Application.Services
{
    public class MusicianProfileService : BaseReadOnlyService<
        MusicianProfileDto,
        MusicianProfile>, IMusicianProfileService
    {
        public MusicianProfileService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        public async Task<MusicianProfileDto> CreateAsync(MusicianProfileCreateDto createDto)
        {
            Create.Command command = _mapper.Map<Create.Command>(createDto);
            MusicianProfile createdEntity = await _mediator.Send(command);
            return _mapper.Map<MusicianProfileDto>(createdEntity);
        }

        public Task<IEnumerable<MusicianProfileDto>> GetByPersonAsync(Guid personId, bool includeDeactivated)
        {
            Expression<Func<MusicianProfile, bool>> predicate = includeDeactivated ? mp => mp.PersonId == personId : mp => mp.PersonId == personId && !mp.IsDeactivated;

            return GetAsync(predicate, orderBy: profile => profile.OrderByDescending(p => p.IsMainProfile));
        }
    }
}

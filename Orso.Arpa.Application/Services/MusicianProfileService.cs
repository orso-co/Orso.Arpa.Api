using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Application.MyMusicianProfileApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.MusicianProfiles;

namespace Orso.Arpa.Application.Services
{
    public class MusicianProfileService : BaseService<
        MusicianProfileDto,
        MusicianProfile,
        MusicianProfileCreateBodyDto,
        Create.Command,
        MusicianProfileModifyDto,
        MusicianProfileModifyBodyDto,
        Modify.Command>, IMusicianProfileService
    {
        public MusicianProfileService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        public async Task<MyMusicianProfileDto> CreateAsync(MyMusicianProfileCreateDto createDto)
        {
            Create.Command command = _mapper.Map<Create.Command>(createDto);
            MusicianProfile createdEntity = await _mediator.Send(command);
            return _mapper.Map<MyMusicianProfileDto>(createdEntity);
        }

        public Task<IEnumerable<MusicianProfileDto>> GetAsync(Guid personId)
        {
            return GetAsync(profile => profile.PersonId == personId);
        }

        public Task<IEnumerable<MyMusicianProfileDto>> GetMyAsync(Guid personId)
        {
            throw new NotImplementedException();
        }

        public Task<MyMusicianProfileDto> GetMyByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}

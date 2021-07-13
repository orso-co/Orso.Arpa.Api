using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.MeApplication;
using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Application.MyMusicianProfileApplication;
using Orso.Arpa.Application.ProjectApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.GenericHandlers;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Logic.Me;

namespace Orso.Arpa.Application.Services
{
    public class MeService : IMeService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;

        public MeService(
            IMediator mediator,
            IMapper mapper,
            IUserAccessor userAccessor)
        {
            _mediator = mediator;
            _mapper = mapper;
            _userAccessor = userAccessor;
        }

        public async Task<MyUserProfileDto> GetMyUserProfileAsync()
        {
            User user = await _mediator.Send(new UserProfile.Query());
            return _mapper.Map<MyUserProfileDto>(user);
        }

        public async Task ModifyMyUserProfileAsync(MyUserProfileModifyDto userProfileModifyDto)
        {
            Orso.Arpa.Domain.Logic.Me.Modify.Command command = _mapper.Map<Domain.Logic.Me.Modify.Command>(userProfileModifyDto);
            await _mediator.Send(command);
        }

        public async Task<MyAppointmentListDto> GetMyAppointmentsAsync(int? limit, int? offset)
        {
            var treeQuery = new Domain.Logic.Sections.FlattenedTree.Query();
            IEnumerable<ITree<Section>> flattenedTree = await _mediator.Send(treeQuery);
            Person currentPerson = await _userAccessor.GetCurrentPersonAsync();

            Tuple<IQueryable<Appointment>, int> appointmentTuple = await _mediator.Send(
                new AppointmentList.Query(limit, offset, flattenedTree, currentPerson));

            IList<MyAppointmentDto> myAppointmentDtos = await appointmentTuple.Item1
                .ProjectTo<MyAppointmentDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            foreach (MyAppointmentDto dto in myAppointmentDtos)
            {
                AppointmentParticipation participation = await _mediator.Send(new Domain.Logic.AppointmentParticipations.Details.Query(
                    dto.Id, currentPerson.Id));
                if (participation != null)
                {
                    _mapper.Map(participation, dto);
                }
            }

            return new MyAppointmentListDto
            {
                TotalRecordsCount = appointmentTuple.Item2,
                UserAppointments = myAppointmentDtos
            };
        }

        public async Task SetMyAppointmentParticipationPredictionAsync(SetMyProjectAppointmentPredictionDto setParticipationPredictionDto)
        {
            Domain.Logic.AppointmentParticipations.SetPrediction.Command command = _mapper
                .Map<Domain.Logic.AppointmentParticipations.SetPrediction.Command>(setParticipationPredictionDto);

            command.PersonId = _userAccessor.PersonId;
            await _mediator.Send(command);
        }

        public async Task<SendQRCode.QrCodeFile> SendMyQrCodeAsync()
        {
            var command = new SendQRCode.Command { Username = _userAccessor.UserName };
            return await _mediator.Send(command);
        }

        public async Task<MyMusicianProfileDto> CreateMusicianProfileAsync(MyMusicianProfileCreateDto createDto)
        {
            Domain.Logic.MusicianProfiles.Create.Command command = _mapper.Map<Domain.Logic.MusicianProfiles.Create.Command>(createDto);

            command.PersonId = _userAccessor.PersonId;

            MusicianProfile createdEntity = await _mediator.Send(command);
            foreach (MyDoublingInstrumentCreateBodyDto doublingInstrument in createDto.DoublingInstruments)
            {
                Domain.Logic.MusicianProfileSections.Create.Command doublingInstrumentCommand = _mapper.Map<Orso.Arpa.Domain.Logic.MusicianProfileSections.Create.Command>(doublingInstrument);
                doublingInstrumentCommand.MusicianProfileId = createdEntity.Id;
                createdEntity.DoublingInstruments.Add(await _mediator.Send(doublingInstrumentCommand));
            }
            return _mapper.Map<MyMusicianProfileDto>(createdEntity);
        }

        public async Task<MyMusicianProfileDto> GetMyMusicianProfileAsync(Guid id)
        {
            var query = new MusicianProfileById.Query { Id = id, PersonId = _userAccessor.PersonId };
            MusicianProfile musicialProfile = await _mediator.Send(query);
            return _mapper.Map<MyMusicianProfileDto>(musicialProfile);
        }

        public async Task<IEnumerable<MyMusicianProfileDto>> GetMyMusicianProfilesAsync(bool includeDeactivated)
        {
            Expression<Func<MusicianProfile, bool>> predicate = includeDeactivated ? mp => mp.PersonId == _userAccessor.PersonId : mp => mp.PersonId == _userAccessor.PersonId && !mp.IsDeactivated;

            var query = new List.Query<MusicianProfile>(predicate, orderBy: mp => mp.OrderByDescending(m => m.IsMainProfile));

            IQueryable<MusicianProfile> musicianProfiles = await _mediator.Send(query);

            return await musicianProfiles
                .ProjectTo<MyMusicianProfileDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<ProjectParticipationDto> SetMyProjectParticipationAsync(SetMyProjectParticipationDto myProjectParticipationDto)
        {
            SetProjectParticipation.Command command = _mapper
                .Map<SetProjectParticipation.Command>(myProjectParticipationDto);

            command.PersonId = _userAccessor.PersonId;
            ProjectParticipation projectParticipation = await _mediator.Send(command);
            return _mapper.Map<ProjectParticipationDto>(projectParticipation);
        }

        public async Task<MyMusicianProfileDto> UpdateMusicianProfileAsync(MyMusicianProfileModifyDto musicianProfileModifyDto)
        {
            MusicianProfile existingMusicianProfile = await _mediator.Send(new MusicianProfileById.Query { Id = musicianProfileModifyDto.Id, PersonId = _userAccessor.PersonId });

            ModifyMusicianProfile.Command command = _mapper.Map<ModifyMusicianProfile.Command>(musicianProfileModifyDto);

            command.InstrumentId = existingMusicianProfile.InstrumentId;
            command.ExistingMusicianProfile = existingMusicianProfile;

            await _mediator.Send(command);
            return await GetMyMusicianProfileAsync(command.Id);
        }

        public async Task<MyDoublingInstrumentDto> CreateDoublingInstrumentAsync(MyDoublingInstrumentCreateDto myDoublingInstrumentCreateDto)
        {
            Domain.Logic.MusicianProfileSections.Create.Command command = _mapper.Map<Domain.Logic.MusicianProfileSections.Create.Command>(myDoublingInstrumentCreateDto);
            MusicianProfileSection doublingInstrument = await _mediator.Send(command);
            return _mapper.Map<MyDoublingInstrumentDto>(doublingInstrument);
        }

        public async Task ModifyDoublingInstrumentAsync(MyDoublingInstrumentModifyDto myDoublingInstrumentModifyDto)
        {
            ModifyDoublingInstrument.Command command = _mapper.Map<ModifyDoublingInstrument.Command>(myDoublingInstrumentModifyDto);
            await _mediator.Send(command);
        }

        public async Task AddDocumentToMusicienProfileAsync(MyMusicianProfileAddDocumentDto addDocumentDto)
        {
            AddDocumentToMusicianProfile.Command command = _mapper.Map<AddDocumentToMusicianProfile.Command>(addDocumentDto);
            await _mediator.Send(command);
        }

        public async Task RemoveDocumentFromMusicianProfileAsync(MyMusicianProfileRemoveDocumentDto removeDocumentDto)
        {
            RemoveDocumentFromMusicianProfile.Command command = _mapper.Map<RemoveDocumentFromMusicianProfile.Command>(removeDocumentDto);
            await _mediator.Send(command);
        }
    }
}

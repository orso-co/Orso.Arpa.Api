using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.MeApplication;
using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Application.MyMusicianProfileApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.GenericHandlers;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Logic.Me;
using Orso.Arpa.Misc;

namespace Orso.Arpa.Application.Services
{
    public class MeService : IMeService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;
        private readonly IDateTimeProvider _dateTimeProvider;

        public MeService(
            IMediator mediator,
            IMapper mapper,
            IUserAccessor userAccessor,
            IDateTimeProvider dateTimeProvider)
        {
            _mediator = mediator;
            _mapper = mapper;
            _userAccessor = userAccessor;
            _dateTimeProvider = dateTimeProvider;
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

        public async Task<MyAppointmentListDto> GetMyAppointmentsAsync(int? limit, int? offset, bool passed)
        {
            var treeQuery = new Domain.Logic.Sections.FlattenedTree.Query();
            IEnumerable<ITree<Section>> flattenedTree = await _mediator.Send(treeQuery);
            Person currentPerson = await _userAccessor.GetCurrentPersonAsync();

            (IList<Appointment> userAppointments, int totalCount) appointmentTuple = await _mediator.Send(
                new AppointmentList.Query(limit, offset, passed, flattenedTree, currentPerson));

            IList<MyAppointmentDto> myAppointmentDtos = _mapper.Map<IList<MyAppointmentDto>>(appointmentTuple.userAppointments);

            foreach (MyAppointmentDto dto in myAppointmentDtos)
            {
                AppointmentParticipation participation = await _mediator.Send(new Domain.Logic.AppointmentParticipations.Details.Query(
                    dto.Id, currentPerson.Id));
                if (participation != null)
                {
                    dto.Result = participation.Result != null ? participation.Result.SelectValue.Name : null;
                    dto.PredictionId = participation.PredictionId;
                }
            }

            return new MyAppointmentListDto
            {
                TotalRecordsCount = appointmentTuple.totalCount,
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

        public async Task<SendQRCode.QrCodeFile> GetMyQrCodeAsync(bool sendEmail)
        {
            var command = new SendQRCode.Command { Username = _userAccessor.UserName, SendEmail = sendEmail };
            return await _mediator.Send(command);
        }

        public async Task<MyMusicianProfileDto> CreateMusicianProfileAsync(MyMusicianProfileCreateDto createDto)
        {
            Domain.Logic.MusicianProfiles.Create.Command command = _mapper.Map<Domain.Logic.MusicianProfiles.Create.Command>(createDto);

            command.PersonId = _userAccessor.PersonId;

            MusicianProfile createdEntity = await _mediator.Send(command);
            foreach (MyDoublingInstrumentCreateBodyDto doublingInstrument in createDto.DoublingInstruments)
            {
                Domain.Logic.MusicianProfileSections.Create.Command doublingInstrumentCommand = _mapper.Map<Domain.Logic.MusicianProfileSections.Create.Command>(doublingInstrument);
                doublingInstrumentCommand.MusicianProfileId = createdEntity.Id;
                await _mediator.Send(doublingInstrumentCommand);
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
            Expression<Func<MusicianProfile, bool>> predicate = includeDeactivated
                ? mp => mp.PersonId == _userAccessor.PersonId
                : mp => mp.PersonId == _userAccessor.PersonId && (mp.Deactivation == null || mp.Deactivation.DeactivationStart > _dateTimeProvider.GetUtcNow());

            var query = new List.Query<MusicianProfile>(predicate, orderBy: mp => mp.OrderByDescending(m => m.IsMainProfile));

            IQueryable<MusicianProfile> musicianProfiles = await _mediator.Send(query);

            List<MusicianProfile> profiles = await musicianProfiles.ToListAsync();

            return _mapper.Map<IEnumerable<MyMusicianProfileDto>>(profiles);
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

        public async Task AddDocumentToMusicianProfileAsync(MyMusicianProfileAddDocumentDto addDocumentDto)
        {
            AddDocumentToMusicianProfile.Command command = _mapper.Map<AddDocumentToMusicianProfile.Command>(addDocumentDto);
            await _mediator.Send(command);
        }

        public async Task RemoveDocumentFromMusicianProfileAsync(MyMusicianProfileRemoveDocumentDto removeDocumentDto)
        {
            RemoveDocumentFromMusicianProfile.Command command = _mapper.Map<RemoveDocumentFromMusicianProfile.Command>(removeDocumentDto);
            await _mediator.Send(command);
        }

        public async Task<RegionPreferenceDto> CreateRegionPreferenceAsync(MyRegionPreferenceCreateDto myRegionPreferenceCreateDto)
        {
            CreateRegionPreference.Command command = _mapper.Map<CreateRegionPreference.Command>(myRegionPreferenceCreateDto);
            RegionPreference createdEntity = await _mediator.Send(command);
            return _mapper.Map<RegionPreferenceDto>(createdEntity);
        }

        public async Task ModifyRegionPreferenceAsync(MyRegionPreferenceModifyDto myRegionPreferenceModifyDto)
        {
            ModifyRegionPreference.Command command = _mapper.Map<ModifyRegionPreference.Command>(myRegionPreferenceModifyDto);
            await _mediator.Send(command);
        }

        public async Task RemoveRegionPreferenceAsync(MyRegionPreferenceRemoveDto myRegionPreferenceRemoveDto)
        {
            RemoveRegionPreference.Command command = _mapper.Map<RemoveRegionPreference.Command>(myRegionPreferenceRemoveDto);
            await _mediator.Send(command);
        }
    }
}

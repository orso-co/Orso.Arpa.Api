using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Application.MeApplication.Interfaces;
using Orso.Arpa.Application.MeApplication.Model;
using Orso.Arpa.Application.MusicianProfileApplication.Model;
using Orso.Arpa.Domain.AppointmentDomain.Commands;
using Orso.Arpa.Domain.AppointmentDomain.Queries;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.GenericHandlers;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.MusicianProfileDomain.Commands;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;
using Orso.Arpa.Domain.MusicianProfileDomain.Notifications;
using Orso.Arpa.Domain.MusicianProfileDomain.Queries;
using Orso.Arpa.Domain.PersonDomain.Model;
using Orso.Arpa.Domain.RegionDomain.Commands;
using Orso.Arpa.Domain.RegionDomain.Model;
using Orso.Arpa.Domain.SectionDomain.Model;
using Orso.Arpa.Domain.SectionDomain.Queries;
using Orso.Arpa.Domain.UserDomain.Commands;
using Orso.Arpa.Domain.UserDomain.Model;
using Orso.Arpa.Domain.UserDomain.Queries;
using Orso.Arpa.Misc;
using Orso.Arpa.Domain.AppointmentDomain.Enums;

namespace Orso.Arpa.Application.MeApplication.Services
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
            User user = await _mediator.Send(new GetMyUser.Query());
            return _mapper.Map<MyUserProfileDto>(user);
        }

        public async Task ModifyMyUserProfileAsync(MyUserProfileModifyDto modifyDto)
        {
            ModifyMyUser.Command command = _mapper.Map<ModifyMyUser.Command>(modifyDto);
            _ = await _mediator.Send(command);
        }

        public async Task<MyAppointmentListDto> GetMyAppointmentsAsync(int? limit, int? offset, bool passed)
        {
            var treeQuery = new ListFlattenedSectionTree.Query();
            IEnumerable<ITree<Section>> flattenedTree = await _mediator.Send(treeQuery);
            Person currentPerson = await _userAccessor.GetCurrentPersonAsync();

            (IList<Appointment> userAppointments, int totalCount) = await _mediator.Send(
                new ListMyAppointments.Query(limit, offset, passed, flattenedTree, currentPerson));

            IList<MyAppointmentDto> myAppointmentDtos = _mapper.Map<IList<MyAppointmentDto>>(userAppointments);

            foreach (MyAppointmentDto dto in myAppointmentDtos)
            {
                AppointmentParticipation participation = await _mediator.Send(new GetAppointmentParticipation.Query(
                    dto.Id, currentPerson.Id));
                if (participation != null)
                {
                    dto.Result = participation.Result;
                    dto.Prediction = participation.Prediction;
                    dto.CommentByPerformerInner = participation.CommentByPerformerInner;
                }
            }

            return new MyAppointmentListDto
            {
                TotalRecordsCount = totalCount,
                UserAppointments = myAppointmentDtos
            };
        }

        public async Task SetMyAppointmentParticipationPredictionAsync(SetMyAppointmentParticipationPredictionDto setParticipationPredictionDto)
        {
            SetAppointmentParticipationPrediction.Command command = _mapper
                .Map<SetAppointmentParticipationPrediction.Command>(setParticipationPredictionDto);

            command.PersonId = _userAccessor.PersonId;
            _ = await _mediator.Send(command);
        }

        public async Task<SendMyQRCode.QrCodeFile> GetMyQrCodeAsync(bool sendEmail)
        {
            var command = new SendMyQRCode.Command { Username = _userAccessor.UserName, SendEmail = sendEmail };
            return await _mediator.Send(command);
        }

        public async Task<MyMusicianProfileDto> CreateMusicianProfileAsync(MyMusicianProfileCreateDto musicianProfileCreateDto)
        {
            CreateMusicianProfile.Command command = _mapper.Map<CreateMusicianProfile.Command>(musicianProfileCreateDto);

            command.PersonId = _userAccessor.PersonId;

            MusicianProfile createdEntity = await _mediator.Send(command);
            foreach (MyDoublingInstrumentCreateBodyDto doublingInstrument in musicianProfileCreateDto.DoublingInstruments)
            {
                CreateMusicianProfileSection.Command doublingInstrumentCommand = _mapper.Map<CreateMusicianProfileSection.Command>(doublingInstrument);
                doublingInstrumentCommand.MusicianProfileId = createdEntity.Id;
                _ = await _mediator.Send(doublingInstrumentCommand);
            }

            var notification = new MusicianProfileCreatedNotification { MusicianProfile = createdEntity };
            await _mediator.Publish(notification);

            return _mapper.Map<MyMusicianProfileDto>(createdEntity);
        }

        public async Task<MyMusicianProfileDto> GetMyMusicianProfileAsync(Guid id)
        {
            var query = new GetMyMusicianProfile.Query { Id = id, PersonId = _userAccessor.PersonId };
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
            MusicianProfile existingMusicianProfile = await _mediator.Send(new GetMyMusicianProfile.Query { Id = musicianProfileModifyDto.Id, PersonId = _userAccessor.PersonId });

            ModifyMyMusicianProfile.Command command = _mapper.Map<ModifyMyMusicianProfile.Command>(musicianProfileModifyDto);

            command.InstrumentId = existingMusicianProfile.InstrumentId;
            command.ExistingMusicianProfile = existingMusicianProfile;

            _ = await _mediator.Send(command);
            return await GetMyMusicianProfileAsync(command.Id);
        }

        public async Task<MyDoublingInstrumentDto> CreateDoublingInstrumentAsync(MyDoublingInstrumentCreateDto myDoublingInstrumentCreateDto)
        {
            CreateMusicianProfileSection.Command command = _mapper.Map<CreateMusicianProfileSection.Command>(myDoublingInstrumentCreateDto);
            MusicianProfileSection doublingInstrument = await _mediator.Send(command);
            return _mapper.Map<MyDoublingInstrumentDto>(doublingInstrument);
        }

        public async Task ModifyDoublingInstrumentAsync(MyDoublingInstrumentModifyDto myDoublingInstrumentModifyDto)
        {
            ModifyMyDoublingInstrument.Command command = _mapper.Map<ModifyMyDoublingInstrument.Command>(myDoublingInstrumentModifyDto);
            _ = await _mediator.Send(command);
        }

        public async Task AddDocumentToMusicianProfileAsync(MyMusicianProfileAddDocumentDto addDocumentDto)
        {
            AddDocumentToMyMusicianProfile.Command command = _mapper.Map<AddDocumentToMyMusicianProfile.Command>(addDocumentDto);
            _ = await _mediator.Send(command);
        }

        public async Task RemoveDocumentFromMusicianProfileAsync(MyMusicianProfileRemoveDocumentDto removeDocumentDto)
        {
            RemoveDocumentFromMyMusicianProfile.Command command = _mapper.Map<RemoveDocumentFromMyMusicianProfile.Command>(removeDocumentDto);
            _ = await _mediator.Send(command);
        }

        public async Task<RegionPreferenceDto> CreateRegionPreferenceAsync(MyRegionPreferenceCreateDto myRegionPreferenceCreateDto)
        {
            CreateMyRegionPreference.Command command = _mapper.Map<CreateMyRegionPreference.Command>(myRegionPreferenceCreateDto);
            RegionPreference createdEntity = await _mediator.Send(command);
            return _mapper.Map<RegionPreferenceDto>(createdEntity);
        }

        public async Task ModifyRegionPreferenceAsync(MyRegionPreferenceModifyDto myRegionPreferenceModifyDto)
        {
            ModifyMyRegionPreference.Command command = _mapper.Map<ModifyMyRegionPreference.Command>(myRegionPreferenceModifyDto);
            _ = await _mediator.Send(command);
        }

        public async Task RemoveRegionPreferenceAsync(MyRegionPreferenceRemoveDto myRegionPreferenceRemoveDto)
        {
            RemoveMyRegionPreference.Command command = _mapper.Map<RemoveMyRegionPreference.Command>(myRegionPreferenceRemoveDto);
            _ = await _mediator.Send(command);
        }

        public async Task<IList<MyAppointmentDto>> GetAppointmentRangeAsync(DateTime? dateTime, DateRange dateRange)
        {
            dateTime ??= DateTime.Today;
            Guid personId = _userAccessor.PersonId;

            var query = new ListMyAppointmentRange.Query(dateTime, dateRange, personId);

            IList<MyAppointmentDto> myAppointmentDtos = _mapper.Map<IList<MyAppointmentDto>>(await _mediator.Send(query));

            foreach (MyAppointmentDto dto in myAppointmentDtos)
            {
                AppointmentParticipation participation = await _mediator.Send(new GetAppointmentParticipation.Query(
                    dto.Id, personId));
                if (participation != null)
                {
                    dto.Result = participation.Result;
                    dto.Prediction = participation.Prediction;
                    dto.CommentByPerformerInner = participation.CommentByPerformerInner;
                }
            }

            return myAppointmentDtos;
        }

    }
}

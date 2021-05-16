using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.MeApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
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

        public async Task<MyProfileDto> GetMyProfileAsync()
        {
            User user = await _mediator.Send(new UserProfile.Query());
            return _mapper.Map<MyProfileDto>(user);
        }

        public async Task ModifyMyProfileAsync(MyProfileModifyDto userProfileModifyDto)
        {
            Modify.Command command = _mapper.Map<Modify.Command>(userProfileModifyDto);
            await _mediator.Send(command);
        }

        public async Task<MyAppointmentListDto> GetMyAppointmentsAsync(int? limit, int? offset)
        {
            var treeQuery = new Domain.Logic.Sections.FlattenedTree.Query();
            IEnumerable<ITree<Section>> flattenedTree = await _mediator.Send(treeQuery);
            User currentUser = await _userAccessor.GetCurrentUserAsync();

            Tuple<IQueryable<Appointment>, int> appointmentTuple = await _mediator.Send(
                new AppointmentList.Query(limit, offset, flattenedTree, currentUser.Person));

            IList<MyAppointmentDto> myAppointmentDtos = await appointmentTuple.Item1
                .ProjectTo<MyAppointmentDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            foreach (MyAppointmentDto dto in myAppointmentDtos)
            {
                AppointmentParticipation participation = await _mediator.Send(new Domain.Logic.AppointmentParticipations.Details.Query(
                    dto.Id, currentUser.PersonId));
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

            User currentUser = await _userAccessor.GetCurrentUserAsync();
            command.PersonId = currentUser.PersonId;

            await _mediator.Send(command);
        }

        public async Task<SendQRCode.QrCodeFile> SendMyQrCodeAsync()
        {
            var command = new SendQRCode.Command { Username = _userAccessor.UserName };
            return await _mediator.Send(command);
        }
    }
}

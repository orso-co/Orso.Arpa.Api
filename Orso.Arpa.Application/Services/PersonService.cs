using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.AppointmentApplication;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.PersonApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.Persons;

namespace Orso.Arpa.Application.Services
{
    public class PersonService : BaseService<
        PersonDto,
        Person,
        PersonCreateDto,
        Create.Command,
        PersonModifyDto,
        PersonModifyBodyDto,
        Modify.Command>, IPersonService
    {
        public PersonService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        public async Task AddStakeholderGroupAsync(PersonAddStakeholderGroupDto addStakeholderGroupDto)
        {
            AddStakeholderGroup.Command command = _mapper.Map<AddStakeholderGroup.Command>(addStakeholderGroupDto);
            await _mediator.Send(command);
        }

        public Task<IEnumerable<PersonDto>> GetAsync()
        {
            return base.GetAsync(orderBy: p => p.OrderBy(person => person.Surname).ThenBy(person => person.GivenName));
        }

        public async Task<PersonInviteResultDto> InviteAsync(PersonInviteDto dto)
        {
            Invite.Command command = _mapper.Map<Invite.Command>(dto);
            PersonInviteResult result = await _mediator.Send(command);
            return _mapper.Map<PersonInviteResultDto>(result);
        }

        public async Task RemoveStakeholderGroupAsync(PersonRemoveStakeholderGroupDto removeStakeholderGroupDto)
        {
            RemoveStakeholderGroup.Command command = _mapper.Map<RemoveStakeholderGroup.Command>(removeStakeholderGroupDto);
            await _mediator.Send(command);
        }

        public override async Task DeleteAsync(Guid id)
        {
            var notification = new DeleteNotification() { Id = id };
            await _mediator.Publish(notification);
            await base.DeleteAsync(id);
        }
    }
}

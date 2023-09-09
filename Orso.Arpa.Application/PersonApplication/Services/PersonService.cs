using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.General.Services;
using Orso.Arpa.Application.PersonApplication.Interfaces;
using Orso.Arpa.Application.PersonApplication.Model;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.PersonDomain.Commands;
using Orso.Arpa.Domain.PersonDomain.Model;
using Orso.Arpa.Domain.PersonDomain.Queries;

namespace Orso.Arpa.Application.PersonApplication.Services
{
    public class PersonService : BaseService<
        PersonDto,
        Person,
        PersonCreateDto,
        CreatePerson.Command,
        PersonModifyDto,
        PersonModifyBodyDto,
        ModifyPerson.Command>, IPersonService
    {
        public PersonService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        public async Task AddStakeholderGroupAsync(PersonAddStakeholderGroupDto addStakeholderGroupDto)
        {
            AddStakeholderGroup.Command command = _mapper.Map<AddStakeholderGroup.Command>(addStakeholderGroupDto);
            _ = await _mediator.Send(command);
        }

        public Task<IEnumerable<PersonDto>> GetAsync()
        {
            return base.GetAsync(orderBy: p => p.OrderBy(person => person.Surname).ThenBy(person => person.GivenName));
        }

        public async Task<PersonInviteResultDto> InviteAsync(PersonInviteDto dto)
        {
            InvitePersonToApp.Command command = _mapper.Map<InvitePersonToApp.Command>(dto);
            PersonInviteResult result = await _mediator.Send(command);
            return _mapper.Map<PersonInviteResultDto>(result);
        }

        public async Task RemoveStakeholderGroupAsync(PersonRemoveStakeholderGroupDto removeStakeholderGroupDto)
        {
            RemoveStakeholderGroup.Command command = _mapper.Map<RemoveStakeholderGroup.Command>(removeStakeholderGroupDto);
            _ = await _mediator.Send(command);
        }

        public async Task<IFileResult> SetProfilePictureAsync(ProfilePictureCreateDto profilePictureCreateDto)
        {
            var oldFileName = await _mediator.Send(new GetProfilePictureFileName.Query(profilePictureCreateDto.Id));
            IFileResult fileResult = await _mediator.Send(_mapper.Map<UploadProfilePicture.Command>(profilePictureCreateDto));

            await _mediator.Publish(new ProfilePictureUploadedNotification
            {
                PersonId = profilePictureCreateDto.Id,
                OldFileName = oldFileName,
                NewFileName = fileResult.Name
            });
            return fileResult;
        }

        public async Task<IFileResult> GetProfilePictureAsync(Guid personId)
        {
            return await _mediator.Send(new LoadProfilePicture.Query(personId));
        }

        public async Task DeleteProfilePictureAsync(Guid personId)
        {
            _ = await _mediator.Send(new DeleteProfilePicture.Command(personId));
        }
    }
}

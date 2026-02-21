using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
        private readonly IArpaContext _arpaContext;

        public PersonService(IMediator mediator, IMapper mapper, IArpaContext arpaContext) : base(mediator, mapper)
        {
            _arpaContext = arpaContext;
        }

        public async Task AddStakeholderGroupAsync(PersonAddStakeholderGroupDto addStakeholderGroupDto)
        {
            AddStakeholderGroupToPerson.Command command = _mapper.Map<AddStakeholderGroupToPerson.Command>(addStakeholderGroupDto);
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
            RemoveStakeholderGroupFromPerson.Command command = _mapper.Map<RemoveStakeholderGroupFromPerson.Command>(removeStakeholderGroupDto);
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
    
        public async Task<IList<ReducedPersonDto>> GetBirthdayChildrenAsync(DateTime date) {
            var persons = await _mediator.Send(new ListBirthdayChildren.Query { Date = date });
            return _mapper.Map<IList<ReducedPersonDto>>(persons);
        }

        public async Task<IEnumerable<PersonSearchResultDto>> SearchAsync(string query, int take, bool? hasAccount)
        {
            var normalizedQuery = (query ?? "").Trim().ToLower();

            IQueryable<Person> queryable = _arpaContext.Persons
                .AsNoTracking()
                .Include(p => p.User);

            if (!string.IsNullOrEmpty(normalizedQuery))
            {
                queryable = queryable.Where(p =>
                    p.GivenName.ToLower().Contains(normalizedQuery) ||
                    p.Surname.ToLower().Contains(normalizedQuery) ||
                    p.DisplayName.ToLower().Contains(normalizedQuery));
            }

            if (hasAccount == true)
            {
                queryable = queryable.Where(p => p.User != null);
            }
            else if (hasAccount == false)
            {
                queryable = queryable.Where(p => p.User == null);
            }

            var results = await queryable
                .OrderBy(p => p.Surname)
                .ThenBy(p => p.GivenName)
                .Take(take)
                .Select(p => new PersonSearchResultDto
                {
                    Id = p.Id,
                    GivenName = p.GivenName,
                    Surname = p.Surname,
                    DisplayName = p.DisplayName,
                    HasUser = p.User != null,
                    UserId = p.User != null ? p.User.Id : (Guid?)null
                })
                .ToListAsync();

            return results;
        }
    }
}

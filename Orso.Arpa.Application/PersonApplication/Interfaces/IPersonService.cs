using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.PersonApplication.Model;
using Orso.Arpa.Domain.General.Interfaces;

namespace Orso.Arpa.Application.PersonApplication.Interfaces
{
    public interface IPersonService
    {
        Task<PersonDto> CreateAsync(PersonCreateDto createDto);

        Task<PersonDto> GetByIdAsync(Guid id);

        Task<IEnumerable<PersonDto>> GetAsync();

        Task ModifyAsync(PersonModifyDto modifyDto);
        Task DeleteAsync(Guid id);
        Task AddStakeholderGroupAsync(PersonAddStakeholderGroupDto addStakeholderGroupDto);
        Task RemoveStakeholderGroupAsync(PersonRemoveStakeholderGroupDto removeStakeholderGroupDto);
        Task<PersonInviteResultDto> InviteAsync(PersonInviteDto dto);
        Task<IFileResult> SetProfilePictureAsync(ProfilePictureCreateDto profilePictureCreateDto);
        Task<IFileResult> GetProfilePictureAsync(Guid personId);
        Task DeleteProfilePictureAsync(Guid personId);
    }
}

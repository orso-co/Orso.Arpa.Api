using System;
using System.Threading.Tasks;
using Orso.Arpa.Application.ContactDetailApplication;

namespace Orso.Arpa.Application.Interfaces
{
    public interface IContactDetailService
    {
        Task<ContactDetailDto> CreateAsync(ContactDetailCreateDto contactDetailCreateDto);
        Task ModifyAsync(ContactDetailModifyDto contactDetailModifyDto);
        Task DeleteAsync(Guid id);
    }
}

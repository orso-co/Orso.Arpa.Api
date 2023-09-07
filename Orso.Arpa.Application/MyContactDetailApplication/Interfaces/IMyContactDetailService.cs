using System.Threading.Tasks;
using Orso.Arpa.Application.ContactDetailApplication;
using Orso.Arpa.Application.MyContactDetailApplication;

namespace Orso.Arpa.Application.Interfaces
{
    public interface IMyContactDetailService
    {
        Task<ContactDetailDto> CreateAsync(MyContactDetailCreateDto contactDetailCreateDto);
        Task ModifyAsync(MyContactDetailModifyDto contactDetailModifyDto);
        Task DeleteAsync(MyContactDetailDeleteDto deleteDto);
    }
}

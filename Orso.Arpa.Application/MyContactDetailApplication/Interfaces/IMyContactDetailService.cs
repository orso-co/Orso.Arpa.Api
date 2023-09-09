using System.Threading.Tasks;
using Orso.Arpa.Application.ContactDetailApplication.Model;
using Orso.Arpa.Application.MyContactDetailApplication.Model;

namespace Orso.Arpa.Application.MyContactDetailApplication.Interfaces
{
    public interface IMyContactDetailService
    {
        Task<ContactDetailDto> CreateAsync(MyContactDetailCreateDto contactDetailCreateDto);
        Task ModifyAsync(MyContactDetailModifyDto contactDetailModifyDto);
        Task DeleteAsync(MyContactDetailDeleteDto deleteDto);
    }
}

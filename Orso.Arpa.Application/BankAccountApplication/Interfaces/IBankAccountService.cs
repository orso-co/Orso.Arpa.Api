using System;
using System.Threading.Tasks;
using Orso.Arpa.Application.BankAccountApplication;

namespace Orso.Arpa.Application.Interfaces
{
    public interface IBankAccountService
    {
        Task<BankAccountDto> CreateAsync(BankAccountCreateDto bankAccountCreateDto);
        Task ModifyAsync(BankAccountModifyDto bankAccountModifyDto);
        Task DeleteAsync(Guid id);
    }
}

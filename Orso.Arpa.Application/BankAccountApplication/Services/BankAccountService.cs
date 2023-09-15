using AutoMapper;
using MediatR;
using Orso.Arpa.Application.BankAccountApplication.Interfaces;
using Orso.Arpa.Application.BankAccountApplication.Model;
using Orso.Arpa.Application.General.Services;
using Orso.Arpa.Domain.PersonDomain.Commands;
using Orso.Arpa.Domain.PersonDomain.Model;

namespace Orso.Arpa.Application.BankAccountApplication.Services
{
    public class BankAccountService : BaseService<
        BankAccountDto,
        BankAccount,
        BankAccountCreateDto,
        CreateBankAccount.Command,
        BankAccountModifyDto,
        BankAccountModifyBodyDto,
        ModifyBankAccount.Command>, IBankAccountService
    {
        public BankAccountService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }
    }
}

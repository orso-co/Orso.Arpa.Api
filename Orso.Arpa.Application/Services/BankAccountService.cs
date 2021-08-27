using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.BankAccountApplication;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.PersonApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.BankAccounts;

namespace Orso.Arpa.Application.Services
{
    public class BankAccountService : BaseService<
        BankAccountDto,
        BankAccount,
        BankAccountCreateDto,
        Create.Command,
        BankAccountModifyDto,
        BankAccountModifyBodyDto,
        Modify.Command>, IBankAccountService
    {
        public BankAccountService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }
    }
}

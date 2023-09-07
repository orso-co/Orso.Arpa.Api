using AutoMapper;
using MediatR;
using Orso.Arpa.Application.ContactDetailApplication;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.ContactDetails;

namespace Orso.Arpa.Application.Services
{
    public class ContactDetailService : BaseService<
        ContactDetailDto,
        ContactDetail,
        ContactDetailCreateDto,
        Create.Command,
        ContactDetailModifyDto,
        ContactDetailModifyBodyDto,
        Modify.Command>, IContactDetailService
    {
        public ContactDetailService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }
    }
}

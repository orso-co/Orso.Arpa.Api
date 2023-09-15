using AutoMapper;
using MediatR;
using Orso.Arpa.Application.ContactDetailApplication.Interfaces;
using Orso.Arpa.Application.ContactDetailApplication.Model;
using Orso.Arpa.Application.General.Services;
using Orso.Arpa.Domain.PersonDomain.Commands;
using Orso.Arpa.Domain.PersonDomain.Model;

namespace Orso.Arpa.Application.ContactDetailApplication.Services
{
    public class ContactDetailService : BaseService<
        ContactDetailDto,
        ContactDetail,
        ContactDetailCreateDto,
        CreateContactDetail.Command,
        ContactDetailModifyDto,
        ContactDetailModifyBodyDto,
        ModifyContactDetails.Command>, IContactDetailService
    {
        public ContactDetailService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }
    }
}

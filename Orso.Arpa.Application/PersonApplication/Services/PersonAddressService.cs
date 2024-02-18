using AutoMapper;
using MediatR;
using Orso.Arpa.Application.General.Services;
using Orso.Arpa.Application.PersonApplication.Interfaces;
using Orso.Arpa.Application.PersonApplication.Model;
using Orso.Arpa.Domain.PersonDomain.Commands;
using Orso.Arpa.Domain.PersonDomain.Model;

namespace Orso.Arpa.Application.PersonApplication.Services
{
    public class PersonAddressService : BaseService<
        PersonAddressDto,
        PersonAddress,
        PersonAddressCreateDto,
        CreateAddress.Command,
        PersonAddressModifyDto,
        PersonAddressModifyBodyDto,
        ModifyAddress.Command>, IPersonAddressService
    {
        public PersonAddressService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }
    }
}

using AutoMapper;
using MediatR;
using Orso.Arpa.Application.AddressApplication;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.Addresses;

namespace Orso.Arpa.Application.Services
{
    public class AddressService : BaseService<
        AddressDto,
        Address,
        PersonAddressCreateDto,
        Create.Command,
        PersonAddressModifyDto,
        PersonAddressModifyBodyDto,
        Modify.Command>, IAddressService
    {
        public AddressService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }
    }
}

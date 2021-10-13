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
        AddressCreateDto,
        Create.Command,
        AddressModifyDto,
        AddressModifyBodyDto,
        Modify.Command>, IAddressService
    {
        public AddressService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }
    }
}

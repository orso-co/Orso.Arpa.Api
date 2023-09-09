using AutoMapper;
using MediatR;
using Orso.Arpa.Application.AddressApplication.Interfaces;
using Orso.Arpa.Application.AddressApplication.Model;
using Orso.Arpa.Application.General.Services;
using Orso.Arpa.Domain.AddressDomain.Commands;
using Orso.Arpa.Domain.AddressDomain.Model;

namespace Orso.Arpa.Application.AddressApplication.Services
{
    public class AddressService : BaseService<
        AddressDto,
        Address,
        PersonAddressCreateDto,
        CreateAddress.Command,
        PersonAddressModifyDto,
        PersonAddressModifyBodyDto,
        ModifyAddress.Command>, IAddressService
    {
        public AddressService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }
    }
}

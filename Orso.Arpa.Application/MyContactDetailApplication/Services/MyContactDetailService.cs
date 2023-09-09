using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.ContactDetailApplication.Model;
using Orso.Arpa.Application.MyContactDetailApplication.Interfaces;
using Orso.Arpa.Application.MyContactDetailApplication.Model;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.PersonDomain.Commands;
using Orso.Arpa.Domain.PersonDomain.Model;

namespace Orso.Arpa.Application.MyContactDetailApplication.Services
{
    public class MyContactDetailService : IMyContactDetailService
    {
        private readonly ITokenAccessor _tokenAccessor;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public MyContactDetailService(IMediator mediator, IMapper mapper, ITokenAccessor tokenAccessor)
        {
            _tokenAccessor = tokenAccessor;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<ContactDetailDto> CreateAsync(MyContactDetailCreateDto contactDetailCreateDto)
        {
            CreateMyContactDetails.Command command = _mapper.Map<CreateMyContactDetails.Command>(contactDetailCreateDto);
            command.PersonId = _tokenAccessor.PersonId;
            ContactDetail createdEntity = await _mediator.Send(command);
            return _mapper.Map<ContactDetailDto>(createdEntity);
        }

        public async Task DeleteAsync(MyContactDetailDeleteDto deleteDto)
        {
            DeleteMyContactDetails.Command command = _mapper.Map<DeleteMyContactDetails.Command>(deleteDto);
            command.PersonId = _tokenAccessor.PersonId;
            await _mediator.Send(command);
        }

        public async Task ModifyAsync(MyContactDetailModifyDto contactDetailModifyDto)
        {
            ModifyMyContactDetails.Command command = _mapper.Map<ModifyMyContactDetails.Command>(contactDetailModifyDto);
            command.PersonId = _tokenAccessor.PersonId;
            await _mediator.Send(command);
        }
    }
}

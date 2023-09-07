using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.ContactDetailApplication;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.MyContactDetailApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Logic.MyContactDetails;

namespace Orso.Arpa.Application.Services
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
            Create.Command command = _mapper.Map<Create.Command>(contactDetailCreateDto);
            command.PersonId = _tokenAccessor.PersonId;
            ContactDetail createdEntity = await _mediator.Send(command);
            return _mapper.Map<ContactDetailDto>(createdEntity);
        }

        public async Task DeleteAsync(MyContactDetailDeleteDto deleteDto)
        {
            Delete.Command command = _mapper.Map<Delete.Command>(deleteDto);
            command.PersonId = _tokenAccessor.PersonId;
            await _mediator.Send(command);
        }

        public async Task ModifyAsync(MyContactDetailModifyDto contactDetailModifyDto)
        {
            Modify.Command command = _mapper.Map<Modify.Command>(contactDetailModifyDto);
            command.PersonId = _tokenAccessor.PersonId;
            await _mediator.Send(command);
        }
    }
}

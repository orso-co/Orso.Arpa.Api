using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Registers;

namespace Orso.Arpa.Application.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public RegisterService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RegisterDto>> GetAsync()
        {
            IImmutableList<Register> registers = await _mediator.Send(new List.Query());
            return _mapper.Map<IEnumerable<RegisterDto>>(registers);
        }
    }
}

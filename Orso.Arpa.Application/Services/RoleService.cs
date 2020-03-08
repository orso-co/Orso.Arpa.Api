using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.RoleApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.Roles;

namespace Orso.Arpa.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public RoleService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleDto>> GetAsync()
        {
            IEnumerable<Role> roles = await _mediator.Send(new List.Query());

            var dtos = new List<RoleDto>();
            foreach (Role role in roles)
            {
                dtos.Add(_mapper.Map<RoleDto>(role));
            }

            return dtos;
        }
    }
}

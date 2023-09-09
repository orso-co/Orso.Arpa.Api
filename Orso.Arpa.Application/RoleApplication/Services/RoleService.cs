using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.RoleApplication.Interfaces;
using Orso.Arpa.Application.RoleApplication.Model;
using Orso.Arpa.Domain.UserDomain.Model;
using Orso.Arpa.Domain.UserDomain.Queries;

namespace Orso.Arpa.Application.RoleApplication.Services
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
            IEnumerable<Role> roles = await _mediator.Send(new ListRoles.Query());
            return _mapper.Map<IEnumerable<RoleDto>>(roles);
        }
    }
}

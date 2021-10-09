using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.PersonApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.Persons;

namespace Orso.Arpa.Application.Services
{
    public class PersonService : BaseService<
        PersonDto,
        Person,
        PersonCreateDto,
        Create.Command,
        PersonModifyDto,
        PersonModifyBodyDto,
        Modify.Command>, IPersonService
    {
        public PersonService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        public Task<IEnumerable<PersonDto>> GetAsync()
        {
            return base.GetAsync(orderBy: p => p.OrderBy(person => person.Surname).ThenBy(person => person.GivenName));
        }
    }
}

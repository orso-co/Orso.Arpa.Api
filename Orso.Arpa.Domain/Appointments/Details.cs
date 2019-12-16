using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Appointments
{
    public static class Details
    {
        public class Query : IRequest<Appointment>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Appointment>
        {
            private readonly IReadOnlyRepository _repository;

            public Handler(
                IReadOnlyRepository repository)
            {
                _repository = repository;
            }

            public async Task<Appointment> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _repository.GetByIdAsync<Appointment>(request.Id);
            }
        }
    }
}

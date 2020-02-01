using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.Appointments
{
    public static class List
    {
        public class Query : IRequest<IImmutableList<Appointment>>
        {
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
        }

        public class Handler : IRequestHandler<Query, IImmutableList<Appointment>>
        {
            private readonly IReadOnlyRepository _repository;

            public Handler(
                IReadOnlyRepository repository)
            {
                _repository = repository;
            }

            public Task<IImmutableList<Appointment>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_repository
                    .GetAll<Appointment>()
                    .Where(a => a.StartTime >= request.StartTime && a.StartTime <= request.EndTime)
                    .ToImmutableList() as IImmutableList<Appointment>);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Orso.Arpa.Domain.AppointmentDomain.Enums;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.ProjectDomain.Model;

namespace Orso.Arpa.Domain.ProjectDomain.Queries
{
    public static class ListAppointmentsForProject
    {
        public class Query : IRequest<IOrderedQueryable<Appointment>>
        {
            public Guid ProjectId { get; set; }
        }
        
        public class Validator : AbstractValidator<Query>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.ProjectId)
                    .EntityExists<Query, Project>(arpaContext);
            }
        }

        public class Handler : IRequestHandler<Query, IOrderedQueryable<Appointment>>
        {
            private readonly IArpaContext _arpaContext;

            public Handler(IArpaContext arpaContext)
            {
                _arpaContext = arpaContext;
            }

            public Task<IOrderedQueryable<Appointment>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_arpaContext
                    .Set<ProjectAppointment>()
                    .AsQueryable()
                    .Where(pa => pa.ProjectId == request.ProjectId)
                    .Select(pa => pa.Appointment)
                    .Where(a => a.Status != AppointmentStatus.Refused)
                    .OrderBy(a => a.StartTime));
            }
        }
    }
}
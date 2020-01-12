using System;
using MediatR;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Domain.AppointmentParticipations
{
    public static class Create
    {
        public class Command : IRequest<AppointmentParticipation>
        {
            public Guid AppointmentId { get; set; }
            public Guid PersonId { get; set; }
            public Guid? PredictionId { get; set; }
            public Guid? ResultId { get; set; }
        }
    }
}

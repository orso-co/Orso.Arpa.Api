using System;
using Orso.Arpa.Domain.Entities;
using static Orso.Arpa.Domain.GenericHandlers.Create;

namespace Orso.Arpa.Domain.Logic.AppointmentParticipations
{
    public static class Create
    {
        public class Command : ICreateCommand<AppointmentParticipation>
        {
            public Guid AppointmentId { get; set; }
            public Guid PersonId { get; set; }
            public Guid? PredictionId { get; set; }
            public Guid? ResultId { get; set; }
        }
    }
}

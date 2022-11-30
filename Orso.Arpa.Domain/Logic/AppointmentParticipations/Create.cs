using System;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Enums;
using static Orso.Arpa.Domain.GenericHandlers.Create;

namespace Orso.Arpa.Domain.Logic.AppointmentParticipations
{
    public static class Create
    {
        public class Command : ICreateCommand<AppointmentParticipation>
        {
            public Guid AppointmentId { get; set; }
            public Guid PersonId { get; set; }
            public AppointmentParticipationPrediction? Prediction { get; set; }
            public AppointmentParticipationResult? Result { get; set; }
            public string CommentByPerformerInner { get; set; }
        }
    }
}

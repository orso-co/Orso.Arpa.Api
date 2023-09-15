using System;
using Orso.Arpa.Domain.AppointmentDomain.Enums;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Create;

namespace Orso.Arpa.Domain.AppointmentDomain.Commands
{
    public static class CreateAppointmentParticipation
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

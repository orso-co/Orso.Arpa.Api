using System;
using Orso.Arpa.Domain.Logic.AppointmentParticipations;

namespace Orso.Arpa.Domain.Entities
{
    public class AppointmentParticipation : BaseEntity
    {
        public AppointmentParticipation(Guid? id, Create.Command command) : base(id)
        {
            PersonId = command.PersonId;
            AppointmentId = command.AppointmentId;
            PredictionId = command.PredictionId;
            ResultId = command.ResultId;
        }

        protected AppointmentParticipation()
        {
        }

        public Guid PersonId { get; private set; }
        public virtual Person Person { get; private set; }
        public Guid AppointmentId { get; private set; }
        public virtual Appointment Appointment { get; private set; }
        public Guid? ResultId { get; private set; }
        public virtual SelectValueMapping Result { get; private set; }
        public Guid? PredictionId { get; private set; }
        public virtual SelectValueMapping Prediction { get; private set; }
    }
}

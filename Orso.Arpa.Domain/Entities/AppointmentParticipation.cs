using System;

namespace Orso.Arpa.Domain.Entities
{
    public class AppointmentParticipation : BaseEntity
    {
        internal AppointmentParticipation(Guid? id) : base(id)
        {
        }

        private AppointmentParticipation()
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
        public Guid? ExpectationId { get; private set; }
        public virtual SelectValueMapping Expectation { get; private set; }
    }
}

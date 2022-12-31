using System;

namespace Orso.Arpa.Domain.Entities
{
    public class AppointmentParticipation : BaseEntity
    {
        public AppointmentParticipation(Guid? id, Create.Command command) : base(id)
        {
            PersonId = command.PersonId;
            AppointmentId = command.AppointmentId;
            Prediction = command.Prediction;
            Result = command.Result;
            CommentByPerformerInner = command.CommentByPerformerInner;
        }

        protected AppointmentParticipation()
        {
        }

        public void Update(SetPrediction.Command command)
        {
            Prediction = command.Prediction;
            CommentByPerformerInner = command.CommentByPerformerInner;
        }

        public void Update(SetResult.Command command)
        {
            Result = command.Result;
        }

        public Guid PersonId { get; private set; }
        public virtual Person Person { get; private set; }
        public Guid AppointmentId { get; private set; }
        public virtual Appointment Appointment { get; private set; }
        public AppointmentParticipationResult? Result { get; private set; }
        public AppointmentParticipationPrediction? Prediction { get; private set; }
        public string CommentByPerformerInner { get; private set; }
    }
}

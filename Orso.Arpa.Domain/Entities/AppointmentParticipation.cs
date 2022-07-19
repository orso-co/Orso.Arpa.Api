using System;
using Microsoft.EntityFrameworkCore.Diagnostics;
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
            CommentByPerformerInner = command.CommentByPerformerInner;
        }

        protected AppointmentParticipation()
        {
        }

        public void Update(SetPrediction.Command command)
        {
            PredictionId = command.PredictionId;
            CommentByPerformerInner = command.CommentByPerformerInner;
        }

        public void Update(SetResult.Command command)
        {
            ResultId = command.ResultId;
        }

        public Guid PersonId { get; private set; }
        public virtual Person Person { get; private set; }
        public Guid AppointmentId { get; private set; }
        public virtual Appointment Appointment { get; private set; }
        public Guid? ResultId { get; private set; }
        public virtual SelectValueMapping Result { get; private set; }
        public Guid? PredictionId { get; private set; }
        public virtual SelectValueMapping Prediction { get; private set; }
        public string CommentByPerformerInner { get; private set; }
    }
}

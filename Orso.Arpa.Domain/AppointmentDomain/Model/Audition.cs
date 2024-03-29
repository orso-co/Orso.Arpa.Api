using System;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.SelectValueDomain.Model;

namespace Orso.Arpa.Domain.AppointmentDomain.Model
{
    public class Audition : BaseEntity
    {
        public Guid? AppointmentId { get; private set; }
        public virtual Appointment Appointment { get; private set; }
        public Guid? StatusId { get; private set; }
        public virtual SelectValueMapping Status { get; private set; }
        public Guid? RepetitorStatusId { get; private set; }
        public virtual SelectValueMapping RepetitorStatus { get; private set; }
        public string InnerComment { get; private set; }
        public string InternalComment { get; private set; }
        public string Repertoire { get; private set; }
    }
}

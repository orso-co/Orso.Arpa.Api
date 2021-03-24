using System;
using System.Collections.Generic;

namespace Orso.Arpa.Domain.Entities
{
    public class SelectValueMapping : BaseEntity
    {
        public SelectValueMapping(Guid? id, Guid selectValueCategoryId, Guid selectValueId) : base(id)
        {
            SelectValueCategoryId = selectValueCategoryId;
            SelectValueId = selectValueId;
        }

        protected SelectValueMapping()
        {
        }

        public Guid SelectValueId { get; private set; }
        public virtual SelectValue SelectValue { get; private set; }
        public Guid SelectValueCategoryId { get; private set; }
        public virtual SelectValueCategory SelectValueCategory { get; private set; }

        public virtual ICollection<Appointment> AppointmentsAsCategory { get; private set; }
            = new HashSet<Appointment>();

        public virtual ICollection<Appointment> AppointmentsAsStatus { get; private set; }
            = new HashSet<Appointment>();

        public virtual ICollection<Appointment> AppointmentsAsEmolument { get; private set; }
            = new HashSet<Appointment>();

        public virtual ICollection<Appointment> AppointmentsAsEmolumentPattern { get; private set; }
            = new HashSet<Appointment>();

        public virtual ICollection<Appointment> AppointmentsAsExpectation { get; private set; }
            = new HashSet<Appointment>();

        public virtual ICollection<AppointmentParticipation> AppointmentParticipationsAsResult { get; private set; }
            = new HashSet<AppointmentParticipation>();

        public virtual ICollection<AppointmentParticipation> AppointmentParticipationsAsPrediction { get; private set; }
            = new HashSet<AppointmentParticipation>();

        public virtual ICollection<PersonAddress> PersonAddresses { get; private set; }
            = new HashSet<PersonAddress>();

        public virtual ICollection<Project> ProjectsAsGenre { get; private set; }
            = new HashSet<Project>();

        public virtual ICollection<Project> ProjectsAsState { get; private set; }
            = new HashSet<Project>();
    }
}

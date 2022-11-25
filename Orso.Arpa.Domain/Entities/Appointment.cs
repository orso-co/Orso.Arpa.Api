using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.Attributes;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Domain.Logic.Appointments;

namespace Orso.Arpa.Domain.Entities
{
    public class Appointment : BaseEntity
    {
        public Appointment(Guid? id, Create.Command command) : base(id)
        {
            CategoryId = command.CategoryId;
            StartTime = command.StartTime;
            EndTime = command.EndTime;
            Name = command.Name;
            PublicDetails = command.PublicDetails;
            InternalDetails = command.InternalDetails;
            Status = command.Status;
            SalaryId = command.SalaryId;
            SalaryPatternId = command.SalaryPatternId;
            ExpectationId = command.ExpectationId;
        }

        [JsonConstructor]
        protected Appointment()
        {
        }

        public void Update(Modify.Command command)
        {
            CategoryId = command.CategoryId;
            StartTime = command.StartTime;
            EndTime = command.EndTime;
            Name = command.Name;
            PublicDetails = command.PublicDetails;
            InternalDetails = command.InternalDetails;
            Status = command.Status;
            SalaryId = command.SalaryId;
            SalaryPatternId = command.SalaryPatternId;
            ExpectationId = command.ExpectationId;
        }

        public void Update(SetDates.Command command)
        {
            StartTime = command.StartTime ?? StartTime;
            EndTime = command.EndTime ?? EndTime;
        }

        public void Update(SetVenue.Command command)
        {
            VenueId = command.VenueId;
        }

        internal void ClearVenue()
        {
            VenueId = null;
        }

        #region Native

        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public string Name { get; private set; }
        public string PublicDetails { get; private set; }
        public string InternalDetails { get; private set; }

        #endregion
        #region Reference

        public Guid? CategoryId { get; private set; }
        public virtual SelectValueMapping Category { get; private set; }

        [Obsolete("is only needed for migration purposes")]
        public Guid? StatusId { get; private set; }
        public AppointmentStatus? Status { get; private set; }

        public Guid? SalaryId { get; private set; }
        public virtual SelectValueMapping Salary { get; private set; }

        public Guid? SalaryPatternId { get; private set; }
        public virtual SelectValueMapping SalaryPattern { get; private set; }

        public Guid? VenueId { get; private set; }
        public virtual Venue Venue { get; private set; }

        public Guid? ExpectationId { get; private set; }
        public virtual SelectValueMapping Expectation { get; private set; }

        public Guid? AuditionId { get; private set; }
        public virtual Audition Audition { get; private set; }

        #endregion
        #region Collection

        [CascadingSoftDelete]
        public virtual ICollection<AppointmentRoom> AppointmentRooms { get; private set; } = new HashSet<AppointmentRoom>();

        [CascadingSoftDelete]
        public virtual ICollection<ProjectAppointment> ProjectAppointments { get; private set; } = new HashSet<ProjectAppointment>();

        [CascadingSoftDelete]
        public virtual ICollection<SectionAppointment> SectionAppointments { get; private set; } = new HashSet<SectionAppointment>();

        [CascadingSoftDelete]
        public virtual ICollection<AppointmentParticipation> AppointmentParticipations { get; private set; } = new HashSet<AppointmentParticipation>();

        #endregion
    }
}

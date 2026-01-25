using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.AppointmentDomain.Commands;
using Orso.Arpa.Domain.AppointmentDomain.Enums;
using Orso.Arpa.Domain.General.Attributes;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.ProjectDomain.Model;
using Orso.Arpa.Domain.SectionDomain.Model;
using Orso.Arpa.Domain.SelectValueDomain.Model;
using Orso.Arpa.Domain.VenueDomain.Model;

namespace Orso.Arpa.Domain.AppointmentDomain.Model
{
    public class Appointment : BaseEntity
    {
        public Appointment(Guid? id, CreateAppointment.Command command) : base(id)
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

        public Appointment(Guid? id, CopyAppointment.Command command, Appointment appointmentToCopy) : base(id)
        {
            StartTime = command.StartTime;
            EndTime = command.EndTime;
            Name = appointmentToCopy.Name;
            PublicDetails = appointmentToCopy.PublicDetails;
            InternalDetails = appointmentToCopy.InternalDetails;
            Status = appointmentToCopy.Status;
            SalaryId = appointmentToCopy.SalaryId;
            SalaryPatternId = appointmentToCopy.SalaryPatternId;
            ExpectationId = appointmentToCopy.ExpectationId;
            CategoryId = appointmentToCopy.CategoryId;
            AuditionId = appointmentToCopy.AuditionId;
            VenueId = appointmentToCopy.VenueId;
            AuditionId = appointmentToCopy.AuditionId;
        }

        [JsonConstructor]
        protected Appointment()
        {
        }

        public void Update(ModifyAppointment.Command command)
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
        public AppointmentStatus? Status { get; private set; }

        #endregion
        #region Reference

        public Guid? CategoryId { get; private set; }
        public virtual SelectValueMapping Category { get; private set; }

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

        [CascadingSoftDelete]
        public virtual ICollection<AppointmentSetlistPiece> PrioritizedPieces { get; private set; } = new HashSet<AppointmentSetlistPiece>();

        #endregion

        public override string ToString()
        {
            return CategoryId.HasValue ? $"{Name} ({Category.ToString().ToUpperInvariant()})" : Name;
        }
    }
}

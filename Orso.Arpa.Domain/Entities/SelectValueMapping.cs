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

        public virtual ICollection<Appointment> AppointmentsAsSalary { get; private set; }
            = new HashSet<Appointment>();

        public virtual ICollection<Appointment> AppointmentsAsSalaryPattern { get; private set; }
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
        public virtual ICollection<Project> ProjectsAsType { get; private set; }
            = new HashSet<Project>();

        public virtual ICollection<MusicianProfile> MusicianProfilesAsQualification { get; private set; }
            = new HashSet<MusicianProfile>();

        public virtual ICollection<MusicianProfile> MusicianProfilesAsSalary { get; private set; }
            = new HashSet<MusicianProfile>();

        public virtual ICollection<MusicianProfile> MusicianProfilesAsInquiryStatusPerformer { get; private set; }
            = new HashSet<MusicianProfile>();

        public virtual ICollection<MusicianProfile> MusicianProfilesAsInquiryStatusStaff { get; private set; }
            = new HashSet<MusicianProfile>();

        public virtual ICollection<PreferredGenre> PreferredGenres { get; private set; }
            = new HashSet<PreferredGenre>();

        public virtual ICollection<AvailableDocument> AvailableDocuments { get; private set; }
            = new HashSet<AvailableDocument>();

        public virtual ICollection<Audition> AuditionsAsStatus { get; private set; }
            = new HashSet<Audition>();

        public virtual ICollection<Audition> AuditionsAsRepetitorStatus { get; private set; }
            = new HashSet<Audition>();
    }
}

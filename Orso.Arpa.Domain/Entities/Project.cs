using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.Logic.Projects;

namespace Orso.Arpa.Domain.Entities
{
    public class Project : BaseEntity
    {
        public Project(Guid? id, Create.Command command) : base(id)
        {
            Title = command.Title;
            ShortTitle = command.ShortTitle;
            Description = command.Description;
            Number = command.Number;
            TypeId = command.TypeId;
            GenreId = command.GenreId;
            StartDate = command.StartDate;
            EndDate = command.EndDate;
            Urls = command.Urls;
            StateId = command.StateId;
            ParentId = command.ParentId;
            IsCompleted = command.IsCompleted;
        }

        [JsonConstructor]
        protected Project()
        {
        }

        public string Title { get; private set; }
        public string ShortTitle { get; private set; }
        public string Description { get; private set; }
        public string Number { get; private set; }
        public Guid? TypeId { get; set; }
        public virtual SelectValueMapping Type { get; private set; }
        public Guid? GenreId { get; private set; }
        public virtual SelectValueMapping Genre { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public virtual IList<Url> Urls { get; private set; } = new List<Url>();
        public Guid? StateId { get; private set; }
        public virtual SelectValueMapping State { get; private set; }
        public Guid? ParentId { get; private set; }
        public virtual Project Parent { get; private set; }
        public virtual ICollection<Project> Children { get; private set; } = new HashSet<Project>();
        public bool IsCompleted { get; private set; }

        public virtual ICollection<ProjectAppointment> ProjectAppointments { get; private set; } = new HashSet<ProjectAppointment>();
        public virtual ICollection<ProjectParticipation> ProjectParticipations { get; private set; } = new HashSet<ProjectParticipation>();
    }
}

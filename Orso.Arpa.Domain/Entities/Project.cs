using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.Attributes;
using Orso.Arpa.Domain.Enums;
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
            Code = command.Code;
            TypeId = command.TypeId;
            GenreId = command.GenreId;
            StartDate = command.StartDate;
            EndDate = command.EndDate;
            Status = command.Status;
            ParentId = command.ParentId;
            IsCompleted = command.IsCompleted;
        }

        [JsonConstructor]
        protected Project()
        {
        }

        public void Update(Modify.Command command)
        {
            Title = command.Title;
            ShortTitle = command.ShortTitle;
            Description = command.Description;
            Code = command.Code;
            TypeId = command.TypeId;
            GenreId = command.GenreId;
            StartDate = command.StartDate;
            EndDate = command.EndDate;
            Status = command.Status;
            ParentId = command.ParentId;
            IsCompleted = command.IsCompleted;
        }

        public string Title { get; private set; }
        public string ShortTitle { get; private set; }
        public string Description { get; private set; }
        public string Code { get; private set; }
        public Guid? TypeId { get; set; }
        public virtual SelectValueMapping Type { get; private set; }
        public Guid? GenreId { get; private set; }
        public virtual SelectValueMapping Genre { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }

        [CascadingSoftDelete]
        public virtual ICollection<Url> Urls { get; private set; } = new HashSet<Url>();

        [Obsolete("is only needed for migration purposes")]
        public Guid? StateId { get; private set; }
        public ProjectStatus? Status { get; private set; }
        public Guid? ParentId { get; private set; }
        public virtual Project Parent { get; private set; }
        public bool IsCompleted { get; private set; }

        [CascadingSoftDelete]
        public virtual ICollection<Project> Children { get; private set; } = new HashSet<Project>();

        [CascadingSoftDelete]
        public virtual ICollection<ProjectAppointment> ProjectAppointments { get; private set; } = new HashSet<ProjectAppointment>();

        [CascadingSoftDelete]
        public virtual ICollection<ProjectParticipation> ProjectParticipations { get; private set; } = new HashSet<ProjectParticipation>();

        public override string ToString()
        {
            return $"{Code} - {Title}";
        }
    }
}

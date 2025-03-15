using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.General.Attributes;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.ProjectDomain.Commands;
using Orso.Arpa.Domain.ProjectDomain.Enums;
using Orso.Arpa.Domain.SelectValueDomain.Model;

namespace Orso.Arpa.Domain.ProjectDomain.Model
{
    public class Project : BaseEntity
    {
        public Project(Guid id, CreateProject.Command command) : base(id)
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
            IsHiddenForPerformers = command.IsHiddenForPerformers;
        }

        [JsonConstructor]
        protected Project()
        {
        }

        public void Update(ModifyProject.Command command)
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
            IsHiddenForPerformers = command.IsHiddenForPerformers;
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
        public bool IsHiddenForPerformers { get; private set; }

        [CascadingSoftDelete]
        public virtual ICollection<Url> Urls { get; private set; } = new HashSet<Url>();

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

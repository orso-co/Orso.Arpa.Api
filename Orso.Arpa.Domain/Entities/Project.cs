using System;
using System.Collections.Generic;

namespace Orso.Arpa.Domain.Entities
{
    public class Project : BaseEntity
    {
        public Project(Guid id) : base(id)
        {
        }

        public int Number { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public Guid? ParentId { get; private set; }
        public virtual Project Parent { get; private set; }
        public virtual ICollection<Project> Children { get; private set; } = new HashSet<Project>();
        public Guid? GenreId { get; private set; }
        public virtual SelectValueMapping Genre { get; private set; }
        public virtual ICollection<ProjectAppointment> ProjectAppointments { get; private set; } = new HashSet<ProjectAppointment>();
    }
}

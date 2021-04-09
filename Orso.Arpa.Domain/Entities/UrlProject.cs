using System;

namespace Orso.Arpa.Domain.Entities
{
    public class UrlProject : BaseEntity
    {
        public UrlProject(Guid? id, Project project) : base(id)
        {
            Project = project;
        }

        public UrlProject(Guid? id, Guid projectId) : base(id)
        {
            ProjectId = projectId;
        }

        public UrlProject()
        {
        }

        public Guid ProjectId { get; private set; }
        public virtual Project Project { get; private set; }
    }
}

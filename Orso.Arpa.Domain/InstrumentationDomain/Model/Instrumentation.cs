using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.General.Attributes;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.ProjectDomain.Model;

namespace Orso.Arpa.Domain.InstrumentationDomain.Model
{
    public class Instrumentation : BaseEntity
    {
        public Instrumentation(Guid? id, string name, string description, bool isTemplate, Guid? projectId, Guid? sourceTemplateId) : base(id)
        {
            Name = name;
            Description = description;
            IsTemplate = isTemplate;
            ProjectId = projectId;
            SourceTemplateId = sourceTemplateId;
        }

        [JsonConstructor]
        protected Instrumentation() { }

        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsTemplate { get; set; }

        public Guid? ProjectId { get; set; }
        public virtual Project Project { get; set; }

        /// <summary>
        /// Reference to the template this was copied from (informational only, no FK constraint)
        /// </summary>
        public Guid? SourceTemplateId { get; set; }

        [CascadingSoftDelete]
        public virtual ICollection<InstrumentationPosition> Positions { get; set; } = new HashSet<InstrumentationPosition>();
    }
}

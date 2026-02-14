using System;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.TodoDomain.Enums;

namespace Orso.Arpa.Domain.TodoDomain.Model
{
    public class TodoDependency
    {
        public TodoDependency(Guid dependentTaskId, Guid dependsOnTaskId, DependencyType type = DependencyType.FinishToStart, int lagDays = 0)
        {
            DependentTaskId = dependentTaskId;
            DependsOnTaskId = dependsOnTaskId;
            Type = type;
            LagDays = lagDays;
        }

        [JsonConstructor]
        protected TodoDependency()
        {
        }

        public Guid DependentTaskId { get; private set; }
        public virtual TodoItem DependentTask { get; private set; }

        public Guid DependsOnTaskId { get; private set; }
        public virtual TodoItem DependsOnTask { get; private set; }

        public DependencyType Type { get; private set; }
        public int LagDays { get; private set; }
    }
}

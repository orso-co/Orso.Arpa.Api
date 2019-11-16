using System;

namespace Orso.Arpa.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string CreatedBy { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public string ModifiedBy { get; private set; }
        public DateTimeOffset? ModifiedAt { get; private set; }
        public bool Deleted { get; private set; }

        protected BaseEntity(Guid? id)
        {
            Id = id ?? Id;
        }

        protected BaseEntity()
        {
        }

        public virtual void Create(string createdBy)
        {
            CreatedBy = createdBy;
            CreatedAt = DateTimeOffset.UtcNow;
        }

        public virtual void Modify(string modifiedBy)
        {
            ModifiedBy = modifiedBy;
            ModifiedAt = DateTimeOffset.UtcNow;
        }

        public void Delete(string modifiedBy)
        {
            Deleted = true;
            Modify(modifiedBy);
        }
    }
}

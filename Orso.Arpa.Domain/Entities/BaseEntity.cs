using System;

namespace Orso.Arpa.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public string ModifiedBy { get; private set; }
        public DateTime? ModifiedAt { get; private set; }
        public bool Deleted { get; private set; }

        protected BaseEntity(Guid? id)
        {
            Id = id ?? Id;
        }

        protected BaseEntity()
        {
        }

        public virtual void Create(string createdBy, DateTime? createdAt = null)
        {
            CreatedBy = createdBy;
            CreatedAt = createdAt ?? DateTime.UtcNow;
        }

        public virtual void Modify(string modifiedBy)
        {
            ModifiedBy = modifiedBy;
            ModifiedAt = DateTime.UtcNow;
        }

        public virtual void Delete(string modifiedBy)
        {
            Deleted = true;
            Modify(modifiedBy);
        }
    }
}

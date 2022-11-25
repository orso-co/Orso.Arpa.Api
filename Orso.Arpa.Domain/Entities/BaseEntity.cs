using System;
using System.ComponentModel.DataAnnotations;

namespace Orso.Arpa.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; private set; }

        [MaxLength(110)]
        public string CreatedBy { get; private set; }
        public DateTime CreatedAt { get; private set; }

        [MaxLength(110)]
        public string ModifiedBy { get; private set; }
        public DateTime? ModifiedAt { get; private set; }
        public bool Deleted { get; private set; }

        protected BaseEntity(Guid? id)
        {
            Id = id ?? Guid.NewGuid();
        }

        protected BaseEntity()
        {
        }

        public virtual void Create(string createdBy, DateTime createdAt)
        {
            CreatedBy = createdBy;
            CreatedAt = createdAt;
        }

        public virtual void Modify(string modifiedBy, DateTime modifiedAt)
        {
            ModifiedBy = modifiedBy;
            ModifiedAt = modifiedAt;
        }

        public virtual void Delete(string deletedBy, DateTime deletedAt)
        {
            Deleted = true;
            Modify(deletedBy, deletedAt);
        }
    }
}

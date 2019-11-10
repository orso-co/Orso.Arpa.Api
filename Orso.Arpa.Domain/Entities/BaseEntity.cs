using System;

namespace Orso.Arpa.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedAt { get; set; }
        public bool Deleted { get; set; }

        protected BaseEntity(Guid id, string createdBy)
        {
            Id = id;
            CreatedAt = DateTimeOffset.UtcNow;
            CreatedBy = createdBy;
        }

        public virtual void Modify(string modifiedBy)
        {
            if (!string.IsNullOrEmpty(modifiedBy))
            {
                ModifiedBy = modifiedBy;
            }

            ModifiedAt = DateTimeOffset.UtcNow;
        }

        public void Delete(string modifiedBy)
        {
            Deleted = true;
            Modify(modifiedBy);
        }
    }
}

using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.General.Attributes;
using Orso.Arpa.Domain.General.Model;

namespace Orso.Arpa.Domain.AnnouncementDomain.Model
{
    public class Announcement : BaseEntity
    {
        public Announcement(Guid? id, string title, string content, string priority, string link, string linkText, bool active, DateTime? validUntil, int sortOrder) : base(id)
        {
            Title = title;
            Content = content;
            Priority = priority;
            Link = link;
            LinkText = linkText;
            Active = active;
            ValidUntil = validUntil;
            SortOrder = sortOrder;
        }

        protected Announcement() { }

        public string Title { get; private set; }
        public string Content { get; private set; }
        public string Priority { get; private set; } // "info", "warning", "feature"
        public string Link { get; private set; }
        public string LinkText { get; private set; }
        public bool Active { get; private set; }
        public DateTime? ValidUntil { get; private set; }
        public int SortOrder { get; private set; }

        [CascadingSoftDelete]
        public virtual ICollection<AnnouncementRead> Reads { get; private set; } = new HashSet<AnnouncementRead>();

        public void Update(string title, string content, string priority, string link, string linkText, DateTime? validUntil, int sortOrder)
        {
            Title = title;
            Content = content;
            Priority = priority;
            Link = link;
            LinkText = linkText;
            ValidUntil = validUntil;
            SortOrder = sortOrder;
        }

        public void ToggleActive()
        {
            Active = !Active;
        }

        public void Deactivate()
        {
            Active = false;
        }
    }
}

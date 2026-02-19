using System;
using Orso.Arpa.Domain.General.Model;

namespace Orso.Arpa.Domain.UserDomain.Model
{
    public class UserDashboardLayout : BaseEntity
    {
        public UserDashboardLayout(Guid? id, Guid userId, string dashboardType, string layoutData)
            : base(id)
        {
            UserId = userId;
            DashboardType = dashboardType;
            LayoutData = layoutData;
        }

        protected UserDashboardLayout()
        {
        }

        public Guid UserId { get; private set; }
        public string DashboardType { get; private set; }
        public string LayoutData { get; set; }
        public DateTime? LastVisitedAt { get; set; }

        public virtual User User { get; private set; }
    }
}

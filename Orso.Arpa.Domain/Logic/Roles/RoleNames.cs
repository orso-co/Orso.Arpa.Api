using System.Collections.Generic;

namespace Orso.Arpa.Domain.Roles
{
    public static class RoleNames
    {
        public const string Performer = "Performer";
        public const string Staff = "Staff";
        public const string Admin = "Admin";
        public const string PerformerOrStaff = "Performer,Staff";

        public static IList<string> Roles
        {
            get
            {
                return new List<string>
                {
                    Performer,
                    Staff,
                    Admin
                };
            }
        }
    }
}

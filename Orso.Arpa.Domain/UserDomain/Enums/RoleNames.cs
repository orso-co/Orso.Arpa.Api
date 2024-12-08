using System.Collections.Generic;

namespace Orso.Arpa.Domain.UserDomain.Enums
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
                return
                [
                    Performer,
                    Staff,
                    Admin
                ];
            }
        }
    }
}

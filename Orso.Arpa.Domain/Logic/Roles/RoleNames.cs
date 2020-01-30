using System.Collections.Generic;

namespace Orso.Arpa.Domain.Roles
{
    public static class RoleNames
    {
        public const string Orsianer = "Orsianer";
        public const string Orsonaut = "Orsonaut";
        public const string Orsoadmin = "Orsoadmin";

        public static IList<string> Roles
        {
            get
            {
                return new List<string>
                {
                    Orsianer,
                    Orsonaut,
                    Orsoadmin
                };
            }
        }
    }
}

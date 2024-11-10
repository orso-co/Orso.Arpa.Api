
using System;
using Orso.Arpa.Domain.ClubDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Create;

namespace Orso.Arpa.Domain.ClubDomain.Commands
{
    /// <summary>
    /// Unterart einer Mitgliedschaft, z. B. Vollmitgliedschaft, ermäßigt, passiv
    /// </summary>
    public static class CreateClubMembershipSubType
    {
        public class Command : ICreateCommand<ClubMembershipSubType>
        {
            public Guid MemberhsipTypeId { get; set; }
            public string Name { get; set; }
            public string Advantages { get; set; }
            public string Prerequisites { get; set; }
        }
    }
}

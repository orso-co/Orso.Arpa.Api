using System;
using System.Collections.Generic;

namespace Orso.Arpa.Domain.Interfaces
{
    public interface ITokenAccessor
    {
        string UserName { get; }
        string DisplayName { get; }
        IList<string> GetUserRoles();

        Guid UserId { get; }
        Guid PersonId { get; }
    }
}

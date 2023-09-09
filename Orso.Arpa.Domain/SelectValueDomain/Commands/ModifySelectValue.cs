using System;
using Orso.Arpa.Domain.SelectValueDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Modify;

namespace Orso.Arpa.Domain.SelectValueDomain.Commands
{
    public static class ModifySelectValue
    {
        public class Command : IModifyCommand<SelectValue>
        {
            public Guid Id { get; set; }
        }
    }
}

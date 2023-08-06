using System;
using Orso.Arpa.Domain.Entities;
using static Orso.Arpa.Domain.GenericHandlers.Modify;

namespace Orso.Arpa.Domain.Logic.SelectValues
{
    public static class Modify
    {
        public class Command : IModifyCommand<SelectValue>
        {
            public Guid Id { get; set; }
        }
    }
}

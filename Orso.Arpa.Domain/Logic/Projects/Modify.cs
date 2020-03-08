using System;
using Orso.Arpa.Domain.Entities;
using static Orso.Arpa.Domain.GenericHandlers.Modify;

namespace Orso.Arpa.Domain.Logic.Projects
{
    public static class Modify
    {
        public class Command : IModifyCommand<Project>
        {
            public Guid Id { get; set; }

            // ToDo: Add Properties
        }
    }
}

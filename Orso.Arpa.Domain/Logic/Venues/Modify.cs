using System;
using Orso.Arpa.Domain.Entities;
using static Orso.Arpa.Domain.GenericHandlers.Modify;

namespace Orso.Arpa.Domain.Logic.Venues
{
    public static class Modify
    {
        public class Command : IModifyCommand<Venue>
        {
            public Guid Id { get; set; }
            // ToDo: Add proeprties
        }
    }
}

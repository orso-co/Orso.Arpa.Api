using System;
using Orso.Arpa.Domain.Entities;
using static Orso.Arpa.Domain.GenericHandlers.Create;

namespace Orso.Arpa.Domain.Logic.Projects
{
    public static class Create
    {
        public class Command : ICreateCommand<Project>
        {
            public Guid? GenreId { get; set; }
            public Guid? ParentId { get; set; }
            public string Description { get; set; }
            public string Title { get; set; }
            public int Number { get; set; }
        }
    }
}

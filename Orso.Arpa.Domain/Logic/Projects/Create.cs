using System;
using Orso.Arpa.Domain.Entities;
using static Orso.Arpa.Domain.GenericHandlers.Create;

namespace Orso.Arpa.Domain.Logic.Projects
{
    public static class Create
    {
        public class Command : ICreateCommand<Project>
        {
            public string Title { get; set; }
            public string ShortTitle { get; set; }
            public string Description { get; set; }
            public string Number { get; set; }
            public Guid? TypeId { get; set; }
            public Guid? GenreId { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            // TODO public virtual ICollection<Url> Urls { get; private set; } = new HashSet<Url>();
            public Guid? StateId { get; set; }
            public Guid? ParentId { get; set; }
            public bool IsCompleted { get; set; }
        }
    }
}

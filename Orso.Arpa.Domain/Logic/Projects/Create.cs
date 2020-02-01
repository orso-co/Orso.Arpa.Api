using System;
using MediatR;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Domain.Logic.Projects
{
    public static class Create
    {
        public class Command : IRequest<Project>
        {
            public Guid? GenreId { get; set; }
            public Guid? ParentId { get; set; }
            public string Description { get; set; }
            public string Title { get; set; }
            public int Number { get; set; }
        }
    }
}

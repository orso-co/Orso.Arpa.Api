using MediatR;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Domain.SelectValueCategories
{
    public static class Create
    {
        public class Command : IRequest<SelectValueCategory>
        {
            public string Table { get; set; }
            public string Property { get; set; }
            public string Name { get; set; }
        }
    }
}

using MediatR;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Domain.SelectValues
{
    public static class Create
    {
        public class Command : IRequest<SelectValue>
        {
            public string Name { get; set; }
            public string Description { get; set; }
        }
    }
}

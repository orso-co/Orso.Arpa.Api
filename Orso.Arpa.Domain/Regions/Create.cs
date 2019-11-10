using MediatR;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Domain.Regions
{
    public static class Create
    {
        public class Command : IRequest<Region>
        {
            public string Name { get; set; }
        }
    }
}

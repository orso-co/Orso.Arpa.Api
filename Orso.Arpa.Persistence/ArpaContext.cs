using Microsoft.EntityFrameworkCore;

namespace Orso.Arpa.Persistence
{
    public class ArpaContext : DbContext
    {
        public ArpaContext(DbContextOptions options) : base(options)
        {

        }
    }
}

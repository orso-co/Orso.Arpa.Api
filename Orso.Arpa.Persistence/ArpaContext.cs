using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain;

namespace Orso.Arpa.Persistence
{
    public class ArpaContext : IdentityDbContext<User>
    {
        public ArpaContext(DbContextOptions options) : base(options)
        {

        }
    }
}

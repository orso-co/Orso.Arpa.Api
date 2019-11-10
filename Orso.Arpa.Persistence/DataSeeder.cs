using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Roles.Seed;

namespace Orso.Arpa.Persistence
{
    public class DataSeeder : IDataSeeder
    {
        private readonly RoleManager<Role> _roleManager;

        public DataSeeder(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task SeedDataAsync()
        {
            await SeedRolesAsyync();
        }

        private async Task SeedRolesAsyync()
        {
            if (!_roleManager.Roles.Any())
            {
                foreach (Role role in RoleSeedData.Roles)
                {
                    await _roleManager.CreateAsync(role);
                }
            }
        }
    }
}

using System.Threading.Tasks;

namespace Orso.Arpa.Persistence
{
    public interface IDataSeeder
    {
        Task SeedDataAsync();
    }
}

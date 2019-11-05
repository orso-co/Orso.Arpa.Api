using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orso.Arpa.Persistence;

namespace Orso.Arpa.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWebHost host = CreateWebHostBuilder(args).Build();

            EnsureDatabaseMigration(host);

            host.Run();
        }

        private static void EnsureDatabaseMigration(IWebHost host)
        {
            using (IServiceScope scope = host.Services.CreateScope())
            {
                System.IServiceProvider services = scope.ServiceProvider;
                try
                {
                    ArpaContext context = services.GetRequiredService<ArpaContext>();
                    context.Database.Migrate();
                }
                catch (System.Exception ex)
                {
                    ILogger<Program> logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occured during migration");
                    throw;
                }
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}

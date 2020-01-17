using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Persistence;
using Orso.Arpa.Persistence.DataAccess;

namespace Orso.Arpa.Api.Tests.IntegrationTests.Shared

{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
            : base(configuration, hostingEnvironment)
        {
        }

        protected override void ConfigureDatabase(IServiceCollection services)
        {
            services.AddDbContext<ArpaContext>(options =>
                options.UseInMemoryDatabase("DefaultConnection"));
        }

        protected override void EnsureDatabaseMigrations(IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            System.IServiceProvider services = scope.ServiceProvider;
            try
            {
                ArpaContext context = services.GetRequiredService<ArpaContext>();
                UserManager<User> userManager = services.GetRequiredService<UserManager<User>>();
                IRepository repository = services.GetRequiredService<IRepository>();
                IUnitOfWork unitOfWork = services.GetRequiredService<IUnitOfWork>();
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                IDataSeeder dataSeeder = services.GetRequiredService<IDataSeeder>();
                dataSeeder.SeedDataAsync().Wait();
                TestSeed.SeedDataAsync(userManager, repository, unitOfWork).Wait();
            }
            catch (System.Exception ex)
            {
                ILogger<TestStartup> logger = services.GetRequiredService<ILogger<TestStartup>>();
                logger.LogError(ex, "An error occured during test database migration");
                throw;
            }
        }
    }
}

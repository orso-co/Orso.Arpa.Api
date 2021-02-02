using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Identity;
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
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            DbContextOptions<ArpaContext> options = new DbContextOptionsBuilder<ArpaContext>()
                    .UseSqlite(connection)
                    .Options;

            services.AddDbContext<ArpaContext>(options =>
            {
                options.UseSqlite(connection);
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
            });
        }

        protected override void EnsureDatabaseMigrations(IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            System.IServiceProvider services = scope.ServiceProvider;
            try
            {
                ArpaContext context = services.GetRequiredService<ArpaContext>();
                ArpaUserManager userManager = services.GetRequiredService<ArpaUserManager>();
                SignInManager<User> signInManager = services.GetRequiredService<SignInManager<User>>();
                IArpaContext arpaContext = services.GetRequiredService<IArpaContext>();
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                IDataSeeder dataSeeder = services.GetRequiredService<IDataSeeder>();
                dataSeeder.SeedDataAsync().Wait();
                TestSeed.SeedDataAsync(userManager, signInManager, arpaContext).Wait();
            }
            catch (System.Exception ex)
            {
                ILogger<TestStartup> logger = services.GetRequiredService<ILogger<TestStartup>>();
                logger.LogError(ex, "An error occured during test database migration");
                throw;
            }
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<TestRequestMiddleware>();
            base.Configure(app, env);
        }
    }
}

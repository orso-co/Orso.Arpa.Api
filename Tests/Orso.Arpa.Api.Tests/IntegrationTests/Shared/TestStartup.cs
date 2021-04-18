using System;
using DoomedDatabases.Postgres;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Misc;
using Orso.Arpa.Persistence.DataAccess;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Api.Tests.IntegrationTests.Shared

{
    public class TestStartup : Startup
    {
        public static ITestDatabase TestDatabase { get; set; }
        public static bool IsSeeded { get; set; }

        public TestStartup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
            : base(configuration, hostingEnvironment)
        {
        }

        protected override void ConfigureDatabase(IServiceCollection services)
        {
            if (TestDatabase is null)
            {
                TestDatabase = new TestDatabaseBuilder()
                    .WithConnectionString(Configuration.GetConnectionString("PostgreSQLConnection"))
                    .Build();
                TestDatabase.Create();
            }

            services.AddDbContext<ArpaContext>(options =>
            {
                options.UseNpgsql(TestDatabase.ConnectionString,
                    opt => opt.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
                options.UseSnakeCaseNamingConvention();
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
            });
        }

        protected override void EnsureDatabaseMigrations(IApplicationBuilder app)
        {
            base.EnsureDatabaseMigrations(app);

            using IServiceScope scope = app.ApplicationServices.CreateScope();
            IServiceProvider services = scope.ServiceProvider;
            try
            {
                ArpaUserManager userManager = services.GetRequiredService<ArpaUserManager>();
                SignInManager<User> signInManager = services.GetRequiredService<SignInManager<User>>();
                IArpaContext arpaContext = services.GetRequiredService<IArpaContext>();

                if (!IsSeeded)
                {
                    TestSeed.SeedDataAsync(userManager, signInManager, arpaContext).Wait();
                    IsSeeded = true;
                }
            }
            catch (Exception ex)
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

        protected override void RegisterDateTimeProvider(IServiceCollection services)
        {
            services.AddSingleton<IDateTimeProvider, FakeDateTimeProvider>();
        }
    }
}

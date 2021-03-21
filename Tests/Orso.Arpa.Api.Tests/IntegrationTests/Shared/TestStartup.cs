using System.Threading.Tasks;
using Localization.SqlLocalizer.DbStringLocalizer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
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

        public delegate void CallBack();
        protected override void ConfigureLocalization(IServiceCollection services)
        {
            services.AddSingleton<ArpaContext.CallBack<Translation>>(sp => delegate()
            {
                return new Task(() => { });
            });

            var sqlConnectionString = "DataSource=:memory:";
            var connection = new SqliteConnection(sqlConnectionString);
            connection.Open();

            DbContextOptions<LocalizationModelContext> options = new DbContextOptionsBuilder<LocalizationModelContext>()
                .UseSqlite(connection)
                .Options;

            services.AddDbContext<LocalizationModelContext>(options =>
                {
                    options.UseSqlite(connection);
                    options.EnableSensitiveDataLogging();
                    options.EnableDetailedErrors();
                },
                ServiceLifetime.Singleton,
                ServiceLifetime.Singleton);

            CreateLocalizationDbEntities(services);

            services.AddSqlLocalization(options =>
            {
                options.UseTypeFullNames = false;
                options.UseOnlyPropertyNames = false;
                options.ReturnOnlyKeyIfNotFound = true;
                options.CreateNewRecordWhenLocalisedStringDoesNotExist = false;
            });

            services.AddLocalization();

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.SetDefaultCulture("en-US");
                options.AddSupportedUICultures("en-US", "de-DE");
                options.FallBackToParentUICultures = true;
                options.RequestCultureProviders.Add(new CookieRequestCultureProvider {CookieName = "Culture"});
                options.RequestCultureProviders.Remove(
                    new AcceptLanguageHeaderRequestCultureProvider());  // avoids browser from overwriting UI language request
            });

        }

        private void CreateLocalizationDbEntities(IServiceCollection services)
        {
            LocalizationRecord[] records = {
                new() {Id = 1, Key = "This request requires a valid JWT access token to be provided", LocalizationCulture = "en-US", ResourceKey = nameof(ApiResource), Text = "Please try to login again"},
                new() {Id = 2, Key = "This request requires a valid JWT access token to be provided", LocalizationCulture = "de-DE", ResourceKey = nameof(ApiResource), Text = "Bitte melde dich erneut an"}
            };
            LocalizationModelContext context = services.BuildServiceProvider().GetRequiredService<LocalizationModelContext>();
            context.Database.EnsureCreated();
            context.LocalizationRecords.AddRange( records);
            context.SaveChanges();
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

using System;
using System.Threading.Tasks;
using DoomedDatabases.Postgres;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Orso.Arpa.Application.Localization;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Misc;
using Orso.Arpa.Persistence;
using Orso.Arpa.Persistence.DataAccess;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Api.Tests.IntegrationTests.Shared

{
    public class TestStartup : Startup
    {
        public static ITestDatabase TestDatabase { get; set; }

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
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            IServiceProvider services = scope.ServiceProvider;
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

        protected override void ConfigureLocalization(IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof (services));
            var lz = new LocalizerCache(services);
            services.AddSingleton(_ => lz);
            services.AddSingleton<ArpaContext.CallBack<Translation>>(_ => lz.CallBack);
            services.AddSingleton<IStringLocalizerFactory, ArpaLocalizerFactory>();

            services.AddLocalization();

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.SetDefaultCulture("en-US");
                options.AddSupportedUICultures("en-US", "de-DE");
                options.AddSupportedCultures("en-US", "de-DE");
                options.FallBackToParentCultures = true;
                options.RequestCultureProviders.Add(new QueryStringRequestCultureProvider());
                options.RequestCultureProviders.Remove(
                    new AcceptLanguageHeaderRequestCultureProvider());  // avoids browser from overwriting UI language request
            });

        }
    }
}

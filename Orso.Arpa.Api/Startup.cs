using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using AspNetCoreRateLimit;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using HotChocolate.Execution.Configuration;
using HotChocolate.Types;
using MediatR;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Orso.Arpa.Api.Extensions;
using Orso.Arpa.Api.GraphQL;
using Orso.Arpa.Api.Middleware;
using Orso.Arpa.Api.ModelBinding;
using Orso.Arpa.Api.Swagger;
using Orso.Arpa.Application.AddressApplication.Interfaces;
using Orso.Arpa.Application.AddressApplication.Services;
using Orso.Arpa.Application.AppointmentApplication.Interfaces;
using Orso.Arpa.Application.AppointmentApplication.Services;
using Orso.Arpa.Application.AuditLogApplication.Interfaces;
using Orso.Arpa.Application.AuditLogApplication.Services;
using Orso.Arpa.Application.AuthApplication.AuthorizationHandler;
using Orso.Arpa.Application.AuthApplication.Interfaces;
using Orso.Arpa.Application.AuthApplication.Model;
using Orso.Arpa.Application.AuthApplication.Services;
using Orso.Arpa.Application.BankAccountApplication.Interfaces;
using Orso.Arpa.Application.BankAccountApplication.Services;
using Orso.Arpa.Application.ClubApplication.Interfaces;
using Orso.Arpa.Application.ClubApplication.Services;
using Orso.Arpa.Application.ContactDetailApplication.Interfaces;
using Orso.Arpa.Application.ContactDetailApplication.Services;
using Orso.Arpa.Application.CurriculumVitaeReferenceApplication.Interfaces;
using Orso.Arpa.Application.CurriculumVitaeReferenceApplication.Services;
using Orso.Arpa.Application.DoublingInstrumentApplication.Interfaces;
using Orso.Arpa.Application.DoublingInstrumentApplication.Services;
using Orso.Arpa.Application.EducationApplication.Interfaces;
using Orso.Arpa.Application.EducationApplication.Services;
using Orso.Arpa.Application.MeApplication.Interfaces;
using Orso.Arpa.Application.MeApplication.Services;
using Orso.Arpa.Application.MusicianProfileApplication.Interfaces;
using Orso.Arpa.Application.MusicianProfileApplication.Services;
using Orso.Arpa.Application.MusicianProfileDeactivationApplication.Interfaces;
using Orso.Arpa.Application.MusicianProfileDeactivationApplication.Services;
using Orso.Arpa.Application.MyContactDetailApplication.Interfaces;
using Orso.Arpa.Application.MyContactDetailApplication.Services;
using Orso.Arpa.Application.MyProjectApplication.Interfaces;
using Orso.Arpa.Application.MyProjectApplication.Services;
using Orso.Arpa.Application.NewsApplication.Interfaces;
using Orso.Arpa.Application.NewsApplication.Services;
using Orso.Arpa.Application.PersonApplication.Interfaces;
using Orso.Arpa.Application.PersonApplication.Services;
using Orso.Arpa.Application.ProjectApplication.Interfaces;
using Orso.Arpa.Application.ProjectApplication.Services;
using Orso.Arpa.Application.RegionApplication.Interfaces;
using Orso.Arpa.Application.RegionApplication.Services;
using Orso.Arpa.Application.RoleApplication.Interfaces;
using Orso.Arpa.Application.RoleApplication.Services;
using Orso.Arpa.Application.RoomApplication.Interfaces;
using Orso.Arpa.Application.RoomApplication.Services;
using Orso.Arpa.Application.SectionApplication.Interfaces;
using Orso.Arpa.Application.SectionApplication.Services;
using Orso.Arpa.Application.SelectValueApplication.Interfaces;
using Orso.Arpa.Application.SelectValueApplication.Services;
using Orso.Arpa.Application.TranslationApplication.Interfaces;
using Orso.Arpa.Application.TranslationApplication.Services;
using Orso.Arpa.Application.UrlApplication.Interfaces;
using Orso.Arpa.Application.UrlApplication.Services;
using Orso.Arpa.Application.UserApplication.Interfaces;
using Orso.Arpa.Application.UserApplication.Services;
using Orso.Arpa.Application.VenueApplication.Interfaces;
using Orso.Arpa.Application.VenueApplication.Services;
using Orso.Arpa.Domain._General.Errors;
using Orso.Arpa.Domain.AuditLogDomain.Model;
using Orso.Arpa.Domain.General.Configuration;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.LocalizationDomain.Model;
using Orso.Arpa.Domain.ProjectDomain.Commands;
using Orso.Arpa.Domain.UserDomain.Commands;
using Orso.Arpa.Domain.UserDomain.Enums;
using Orso.Arpa.Domain.UserDomain.Model;
using Orso.Arpa.Domain.UserDomain.Repositories;
using Orso.Arpa.Infrastructure.Authentication;
using Orso.Arpa.Infrastructure.Authorization;
using Orso.Arpa.Infrastructure.Authorization.AuthorizationRequirements;
using Orso.Arpa.Infrastructure.FileManagement;
using Orso.Arpa.Infrastructure.Localization;
using Orso.Arpa.Infrastructure.PipelineBehaviors;
using Orso.Arpa.Mail;
using Orso.Arpa.Mail.Interfaces;
using Orso.Arpa.Misc;
using Orso.Arpa.Persistence;
using Orso.Arpa.Persistence.DataAccess;
using Orso.Arpa.Persistence.GraphQL;
using SixLabors.ImageSharp.Web.Caching;
using SixLabors.ImageSharp.Web.Caching.Azure;
using SixLabors.ImageSharp.Web.Commands;
using SixLabors.ImageSharp.Web.DependencyInjection;
using SixLabors.ImageSharp.Web.Providers;
using SixLabors.ImageSharp.Web.Providers.Azure;
using Yoh.Text.Json.NamingPolicies;
using User = Orso.Arpa.Domain.UserDomain.Model.User;

namespace Orso.Arpa.Api
{
    public class Startup
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private ILogger<Startup> _logger;

        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
            // Initialize with a null logger initially
            _logger = null;
        }

        public IConfiguration Configuration
        {
            get;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            RegisterServices(services);

            RegisterDateTimeProvider(services);

            ConfigureLocalization(services);

            ConfigureDatabase(services);

            ConfigureCors(services);

            if (_hostingEnvironment.IsProduction())
            {
                _ = services.AddApplicationInsightsTelemetry();
            }
            _ = services.AddMediatR(typeof(LoginUser.Handler).Assembly);
            _ = services.AddGenericMediatorHandlers();
            _ = services.AddAutoMapper(
                typeof(LoginDtoMappingProfile).Assembly,
                typeof(AddRoleToUrl.MappingProfile).Assembly);
            _ = services.AddHealthChecks().AddDbContextCheck<ArpaContext>();

            _ = services.Configure<ApiBehaviorOptions>(options => options.SuppressInferBindingSourcesForParameters = true);
            _ = services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter());
                    options.JsonSerializerOptions.Converters.Add(new TrimmedStringConverter());
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicies.SnakeCaseUpper)); // https://github.com/dotnet/runtime/issues/782 will be included in Text.Json in .NET 8
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                })
                .AddApplicationPart(typeof(Startup).Assembly)
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        ProblemDetailsFactory problemDetailsFactory = context.HttpContext.RequestServices.GetRequiredService<ProblemDetailsFactory>();
                        ValidationProblemDetails problemDetails = problemDetailsFactory.CreateValidationProblemDetails(context.HttpContext, context.ModelState, statusCode: 422);
                        var result = new UnprocessableEntityObjectResult(problemDetails);
                        result.ContentTypes.Add("application/problem+json");
                        result.ContentTypes.Add("application/problem+xml");
                        return result;
                    };
                });

            ConfigureValidation(services);

            ConfigureSwagger(services);

            ConfigureAuthentication(services);

            ConfigureAuthorization(services);

            ConfigureGraphQL(services);

            ConfigureIpRateLimiting(services);

            ConfigureStorage(services);

            // services.AddHostedService<BirthdayWorker>(); only works with alwaysOn=true which is only available in higher pricing tiers of app service
        }

        private void ConfigureStorage(IServiceCollection services)
        {
            // Check if we're running on Raspberry Pi environment
            bool isRaspberryPi = _hostingEnvironment.EnvironmentName.Equals("RaspberryPi", StringComparison.OrdinalIgnoreCase);

            // Get storage configuration
            var storageConfig = Configuration.GetSection("Storage");
            var useLocalStorage = storageConfig.GetValue<bool>("UseLocalStorage") || isRaspberryPi;

            // Initialize ImageSharp builder
            var imageSharpBuilder = services.AddImageSharp();

            // Don't use logger yet, as it might not be initialized
            Console.WriteLine($"Environment: {_hostingEnvironment.EnvironmentName}, Using local storage: {useLocalStorage}");

            if (useLocalStorage)
            {
                // Get the local cache folder path
                string localCacheFolder = storageConfig.GetValue<string>("LocalCacheFolder");

                if (string.IsNullOrEmpty(localCacheFolder))
                {
                    localCacheFolder = Path.Combine(Path.GetTempPath(), "ImageSharpCache");
                    Console.WriteLine($"WARNING: LocalCacheFolder not specified, using temp directory: {localCacheFolder}");
                }

                // Ensure the directory exists
                try
                {
                    if (!Directory.Exists(localCacheFolder))
                    {
                        Console.WriteLine($"INFO: Creating cache directory: {localCacheFolder}");
                        Directory.CreateDirectory(localCacheFolder);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR: Failed to create cache directory {localCacheFolder}. Falling back to temp directory. Exception: {ex.Message}");
                    localCacheFolder = Path.Combine(Path.GetTempPath(), "ImageSharpCache");
                    Directory.CreateDirectory(localCacheFolder);
                }

                // Configure ImageSharp for local file system
                imageSharpBuilder
                    .SetRequestParser<QueryCollectionRequestParser>()
                    .Configure<PhysicalFileSystemCacheOptions>(options =>
                    {
                        options.CacheFolder = localCacheFolder;
                    })
                    .AddProvider<PhysicalFileSystemProvider>();

                // Log which provider is being used
                Console.WriteLine($"INFO: Using local file system storage for ImageSharp: {localCacheFolder}");
            }
            else
            {
                // Only configure Azure if not on Raspberry Pi
                // Get Azure configuration
                var azureConnectionString = Configuration.GetValue<string>("AzureStorageConnectionString");
                var containerName = Configuration.GetValue<string>("AzureStorage:ContainerName") ?? "imagesharp-cache";

                if (string.IsNullOrEmpty(azureConnectionString))
                {
                    Console.WriteLine("ERROR: AzureStorageConnectionString is missing. Cannot configure Azure storage.");
                    throw new InvalidOperationException("Azure Storage connection string is missing from configuration");
                }

                try
                {
                    // Configure for Azure Blob Storage
                    imageSharpBuilder
                        .SetRequestParser<QueryCollectionRequestParser>()
                        .Configure<AzureBlobStorageCacheOptions>(options =>
                        {
                            options.ConnectionString = azureConnectionString;
                            options.ContainerName = containerName;
                        })
                        .AddProvider<AzureBlobStorageImageProvider>();

                    // Try to initialize the container if it doesn't exist
                    var blobServiceClient = new BlobServiceClient(azureConnectionString);
                    var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
                    _ = containerClient.CreateIfNotExists(PublicAccessType.None);

                    // Register needed Azure services
                    services.AddScoped(_ => new BlobServiceClient(azureConnectionString));
                    services.AddScoped<IFileAccessor, AzureStorageProfilePictureAccessor>();

                    // Log which provider is being used
                    Console.WriteLine($"INFO: Using Azure Blob Storage for ImageSharp: {containerName}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR: Failed to initialize Azure Blob Storage. Exception: {ex.Message}");
                    throw; // Re-throw to fail startup - we don't want a silent fallback in production
                }
            }
        }

        private void ConfigureIpRateLimiting(IServiceCollection services)
        {
            _ = services.AddMemoryCache();
            _ = services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));
            _ = services.Configure<IpRateLimitPolicies>(Configuration.GetSection("IpRateLimitPolicies"));
            _ = services.AddInMemoryRateLimiting();
            _ = services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        }

        public static IRequestExecutorBuilder RequestExecutorBuilder { get; private set; }
        protected virtual void ConfigureGraphQL(IServiceCollection services)
        {
            RequestExecutorBuilder = services
                .AddGraphQLServer()
                .RemoveMaxAllowedFieldCycleDepthRule()
                .AddMaxExecutionDepthRule(10)
                .AddMaxAllowedFieldCycleDepthRule(10)
                .ModifyCostOptions(opt => opt.MaxFieldCost = 1500)
                .RegisterDbContextFactory<GraphQLContext>()
                .AddAuthorization()
                .AddFiltering<CustomFilteringConvention>()
                .AddQueryType<Query>()
                .AddFiltering()
                .AddSorting()
                .AddType(new UuidType('D'))
                .ModifyPagingOptions(opt =>
                {
                    opt.MaxPageSize = 100;
                    opt.IncludeTotalCount = true;
                })
                .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true);
        }

        protected virtual void ConfigureLocalization(IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            var lz = new LocalizerCache(services);
            _ = services.AddSingleton<ILocalizerCache>(_ => lz);
            _ = services.AddSingleton<ArpaContext.CallBack<Localization>>(_ => lz.LoadTranslations);
            _ = services.AddSingleton<IStringLocalizerFactory, ArpaLocalizerFactory>();

            _ = services.AddLocalization();

            LocalizationConfiguration localizationConfiguration = Configuration
                .GetSection(nameof(LocalizationConfiguration))
                .Get<LocalizationConfiguration>();

            _ = services.Configure<RequestLocalizationOptions>(options =>
            {
                _ = options.SetDefaultCulture(localizationConfiguration.DefaultCulture);
                _ = options.AddSupportedUICultures(localizationConfiguration.SupportedUiCultures.ToArray());
                _ = options.AddSupportedCultures(localizationConfiguration.SupportedUiCultures.ToArray());
                options.ApplyCurrentCultureToResponseHeaders = true;
                options.FallBackToParentCultures = localizationConfiguration.FallbackToParentCulture;
                options.FallBackToParentUICultures = localizationConfiguration.FallbackToParentCulture;
            });
        }

        private static void ConfigureValidation(IServiceCollection services)
        {
            _ = services
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters()
                .AddValidatorsFromAssemblyContaining<LoginDtoValidator>()
                .AddValidatorsFromAssemblyContaining<LoginUser.Validator>();
            ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
        }

        private static void ConfigureAuthorization(IServiceCollection services)
        {
            _ = services.AddAuthorization(options =>
            {
                options.InvokeHandlersAfterFailure = false;
                options.AddPolicy(AuthorizationPolicies.SetRolePolicy, policy =>
                    policy.Requirements.Add(new SetRoleAuthorizationRequirement()));
                options.AddPolicy(AuthorizationPolicies.IsMyMusicianProfile, policy =>
                    policy.Requirements.Add(new IsMyMusicianProfileRequirement()));
                options.AddPolicy(AuthorizationPolicies.IsMyPerson, policy =>
                    policy.Requirements.Add(new IsMyPersonRequirement()));
                options.AddPolicy(AuthorizationPolicies.HasRolePolicy, policy =>
                   policy.RequireAssertion(context =>
                   {
                       IEnumerable<Claim> roleLevelClaims = context.User.Claims.Where(c => c.Type == ClaimsIdentity.DefaultRoleClaimType);
                       return roleLevelClaims.Any();
                   }));
                options.AddPolicy(AuthorizationPolicies.AtLeastStaffPolicy, policy =>
                   policy.RequireAssertion(context =>
                   {
                       IEnumerable<string> roleLevelClaims = context.User.Claims
                        .Where(c => c.Type == ClaimsIdentity.DefaultRoleClaimType)
                        .Select(c => c.Value);
                       return roleLevelClaims.Any(claim => claim.Equals(RoleNames.Staff) || claim.Equals(RoleNames.Admin));
                   }));
            });
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            ClubConfiguration clubConfig = Configuration.GetSection(nameof(ClubConfiguration)).Get<ClubConfiguration>();

            _ = services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Arpa 2.0 Api",
                    Version = "v1",
                    License = new OpenApiLicense
                    {
                        Name = "EUPL-1.2 License",
                        Url = Configuration.GetSection("SwaggerConfiguration").GetValue<Uri>("LicenseUrl")
                    },
                    Contact = new OpenApiContact
                    {
                        Name = clubConfig.Name,
                        Url = clubConfig.Url,
                        Email = clubConfig.ContactEmail,
                    }
                });

                options.DocumentFilter<LowerCaseDocumentFilter>();

                options.OperationFilter<SwaggerAuthorizeOperationFilter>();

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme (hint: Bearer token part should be appended with 'Bearer')",
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            Array.Empty<string>()
                    }
                });
            });

            _ = services.AddFluentValidationRulesToSwagger();
        }

        private void RegisterServices(IServiceCollection services)
        {
            _ = services.AddScoped<IJwtGenerator, JwtGenerator>();
            _ = services.AddScoped<IUserAccessor, UserAccessor>();
            _ = services.AddScoped<ITokenAccessor, TokenAccessor>();
            _ = services.AddScoped<IDataSeeder, DataSeeder>();
            _ = services.AddScoped<IAuthorizationHandler, SetRoleAuthorizationHandler>();
            _ = services.AddScoped<IAuthorizationHandler, IsMyMusicianProfileAuthorizationHandler>();
            _ = services.AddScoped<IAuthorizationHandler, IsMyPersonAuthorizationHandler>();
            _ = services.AddScoped<IAuthService, AuthService>();
            _ = services.AddScoped<IUserService, UserService>();
            _ = services.AddScoped<IRegionService, RegionService>();
            _ = services.AddScoped<IRoleService, RoleService>();
            _ = services.AddScoped<ISelectValueService, SelectValueService>();
            _ = services.AddScoped<IProjectService, ProjectService>();
            _ = services.AddScoped<IUrlService, UrlService>();
            _ = services.AddScoped<ISectionService, SectionService>();
            _ = services.AddScoped<IAppointmentService, AppointmentService>();
            _ = services.AddScoped<IVenueService, VenueService>();
            _ = services.AddScoped<IAuditLogService, AuditLogService>();
            _ = services.AddScoped<IMusicianProfileService, MusicianProfileService>();
            _ = services.AddScoped<IEducationService, EducationService>();
            _ = services.AddScoped<ICurriculumVitaeReferenceService, CurriculumVitaeReferenceService>();
            _ = services.AddScoped<IPersonService, PersonService>();
            _ = services.AddScoped<IDoublingInstrumentService, DoublingInstrumentService>();
            _ = services.AddScoped<IMeService, MeService>();
            _ = services.AddScoped<ITemplateParser, TemplateParser>();
            _ = services.AddScoped<IEmailSender, EmailSender>();
            _ = services.AddScoped<ITranslationService, TranslationService>();
            _ = services.AddScoped<IMusicianProfileDeactivationService, MusicianProfileDeactivationService>();
            services.AddGenericListHandler(typeof(AuditLog));
            _ = services.AddScoped<IBankAccountService, BankAccountService>();
            _ = services.AddScoped<IContactDetailService, ContactDetailService>();
            _ = services.AddScoped<IMyContactDetailService, MyContactDetailService>();
            _ = services.AddScoped<IAddressService, AddressService>();
            _ = services.AddScoped<IMyProjectService, MyProjectService>();
            _ = services.AddScoped<INewsService, NewsService>();
            _ = services.AddScoped<IClubService, ClubService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IRoomEquipmentService, RoomEquipmentService>();
            services.AddScoped<IRoomSectionService, RoomSectionService>();

            _ = services.AddScoped<IFileNameGenerator, FileNameGenerator>();

            _ = services.AddTransient(typeof(IPipelineBehavior<,>), typeof(DomainValidationBehavior<,>));
            _ = services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));

            _ = services.AddScoped<IArpaContext>(provider => provider.GetService<ArpaContext>());

            _ = AddConfiguration<EmailConfiguration>(services);
            _ = AddConfiguration<ClubConfiguration>(services);
            _ = AddConfiguration<SeedConfiguration>(services);
        }

        private T AddConfiguration<T>(IServiceCollection services) where T : class
        {
            T config = Configuration
                .GetSection(typeof(T).Name)
                .Get<T>();
            _ = services.AddSingleton(config);
            return config;
        }

        protected virtual void RegisterDateTimeProvider(IServiceCollection services)
        {
            _ = services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        }

        private void ConfigureAuthentication(IServiceCollection services)
        {
            IdentityBuilder builder = services.AddIdentityCore<User>();
            var identityBuilder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
            _ = identityBuilder
                .AddEntityFrameworkStores<ArpaContext>()
                .AddSignInManager<SignInManager<User>>()
                .AddEntityFrameworkStores<ArpaContext>()
                .AddDefaultTokenProviders()
                .AddTokenProvider<EmailConfirmationTokenProvider<User>>("emailconfirmation")
                .AddRoleValidator<RoleValidator<Role>>()
                .AddRoleManager<RoleManager<Role>>()
                .AddUserManager<ArpaUserManager>();

            IdentityConfiguration identityConfig = AddConfiguration<IdentityConfiguration>(services);

            _ = services.Configure<IdentityOptions>(opts =>
            {
                opts.Lockout.AllowedForNewUsers = true;
                opts.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(identityConfig.LockoutExpiryInMinutes);
                opts.Lockout.MaxFailedAccessAttempts = identityConfig.MaxFailedLoginAttempts;
                opts.SignIn.RequireConfirmedEmail = true;
                opts.Tokens.EmailConfirmationTokenProvider = "emailconfirmation";
            });

            _ = services.Configure<DataProtectionTokenProviderOptions>(opt =>
                opt.TokenLifespan = TimeSpan.FromHours(identityConfig.DataProtectionTokenExpiryInHours));

            _ = services.Configure<EmailConfirmationTokenProviderOptions>(opt =>
                opt.TokenLifespan = TimeSpan.FromDays(identityConfig.EmailConfirmationTokenExpiryInDays));

            JwtConfiguration jwtConfig = AddConfiguration<JwtConfiguration>(services);

            _ = services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearerConfiguration(jwtConfig);
        }

        private void ConfigureCors(IServiceCollection services)
        {
            var allowedOrigins = Configuration
                .GetSection("CorsConfiguration")
                .GetSection("AllowedOrigins")
                .Get<string[]>();

            _ = services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    _ = policy
                        .AllowAnyHeader()
                        .WithExposedHeaders("x-token-expired")
                        .AllowCredentials()
                        .AllowAnyMethod()
                        .WithOrigins(allowedOrigins);
                });
            });
        }

        protected virtual void ConfigureDatabase(IServiceCollection services)
        {
            _ = services.AddDbContext<ArpaContext>(opt =>
            {
                _ = opt
                    .UseNpgsql(Configuration.GetConnectionString("PostgreSQLConnection"))
                    .UseSnakeCaseNamingConvention()
                    .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning)); // workaround

                if (_hostingEnvironment.IsDevelopment())
                {
                    _ = opt.EnableSensitiveDataLogging();
                    _ = opt.EnableDetailedErrors();
                }
            });
            _ = services.AddPooledDbContextFactory<GraphQLContext>(opt =>
            {
                _ = opt
                    .UseNpgsql(Configuration.GetConnectionString("PostgreSQLConnection"))
                    .UseSnakeCaseNamingConvention()
                    .UseLazyLoadingProxies();

                if (_hostingEnvironment.IsDevelopment())
                {
                    _ = opt.EnableSensitiveDataLogging();
                    _ = opt.EnableDetailedErrors();
                }
            });
        }

        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Initialize the logger from the service provider after DI container is built
            if (_logger == null)
            {
                // Get logger from the app's service provider
                _logger = app.ApplicationServices.GetRequiredService<ILogger<Startup>>();
                _logger.LogInformation("Startup logger initialized in Configure method");
            }
            
            _ = app.UseIpRateLimiting();

            _ = app.UseRequestLocalization();

            _ = app.UseErrorResponseLocalizationMiddleware();

            _ = app.UseMiddleware<ErrorHandlingMiddleware>();

            _ = app.UseMiddleware<EnableRequestBodyRewindMiddleware>();

            _ = app.UseMiddleware<SecurityHeaderMiddleware>();

            ConfigureSecurityHeaders(app, env);

            _ = app.UseRouting();

            _ = app.UseCors("CorsPolicy");

            _ = app.UseHealthChecks("/health");
            _ = app.UseHttpsRedirection();

            _ = app.UseAuthentication();
            _ = app.UseAuthorization();

            _ = app.UseImageSharp();

            _ = app.UseDefaultFiles(); // use index.html
            _ = app.UseStaticFiles();

            AddSwagger(app);

            _ = app.UseEndpoints(endpoints =>
            {
                _ = endpoints.MapControllers();
                _ = endpoints.MapFallbackToController("Index", "Fallback");
                _ = endpoints.MapGraphQL().RequireAuthorization(new AuthorizeAttribute { Roles = RoleNames.Staff });
            });

            EnsureDatabaseMigrations(app);

            PreloadTranslationsFromDb(app);
        }

        private static void ConfigureSecurityHeaders(IApplicationBuilder app, IWebHostEnvironment env)
        {
            _ = app.UseXContentTypeOptions();
            _ = app.UseReferrerPolicy(opt => opt.NoReferrer());
            _ = app.UseXXssProtection(opt => opt.EnabledWithBlockMode());
            _ = app.UseXfo(opt => opt.Deny());
            _ = app.UseCsp(opt => opt
                    .BlockAllMixedContent()
                    .DefaultSources(s => s.Self())
                    .StyleSources(s => s.Self().UnsafeInline().CustomSources("fonts.googleapis.com")) // https://angular.io/guide/security
                    .FormActions(s => s.Self())
                    .FrameSources(s => s.Self().CustomSources("https://www.google.com/recaptcha/", "https://recaptcha.google.com/recaptcha/", "https://*.orso.co", "https://*.orso.berlin", "https://*.podio.com", "https://*.notion.so"))
                    .FrameAncestors(s => s.Self())
                    .ScriptSources(s => s.Self().UnsafeInline().CustomSources("https://www.google.com/recaptcha/", "https://www.gstatic.com/recaptcha/"))
                    .ImageSources(s => s.Self().CustomSources("data:", "blob:"))
                    .ManifestSources(s => s.Self())
                    .MediaSources(s => s.Self())
                    .ObjectSources(s => s.Self())
                    .ChildSources(s => s.Self())
                    .ConnectSources(s => s.Self().CustomSources("fonts.gstatic.com"))
                    .WorkerSources(s => s.Self())
                    .FontSources(s => s.Self().CustomSources("fonts.gstatic.com", "data:"))
            );

            if (env.IsProduction())
            {
                _ = app.UseHsts();
            }
        }

        private static void AddSwagger(IApplicationBuilder app)
        {
            _ = app.UseSwagger();
            _ = app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Orso.Arpa.Api v1");
                c.RoutePrefix = string.Empty;
            });
        }

        protected virtual void EnsureDatabaseMigrations(IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            IServiceProvider services = scope.ServiceProvider;
            try
            {
                Console.WriteLine("INFO: Starting database migration...");
                ArpaContext context = services.GetRequiredService<ArpaContext>();
                context.Database.Migrate();
                Console.WriteLine("INFO: Database migration completed successfully");
                
                Console.WriteLine("INFO: Seeding initial data...");
                IDataSeeder dataSeeder = services.GetRequiredService<IDataSeeder>();
                dataSeeder.SeedDataAsync().Wait();
                Console.WriteLine("INFO: Initial data seeding completed successfully");
            }
            catch (Exception ex)
            {
                // Try to get logger, but fall back to console if it fails
                try 
                {
                    ILogger<Startup> logger = services.GetRequiredService<ILogger<Startup>>();
                    logger.LogError(ex, "An error occured during database migration");
                }
                catch 
                {
                    Console.WriteLine($"ERROR: An error occured during database migration: {ex.Message}");
                    Console.WriteLine(ex.StackTrace);
                }
                
                throw new SystemStartException("An error occured during database migration", ex);
            }
        }

        protected static void PreloadTranslationsFromDb(IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            IServiceProvider services = scope.ServiceProvider;
            try
            {
                Console.WriteLine("INFO: Preloading translations from database...");
                ILocalizerCache localizerCache = services.GetRequiredService<ILocalizerCache>();
                _ = localizerCache.LoadTranslations();
                Console.WriteLine("INFO: Translation preloading completed successfully");
            }
            catch (Exception ex)
            {
                // Try to get logger, but fall back to console if it fails
                try 
                {
                    ILogger<Startup> logger = services.GetRequiredService<ILogger<Startup>>();
                    logger.LogError(ex, "Error during preload of localization data from database");
                }
                catch 
                {
                    Console.WriteLine($"ERROR: Error during preload of localization data from database: {ex.Message}");
                    Console.WriteLine(ex.StackTrace);
                }
                
                throw new SystemStartException("Error during preload of localization data from database", ex);
            }
        }
    }
}

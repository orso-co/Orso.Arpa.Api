using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using AspNetCoreRateLimit;
using FluentValidation;
using FluentValidation.AspNetCore;
using HotChocolate.Execution.Configuration;
using HotChocolate.Types;
using HotChocolate.Types.Pagination;
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
using Orso.Arpa.Application.AuthApplication;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.Services;
using Orso.Arpa.Domain.Configuration;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Logic.Auth;
using Orso.Arpa.Domain.Roles;
using Orso.Arpa.Infrastructure.Authentication;
using Orso.Arpa.Infrastructure.Authorization;
using Orso.Arpa.Infrastructure.Authorization.AuthorizationRequirements;
using Orso.Arpa.Infrastructure.Localization;
using Orso.Arpa.Infrastructure.PipelineBehaviors;
using Orso.Arpa.Mail;
using Orso.Arpa.Mail.Interfaces;
using Orso.Arpa.Misc;
using Orso.Arpa.Persistence;
using Orso.Arpa.Persistence.DataAccess;
using Orso.Arpa.Persistence.GraphQL;
using static Orso.Arpa.Domain.Logic.Regions.Create;

namespace Orso.Arpa.Api
{
    public class Startup
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
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

            _ = services.AddMediatR(typeof(Login.Handler).Assembly);
            _ = services.AddGenericMediatorHandlers();
            _ = services.AddAutoMapper(
                typeof(LoginDtoMappingProfile).Assembly,
                typeof(Domain.Logic.Urls.AddRole.MappingProfile).Assembly);
            _ = services.AddHealthChecks().AddDbContextCheck<ArpaContext>();

            _ = services.Configure<ApiBehaviorOptions>(options => options.SuppressInferBindingSourcesForParameters = true);
            _ = services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter());
                    options.JsonSerializerOptions.Converters.Add(new TrimmedStringConverter());
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
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
                .AddAuthorization()
                .AddFiltering<CustomFilteringConvention>()
                .AddQueryType<Query>()
                .AddFiltering()
                .AddSorting()
                .AddType(new UuidType('D'))
                .SetPagingOptions(new PagingOptions
                {
                    MaxPageSize = 100,
                    IncludeTotalCount = true,
                })
                .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true);
        }

        protected virtual void ConfigureLocalization(IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            var lz = new LocalizerCache(services);
            _ = services.AddSingleton<ILocalizerCache>(_ => lz);
            _ = services.AddSingleton<ArpaContext.CallBack<Localization>>(_ => lz.LoadTranslations);
            _ = services.AddSingleton<IStringLocalizerFactory, ArpaLocalizerFactory>();

            _ = services.AddLocalization();

            LocalizationConfiguration localizationConfiguration = Configuration
                .GetSection("LocalizationConfiguration")
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
                .AddValidatorsFromAssemblyContaining<Validator>();
        }

        private static void ConfigureAuthorization(IServiceCollection services)
        {
            _ = services.AddAuthorization(options =>
            {
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

        private static void ConfigureSwagger(IServiceCollection services)
        {
            _ = services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Arpa 2.0 Api",
                    Version = "v1",
                    License = new OpenApiLicense
                    {
                        Name = "EUPL-1.2 License",
                        Url = new Uri("https://github.com/orso-co/Orso.Arpa.Api/blob/master/LICENSE.txt")
                    },
                    Contact = new OpenApiContact
                    {
                        Name = "ORSO – Orchestra & Choral Society Freiburg | Berlin e. V.",
                        Url = new Uri("https://www.orso.co/"),
                        Email = "mail@orso.co"
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
                    Description = "JWT Authorization header using the Bearer scheme (hint: Bearer token part should be appended with ‘Bearer’)",
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
                    .UseSnakeCaseNamingConvention();

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
            _ = app.UseIpRateLimiting();

            _ = app.UseRequestLocalization();

            _ = app.UseErrorResponseLocalizationMiddleware();

            _ = app.UseMiddleware<NLog.Web.NLogRequestPostedBodyMiddleware>();

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
                    .ImageSources(s => s.Self().CustomSources("data:"))
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
                ArpaContext context = services.GetRequiredService<ArpaContext>();
                context.Database.Migrate();
                IDataSeeder dataSeeder = services.GetRequiredService<IDataSeeder>();
                dataSeeder.SeedDataAsync().Wait();
            }
            catch (Exception ex)
            {
                ILogger<Startup> logger = services.GetRequiredService<ILogger<Startup>>();
                logger.LogError(ex, "An error occured during database migration");
                throw;
            }
        }

        protected void PreloadTranslationsFromDb(IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            IServiceProvider services = scope.ServiceProvider;
            try
            {
                ILocalizerCache localizerCache = services.GetRequiredService<ILocalizerCache>();
                _ = localizerCache.LoadTranslations();
            }
            catch (Exception ex)
            {
                ILogger<Startup> logger = services.GetRequiredService<ILogger<Startup>>();
                logger.LogError(ex, "Error during localization of data");
                throw;
            }
        }
    }
}

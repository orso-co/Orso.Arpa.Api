using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using FluentValidation.AspNetCore;
using MediatR;
using MicroElements.Swashbuckle.FluentValidation;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Orso.Arpa.Api.Extensions;
using Orso.Arpa.Api.Middleware;
using Orso.Arpa.Api.ModelBinding;
using Orso.Arpa.Api.Swagger;
using Orso.Arpa.Application.AuthApplication;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.Localization;
using Orso.Arpa.Application.Services;
using Orso.Arpa.Domain.Configuration;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Logic.Appointments;
using Orso.Arpa.Domain.Logic.Auth;
using Orso.Arpa.Domain.Roles;
using Orso.Arpa.Infrastructure.Authentication;
using Orso.Arpa.Infrastructure.Authorization;
using Orso.Arpa.Infrastructure.Authorization.AuthorizationHandlers;
using Orso.Arpa.Infrastructure.Authorization.AuthorizationRequirements;
using Orso.Arpa.Infrastructure.PipelineBehaviors;
using Orso.Arpa.Mail;
using Orso.Arpa.Mail.Interfaces;
using Orso.Arpa.Misc;
using Orso.Arpa.Persistence;
using Orso.Arpa.Persistence.DataAccess;
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

            services.AddMediatR(typeof(Login.Handler).Assembly);
            services.AddGenericMediatorHandlers();
            services.AddAutoMapper(
                typeof(LoginDtoMappingProfile).Assembly,
                typeof(Modify.MappingProfile).Assembly);
            services.AddHealthChecks().AddDbContextCheck<ArpaContext>();

            services.Configure<ApiBehaviorOptions>(options => options.SuppressInferBindingSourcesForParameters = true);
            services.AddControllers(options =>
                {
                    options.ModelBinderProviders.InsertBodyAndRouteBinding();
                    options.Filters.Add(typeof(LocationResultFilter));
                })
                .AddJsonOptions(options => options.JsonSerializerOptions.Converters
                    .Add(new DateTimeJsonConverter()))
                .AddApplicationPart(typeof(Startup).Assembly)
                .AddFluentValidation(config =>
                {
                    config.RegisterValidatorsFromAssemblyContaining<LoginDtoValidator>();
                    config.RegisterValidatorsFromAssemblyContaining<Validator>();
                    config.ValidatorFactoryType = typeof(HttpContextServiceProviderValidatorFactory); // Workaround https://github.com/micro-elements/MicroElements.Swashbuckle.FluentValidation#error-systeminvalidoperationexception-cannot-resolve-ivalidatort-from-root-provider-because-it-requires-scoped-service-tdependency
                })
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

            ConfigureSwagger(services);

            ConfigureAuthentication(services);

            ConfigureAuthorization(services);

            ConfigureLocalization(services);
        }

        protected virtual void ConfigureLocalization(IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof (services));
            var lz = new LocalizerCache(services);
            services.AddSingleton(_ => lz);
            services.AddSingleton<ArpaContext.CallBack<Translation>>(_ => lz.CallBack);
            services.AddSingleton<IStringLocalizerFactory, ArpaLocalizerFactory>();

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.SetDefaultCulture("en-GB");
                options.AddSupportedUICultures("en", "en-GB", "de", "de-DE");
                options.FallBackToParentCultures = true;
            });

            services.AddLocalization();
        }


        private static void ConfigureAuthorization(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthorizationPolicies.SetRolePolicy, policy =>
                    policy.Requirements.Add(new SetRoleAuthorizationRequirement()));
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
            services.AddSwaggerGen(options =>
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

            services.AddFluentValidationRulesToSwagger();
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddScoped<IUserAccessor, UserAccessor>();
            services.AddScoped<ITokenAccessor, TokenAccessor>();
            services.AddScoped<IDataSeeder, DataSeeder>();
            services.AddScoped<IAuthorizationHandler, SetRoleAuthorizationHandler>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRegionService, RegionService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ISelectValueService, SelectValueService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IUrlService, UrlService>();
            services.AddScoped<ISectionService, SectionService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IVenueService, VenueService>();
            services.AddScoped<IAuditLogService, AuditLogService>();
            services.AddScoped<IMusicianProfileService, MusicianProfileService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IMeService, MeService>();
            services.AddScoped<ITemplateParser, TemplateParser>();
            services.AddScoped<IEmailSender, EmailSender>();

            services.AddGenericListHandler(typeof(AuditLog));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(DomainValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));

            services.AddScoped<IArpaContext>(provider => provider.GetService<ArpaContext>());

            EmailConfiguration emailConfig = Configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);

            ClubConfiguration clubConfig = Configuration
                .GetSection("ClubConfiguration")
                .Get<ClubConfiguration>();
            services.AddSingleton(clubConfig);
        }

        protected virtual void RegisterDateTimeProvider(IServiceCollection services)
        {
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        }

        private void ConfigureAuthentication(IServiceCollection services)
        {


            IdentityBuilder builder = services.AddIdentityCore<User>();
            var identityBuilder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
            identityBuilder
                .AddEntityFrameworkStores<ArpaContext>()
                .AddSignInManager<SignInManager<User>>()
                .AddEntityFrameworkStores<ArpaContext>()
                .AddDefaultTokenProviders()
                .AddTokenProvider<EmailConfirmationTokenProvider<User>>("emailconfirmation")
                .AddRoleValidator<RoleValidator<Role>>()
                .AddRoleManager<RoleManager<Role>>()
                .AddUserManager<ArpaUserManager>();

            IdentityConfiguration identityConfig = Configuration
                .GetSection(nameof(IdentityConfiguration))
                .Get<IdentityConfiguration>();

            services.AddSingleton(identityConfig);

            services.Configure<IdentityOptions>(opts =>
            {
                opts.Lockout.AllowedForNewUsers = true;
                opts.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(identityConfig.LockoutExpiryInMinutes);
                opts.Lockout.MaxFailedAccessAttempts = identityConfig.MaxFailedLoginAttempts;
                opts.SignIn.RequireConfirmedEmail = true;
                opts.Tokens.EmailConfirmationTokenProvider = "emailconfirmation";
            });

            services.Configure<DataProtectionTokenProviderOptions>(opt =>
                opt.TokenLifespan = TimeSpan.FromHours(identityConfig.DataProtectionTokenExpiryInHours));

            services.Configure<EmailConfirmationTokenProviderOptions>(opt =>
                opt.TokenLifespan = TimeSpan.FromDays(identityConfig.EmailConfirmationTokenExpiryInDays));

            JwtConfiguration jwtConfig = Configuration
                .GetSection(nameof(JwtConfiguration))
                .Get<JwtConfiguration>();

            services.AddSingleton(jwtConfig);

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearerConfiguration(jwtConfig);
        }

        private void ConfigureCors(IServiceCollection services)
        {
            var allowedOrigins = Configuration
                .GetSection("CorsConfiguration")
                .GetSection("AllowedOrigins")
                .Get<string[]>();

            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy
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
            services.AddDbContext<ArpaContext>(opt =>
            {
                opt
                    .UseNpgsql(Configuration.GetConnectionString("PostgreSQLConnection"))
                    .UseSnakeCaseNamingConvention();

                if (_hostingEnvironment.IsDevelopment())
                {
                    opt.EnableSensitiveDataLogging();
                    opt.EnableDetailedErrors();
                }
            });
        }

        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRequestLocalization();

            app.UseErrorResponseLocalizationMiddleware();

            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMiddleware<EnableRequestBodyRewindMiddleware>();

            app.UseStaticFiles();

            if (!env.IsDevelopment())
            {
                app.UseHsts();
            }

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseHealthChecks("/health");
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            AddSwagger(app);

            EnsureDatabaseMigrations(app);

            PreloadTranslationsFromDb(app);

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }

        
        private static void AddSwagger(IApplicationBuilder app)
        {
            app.UseScopedSwagger(); // Workaround https://github.com/micro-elements/MicroElements.Swashbuckle.FluentValidation#error-systeminvalidoperationexception-cannot-resolve-ivalidatort-from-root-provider-because-it-requires-scoped-service-tdependency
            app.UseSwaggerUI(c =>
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
                LocalizerCache localizerCache = services.GetRequiredService<LocalizerCache>();
                localizerCache.CallBack();
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

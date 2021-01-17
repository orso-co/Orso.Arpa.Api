using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Orso.Arpa.Api.Extensions;
using Orso.Arpa.Api.Middleware;
using Orso.Arpa.Api.ModelBinding;
using Orso.Arpa.Application.AuthApplication;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.Services;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Logic.Appointments;
using Orso.Arpa.Domain.Logic.Auth;
using Orso.Arpa.Domain.PipelineBehaviors;
using Orso.Arpa.Infrastructure.Authentication;
using Orso.Arpa.Infrastructure.Authorization;
using Orso.Arpa.Infrastructure.Authorization.AuthorizationHandlers;
using Orso.Arpa.Infrastructure.Authorization.AuthorizationRequirements;
using Orso.Arpa.Mail;
using Orso.Arpa.Persistence;
using Orso.Arpa.Persistence.DataAccess;
using Swashbuckle.AspNetCore.Swagger;
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

            ConfigureDatabase(services);

            ConfigureCors(services);

            services.AddMediatR(typeof(Login.Handler).Assembly);
            services.AddGenericMediatorHandlers();
            services.AddAutoMapper(
                typeof(LoginDtoMappingProfile).Assembly,
                typeof(Modify.MappingProfile).Assembly);
            services.AddHealthChecks().AddDbContextCheck<ArpaContext>();

            services.AddControllers(options =>
            {
                options.ModelBinderProviders.InsertBodyAndRouteBinding();
            })
                .AddApplicationPart(typeof(Startup).Assembly)
                .AddFluentValidation(config =>
                {
                    config.RegisterValidatorsFromAssemblyContaining<LoginDtoValidator>();
                    config.RegisterValidatorsFromAssemblyContaining<Validator>();
                });

            ConfigureSwagger(services);

            ConfigureAuthentication(services);

            ConfigureAuthorization(services);
        }

        private static void ConfigureAuthorization(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthorizationPolicies.SetRolePolicy, policy =>
                    policy.Requirements.Add(new SetRoleAuthorizationRequirement()));
                options.AddPolicy(AuthorizationPolicies.AtLeastOrsianerPolicy, policy =>
                   policy.RequireAssertion(context =>
                   {
                       System.Security.Claims.Claim roleLevelClaim = context.User.Claims.FirstOrDefault(c => c.Type == "RoleLevel");
                       if (roleLevelClaim == null)
                       {
                           return false;
                       }
                       if (!short.TryParse(roleLevelClaim.Value, out var level))
                       {
                           return false;
                       }
                       return level > 0;
                   }));
                options.AddPolicy(AuthorizationPolicies.AtLeastOrsonautPolicy, policy =>
                   policy.RequireAssertion(context =>
                   {
                       System.Security.Claims.Claim roleLevelClaim = context.User.Claims.FirstOrDefault(c => c.Type == "RoleLevel");
                       if (roleLevelClaim == null)
                       {
                           return false;
                       }
                       if (!short.TryParse(roleLevelClaim.Value, out var level))
                       {
                           return false;
                       }
                       return level > 1;
                   }));
            });
        }

        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Orso.Arpa.Api",
                    Version = "v1",
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://github.com/orso-co/Orso.Arpa.Api/blob/master/LICENSE")
                    },
                    Contact = new OpenApiContact
                    {
                        Name = "ORSO – Orchestra & Choral Society Freiburg | Berlin e. V.",
                        Url = new System.Uri("https://www.orso.co/"),
                        Email = "mail@orso.co"
                    }
                });

                options.AddFluentValidationRules();

                options.OperationFilter<SwaggerAddFromRoutePropertiesOperationFilter>();
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
                            new string[] {}
                    }
                });
            });
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
            services.AddScoped<ISectionService, SectionService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IVenueService, VenueService>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(DomainValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddScoped<IArpaContext>(provider => provider.GetService<ArpaContext>());
            EmailConfiguration emailConfig = Configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);
            services.AddScoped<IEmailSender, EmailSender>();
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
                .AddRoleValidator<RoleValidator<Role>>()
                .AddRoleManager<RoleManager<Role>>();

            services.Configure<IdentityOptions>(opts =>
            {
                opts.Lockout.AllowedForNewUsers = true;
                opts.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                opts.Lockout.MaxFailedAccessAttempts = 3;
            });

            JwtConfiguration jwtConfig = Configuration
                .GetSection("JwtConfiguration")
                .Get<JwtConfiguration>();
            services.AddSingleton(jwtConfig);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.TokenKey));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,

                        IssuerSigningKey = key,
                        ValidAudience = jwtConfig.Audience,
                        ValidIssuer = jwtConfig.Issuer,
                    };
                });
        }

        private void ConfigureCors(IServiceCollection services)
        {
            var allowedOrigin = Configuration
                .GetSection("CorsConfiguration")["AllowedOrigin"];

            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithOrigins(allowedOrigin);
                });
            });
        }

        protected virtual void ConfigureDatabase(IServiceCollection services)
        {
            services.AddDbContext<ArpaContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                if (_hostingEnvironment.IsDevelopment())
                {
                    opt.EnableSensitiveDataLogging();
                    opt.EnableDetailedErrors();
                }
            });
        }

        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMiddleware<EnableRequestBodyRewindMiddleware>();

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

            app.UseEndpoints(endpoints => endpoints.MapControllers());

            EnsureDatabaseMigrations(app);
        }

        private static void AddSwagger(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Orso.Arpa.Api v1");
                c.RoutePrefix = string.Empty;
            });
        }

        protected virtual void EnsureDatabaseMigrations(IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            System.IServiceProvider services = scope.ServiceProvider;
            try
            {
                ArpaContext context = services.GetRequiredService<ArpaContext>();
                context.Database.Migrate();
                IDataSeeder dataSeeder = services.GetRequiredService<IDataSeeder>();
                dataSeeder.SeedDataAsync().Wait();
            }
            catch (System.Exception ex)
            {
                ILogger<Startup> logger = services.GetRequiredService<ILogger<Startup>>();
                logger.LogError(ex, "An error occured during database migration");
                throw;
            }
        }
    }
}

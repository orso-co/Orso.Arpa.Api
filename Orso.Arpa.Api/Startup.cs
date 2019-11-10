using System.Text;
using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Orso.Arpa.Api.Authorization;
using Orso.Arpa.Api.Authorization.AuthorizationHandlers;
using Orso.Arpa.Api.Authorization.AuthorizationRequirements;
using Orso.Arpa.Api.Middleware;
using Orso.Arpa.Application.Auth;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.Users.Dtos;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Infrastructure.DataAccess;
using Orso.Arpa.Infrastructure.Security;
using Orso.Arpa.Persistence;
using Swashbuckle.AspNetCore.Swagger;
using static Orso.Arpa.Application.Auth.Login;

namespace Orso.Arpa.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
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
            services.AddAutoMapper(typeof(UserProfileDtoMappingProfile).Assembly);

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(config =>
                    config.RegisterValidatorsFromAssemblyContaining<QueryValidator>());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Orso.Arpa.Api", Version = "v1" });
            });

            ConfigureAuthentication(services);

            services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthorizationPolicies.SetRolePolicy, policy =>
                    policy.Requirements.Add(new SetRoleAuthorizationRequirement()));
            });
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddScoped<IUserAccessor, UserAccessor>();
            services.AddScoped<IDataSeeder, DataSeeder>();
            services.AddScoped<IAuthorizationHandler, SetRoleAuthorizationHandler>();
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
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

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenKey"]));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateAudience = false,
                        ValidateIssuer = false
                    };
                });
        }

        private static void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin();
                });
            });
        }

        protected virtual void ConfigureDatabase(IServiceCollection services)
        {
            services.AddDbContext<ArpaContext>(opt =>
            {
                opt.UseLazyLoadingProxies();
                opt.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
            });
        }

        public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMiddleware<EnableRequestBodyRewindMiddleware>();

            if (!env.IsDevelopment())
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();

            AddSwagger(app);

            app.UseCors("CorsPolicy");
            app.UseMvc();

            EnsureDatabaseMigrations(app);
        }

        private static void AddSwagger(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Orso.Arpa.Api v1");
            });
        }

        protected virtual void EnsureDatabaseMigrations(IApplicationBuilder app)
        {
            using (IServiceScope scope = app.ApplicationServices.CreateScope())
            {
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
}

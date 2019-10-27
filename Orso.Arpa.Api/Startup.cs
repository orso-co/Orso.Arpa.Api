using System.Text;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Orso.Arpa.Api.Middleware;
using Orso.Arpa.Application.Auth;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Domain;
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
            ConfigureDatabase(services);

            ConfigureCors(services);

            services.AddMediatR(typeof(Login.Handler).Assembly);

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(config =>
                    config.RegisterValidatorsFromAssemblyContaining<QueryValidator>());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Orso.Arpa.Api", Version = "v1" });
            });

            ConfigureAuthentication(services);

            RegisterServices(services);
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IJwtGenerator, JwtGenerator>();
        }

        private void ConfigureAuthentication(IServiceCollection services)
        {
            IdentityBuilder builder = services.AddIdentityCore<User>();
            var identityBuilder = new IdentityBuilder(builder.UserType, typeof(AppRole), builder.Services);
            identityBuilder
                .AddEntityFrameworkStores<ArpaContext>()
                .AddSignInManager<SignInManager<User>>()
                .AddEntityFrameworkStores<ArpaContext>()
                .AddDefaultTokenProviders()
                .AddRoleValidator<RoleValidator<AppRole>>()
                .AddRoleManager<RoleManager<AppRole>>();

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

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();

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

using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using Orso.Arpa.Api.Middleware;
using Orso.Arpa.Api.ModelBinding;
using Orso.Arpa.Domain._General.Errors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace Orso.Arpa.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Logger logger = LogManager
                .Setup()
                .SetupExtensions(x => _ = x.RegisterLayoutRenderer<AspnetPostedBodyShadowedLayoutRenderer>(SensibleRequestDataShadower.ASPNET_REQUEST_POSTED_BODY_SHADOWED))
                .LoadConfigurationFromAppSettings()
                .GetCurrentClassLogger();

            try
            {
                logger.Debug("init main");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception exception)
            {
                logger.Error(exception, "Stopped program because of exception");
                throw new SystemStartException("Stopped program because of exception", exception);
            }
            finally
            {
                LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder
                    .UseStartup<Startup>()
                    .ConfigureLogging(logging =>
                    {
                        _ = logging.ClearProviders();
                        _ = logging.SetMinimumLevel(LogLevel.Trace);
                    })
                    .ConfigureServices((context, services) =>
                    {
                        // CORS nur in der Entwicklungsumgebung aktivieren
                        if (context.HostingEnvironment.IsDevelopment())
                        {
                            var allowedOrigins = context.Configuration.GetSection("CorsConfiguration:AllowedOrigins").Get<string[]>();

                            services.AddCors(options =>
                            {
                                options.AddPolicy("DevCorsPolicy", policy =>
                                {
                                    policy.WithOrigins(allowedOrigins)
                                          .AllowAnyMethod()
                                          .AllowAnyHeader()
                                          .AllowCredentials();
                                });
                            });
                        }
                    })
                    .UseNLog());
    }
}

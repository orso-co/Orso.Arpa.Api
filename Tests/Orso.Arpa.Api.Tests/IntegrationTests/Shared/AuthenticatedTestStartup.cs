using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Orso.Arpa.Api.Tests.IntegrationTests.Shared

{
    public class AuthenticatedTestStartup : TestStartup
    {
        public AuthenticatedTestStartup(IConfiguration configuration) : base(configuration)
        {
        }
        public override void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware<AuthenticatedTestRequestMiddleware>();
            base.Configure(app, env);
        }
    }
}

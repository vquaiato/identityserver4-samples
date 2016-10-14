using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Server
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDeveloperIdentityServer()
                .AddInMemoryClients(Config.GetClients())
                .AddInMemoryScopes(Config.GetScopes())
                .AddInMemoryUsers(Config.GetUsers());

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            app.UseStaticFiles();

            app.UseDeveloperExceptionPage();

            app.UseIdentityServer();

            app.UseMvcWithDefaultRoute();
        }
    }
}

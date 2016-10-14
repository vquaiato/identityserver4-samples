using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Client
{
public class Startup
{
    public IConfigurationRoot Configuration { get; }
    public Startup(IHostingEnvironment env)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables();
        Configuration = builder.Build();
    }
    public void ConfigureServices(IServiceCollection services) => services.AddMvc();

    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
        app.UseStaticFiles();

        app.UseCookieAuthentication(new CookieAuthenticationOptions
        {
            AuthenticationScheme = "cookies",
            AutomaticAuthenticate = true,
            ExpireTimeSpan = TimeSpan.FromMinutes(60)
        });

        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

        app.UseOpenIdConnectAuthentication(new OpenIdConnectOptions
        {
            AuthenticationScheme = "oidc",
            SignInScheme = "cookies",

            Authority = "http://localhost:5001/",
            RequireHttpsMetadata = false,

            ClientId = "mvc",
            PostLogoutRedirectUri = "http://localhost:5000/",

            ResponseType = "id_token",
            Scope = { "openid", "profile" },

            SaveTokens = true,
        });

        app.UseMvcWithDefaultRoute();
    }
}
}

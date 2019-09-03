using Blazored.LocalStorage;
using WebApplication1.Client;
using WebApplication1.Client.Services;//dsfassdfsafsd
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using WebApplication1.Client.Models;
using Blazor.Extensions;
using Blazored.Localisation;
using Blazored.SessionStorage;

namespace WebApplication1.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddI18nText<Startup>();
            services.AddSingleton<B75Config>();
            // use local storage from blazor
            services.AddBlazoredLocalStorage();

            // authentication things..
            services.AddAuthorizationCore();    // built in!
                                                // add our own authentication stuff
            services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddBlazoredSessionStorage();

            // Signlr for blazor..
            services.AddTransient<HubConnectionBuilder>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            // for timezone support.. since blaxor don't have it yet..
            // https://github.com/jsakamoto/Toolbelt.Blazor.TimeZoneKit Install in NuGet???
            app.UseLocalTimeZone();
            // to get the correct Culture Info based on the users browser... since blazor don't set that yet either
            // https://github.com/Blazored/Localisation
            //app.UseBlazoredLocalisation();

            app.AddComponent<App>("app");
        }
    }
}

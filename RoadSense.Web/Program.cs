using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;
using RoadSense.Web.Services.AuthProvider;
using RoadSense.Web.Services.User;
using RoadSense.Web;

namespace RoadSense.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            // Set the API base URL depending on the environment
            var apiBaseUrl = builder.HostEnvironment.IsDevelopment()
                ? "https://localhost:7002" // Development URL
                : "https://your-production-api.com"; // Production URL

            // Add HttpClient with the base address
            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(apiBaseUrl)
            });

            
            builder.Services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopRight;
                config.SnackbarConfiguration.PreventDuplicates = false;
                config.SnackbarConfiguration.NewestOnTop = false;
                config.SnackbarConfiguration.ShowCloseIcon = true;
                config.SnackbarConfiguration.VisibleStateDuration = 4000;
                config.SnackbarConfiguration.HideTransitionDuration = 500;
                config.SnackbarConfiguration.ShowTransitionDuration = 500;
                config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
            });
            builder.Services.AddBlazoredLocalStorage();

            // Add Authentication
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

            // Add User Authentication Service
            builder.Services.AddScoped<UserAuthService>();

            await builder.Build().RunAsync();
        }
    }
}

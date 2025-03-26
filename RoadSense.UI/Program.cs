using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;
using RoadSense.UI.Services.AuthProvider;
using RoadSense.UI.Services.ManageUsers;
using RoadSense.UI.Services.ManageViolationService;
using RoadSense.UI.Services.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RoadSense.UI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            // Set the API base URL
            var apiBaseUrl = builder.HostEnvironment.IsDevelopment()
                ? "https://localhost:7002"
                : "https://your-production-api.com";

            // Add HttpClient with the base address
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBaseUrl) });

            // Add MudBlazor services
            builder.Services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopRight;
                config.SnackbarConfiguration.PreventDuplicates = false;
                config.SnackbarConfiguration.NewestOnTop = false;
                config.SnackbarConfiguration.ShowCloseIcon = true;
                config.SnackbarConfiguration.VisibleStateDuration = 5000;
                config.SnackbarConfiguration.HideTransitionDuration = 500;
                config.SnackbarConfiguration.ShowTransitionDuration = 500;
                config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
            });

            // Add Blazored Local Storage
            builder.Services.AddBlazoredLocalStorage();

            // Add Authentication
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

            // Add User Authentication Service
            builder.Services.AddScoped<UserAuthService>();
            builder.Services.AddScoped<ViolationService>();
            builder.Services.AddScoped<UserService>();
            // Ensure all async tasks are awaited
            await builder.Build().RunAsync();
        }
    }
}

using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using RoadSense.Components.Pages.Common.Models;

namespace RoadSense.Services.User
{
    public class UserAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        public UserAuthService(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        public async Task<HttpResponseMessage> RegisterAsync(UserLogin user)
        {
            return await _httpClient.PostAsJsonAsync("api/auth/register", user);
        }

        /// <summary>
        /// Logs in a user.
        /// </summary>
        public async Task<bool> LoginAsync(string username, string password)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", new { username, password });

            if (!response.IsSuccessStatusCode)
                return false;

            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "authToken", result.Token);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.Token);

            return true;
        }

        public async Task LogoutAsync()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authToken");
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }

    public class LoginResponse
    {
        public string Token { get; set; }
    }
}

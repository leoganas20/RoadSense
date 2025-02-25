using Blazored.LocalStorage;
using RoadSense.UI.Models;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace RoadSense.UI.Services.User
{
    public class UserAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public UserAuthService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
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
        public async Task<HttpResponseMessage> LoginAsync(string email, string password)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", new { email, password });

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Login failed: {response.StatusCode}, Response: {errorContent}");
                return response;
            }

            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

            if (result == null || string.IsNullOrEmpty(result.Token))
            {
                Console.WriteLine("Error: No token received from API.");
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }

            await _localStorage.SetItemAsync("authToken", result.Token);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.Token);

            return response;
        }

        public async Task LogoutAsync()
        {
            await _localStorage.RemoveItemAsync("authToken");
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }

    public class LoginResponse
    {
        public string Token { get; set; }
    }
}

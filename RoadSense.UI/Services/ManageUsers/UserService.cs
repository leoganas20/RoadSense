using RoadSense.UI.Models;
using RoadSense.UI.Services.ManageViolationService;
using System.Net.Http.Json;

namespace RoadSense.UI.Services.ManageUsers;

public class UserService
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<HttpResponseMessage> AddAsync(UserModel user)
    {
        return await _httpClient.PostAsJsonAsync("api/auth/user/Add", user);
    }

    public async Task<PaginatedResponse<UserModel>> GetUsersAsync(int skip = 0, int take = 10, string? search = null, string? status = null, CancellationToken token = default)
    {
        var queryParams = new Dictionary<string, string>
        {
            { "skip", skip.ToString() },
            { "take", take.ToString() }
        };

        if (!string.IsNullOrEmpty(search))
        {
            queryParams.Add("search", search);
        }

        if (!string.IsNullOrEmpty(status))
        {
            queryParams.Add("status", status);
        }

        var queryString = string.Join("&", queryParams.Select(kv => $"{kv.Key}={Uri.EscapeDataString(kv.Value)}"));
        var url = $"api/auth/user/List?{queryString}";

        return await _httpClient.GetFromJsonAsync<PaginatedResponse<UserModel>>(url, token)
               ?? new PaginatedResponse<UserModel>();
    }

    public async Task<UserModel?> GetByIdAsync(string id)
    {
        return await _httpClient.GetFromJsonAsync<UserModel>($"api/auth/user/{id}");
    }

    public async Task<HttpResponseMessage> UpdateAsync(string id, UserModel user)
    {
        return await _httpClient.PutAsJsonAsync($"api/auth/user/update/{id}", user);
    }

    public async Task<HttpResponseMessage> DeleteAsync(string id)
    {
        return await _httpClient.DeleteAsync($"api/auth/user/delete/{id}");
    }
}
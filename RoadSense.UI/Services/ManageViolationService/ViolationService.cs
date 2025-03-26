using System.Net.Http.Json;
using RoadSense.UI.Models;

namespace RoadSense.UI.Services.ManageViolationService;

public class ViolationService
{
    private readonly HttpClient _httpClient;

    public ViolationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<HttpResponseMessage> AddAsync(ManageViolationModel user)
    {
        return await _httpClient.PostAsJsonAsync("api/auth/Add", user);
    }

    public async Task<PaginatedResponse<ManageViolationModel>> GetViolationsAsync(int skip = 0, int take = 10, string? search = null, string? status = null, CancellationToken token = default)
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
        var url = $"api/auth/List?{queryString}";

        return await _httpClient.GetFromJsonAsync<PaginatedResponse<ManageViolationModel>>(url, token)
               ?? new PaginatedResponse<ManageViolationModel>();
    }

    public async Task<ManageViolationModel?> GetByIdAsync(string id)
    {
        return await _httpClient.GetFromJsonAsync<ManageViolationModel>($"api/auth/{id}");
    }

    public async Task<HttpResponseMessage> UpdateAsync(string id, ManageViolationModel user)
    {
        return await _httpClient.PutAsJsonAsync($"api/auth/update/{id}", user);
    }

    public async Task<HttpResponseMessage> DeleteAsync(string id)
    {
        return await _httpClient.DeleteAsync($"api/auth/delete/{id}");
    }

}

public class PaginatedResponse<T>
{
    public int TotalRecords { get; set; }
    public List<T> Data { get; set; } = new();
}

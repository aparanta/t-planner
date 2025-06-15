using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Model;

namespace GeoLocationClients;

/// <summary>
/// REST API client for GeoLocationProviderAPI.
/// </summary>
public class GeoLocationApiClient : IGeoLocationApiClient
{
    private readonly HttpClient _httpClient;

    public GeoLocationApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }



    public async Task<GeoLocationResponse?> ResolveLocationAsync(GpsPosition position)
    {
        var response = await _httpClient.PostAsJsonAsync("api/geolocation/resolve", position);
        response.EnsureSuccessStatusCode();
        var location = await response.Content.ReadFromJsonAsync<GeoLocationResponse>();
        return location;
    }


}

/// <summary>
/// DTO for the geolocation response.
/// </summary>
public class GeoLocationResponse
{
    public string? Country { get; set; }
    public string? City { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTime Timestamp { get; set; }
}
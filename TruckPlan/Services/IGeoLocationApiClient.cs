using Model;

namespace GeoLocationClients;

public interface IGeoLocationApiClient
{
    /// <summary>
    /// Resolves the location for a given GPS position.
    /// </summary>
    /// <param name="position">The GPS position to resolve.</param>
    /// <returns>The resolved location as a GeoLocationResponse object.</returns>
    public  Task<GeoLocationResponse?> ResolveLocationAsync(GpsPosition position);
}



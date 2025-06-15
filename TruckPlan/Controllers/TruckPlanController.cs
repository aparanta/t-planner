using GeoLocationClients;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.Diagnostics.Metrics;
using TruckPlanManager.DTO;

namespace TruckPlanManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TruckPlanController : ControllerBase
    {
        private readonly ILogger<TruckPlanController> _logger;
        private readonly IGeoLocationApiClient _geoLocationApiClient;

        public TruckPlanController(
            ILogger<TruckPlanController> logger,
            IGeoLocationApiClient geoLocationApiClient)
        {
            _logger = logger;
            _geoLocationApiClient = geoLocationApiClient;
        }

        /// <summary>
        /// Calculates the approximate distance driven for a single TruckPlan.
        /// </summary>
        /// <param name="truckPlan">The TruckPlan containing GPS positions.</param>
        /// <returns>The total distance in kilometers.</returns>
        [HttpPost("distance")]
        public IActionResult CalculateDistance([FromBody] Model.TruckPlan truckPlan)
        {
            if (truckPlan == null || truckPlan.Positions == null || truckPlan.Positions.Count < 2)
                return BadRequest("At least two GPS positions are required to calculate distance.");

            double totalDistance = 0.0;
            for (int i = 1; i < truckPlan.Positions.Count; i++)
            {
                var prev = truckPlan.Positions[i - 1];
                var curr = truckPlan.Positions[i];
                totalDistance += HaversineDistance(
                    prev.Latitude, prev.Longitude,
                    curr.Latitude, curr.Longitude
                );
            }

            return Ok(new DistanceResponse { DistanceKm = totalDistance });
        }

        /// <summary>
        /// Resolves the country for a given GPS position.
        /// </summary>
        /// <param name="position">The GPS position.</param>
        /// <returns>Country as string.</returns>
        [HttpPost("ResolveCountry")]
        public async Task<IActionResult> ResolveCountry([FromBody] GpsPosition position)
        {
            if (position == null)
                return BadRequest("Position is required.");

            
            try
            {
              var  country = await _geoLocationApiClient.ResolveLocationAsync(position);
                if (country is null)
                    return NotFound("Country could not be resolved.");

                return Ok(country);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error resolving country for position: {@Position}", position);
                return StatusCode(500, "An error occurred while resolving the country.");
            }

            
        }

        // Placeholder, logic can be replaced with a more accurate implementation or API call
        // Haversine formula to calculate distance between two GPS points in kilometers
        private static double HaversineDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371.0; // Earth radius in kilometers
            double dLat = DegreesToRadians(lat2 - lat1);
            double dLon = DegreesToRadians(lon2 - lon1);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(DegreesToRadians(lat1)) * Math.Cos(DegreesToRadians(lat2)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }

        private static double DegreesToRadians(double deg) => deg * (Math.PI / 180.0);
    }
}
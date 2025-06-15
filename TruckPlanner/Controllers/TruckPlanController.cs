using Microsoft.AspNetCore.Mvc;
using Model;
using TruckPlanner.DTO;

namespace TruckPlanner.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TruckPlanController : ControllerBase
    {
        private readonly ILogger<TruckPlanController> _logger;

        public TruckPlanController(ILogger<TruckPlanController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Calculates the approximate distance driven for a single TruckPlan.
        /// </summary>
        /// <param name="truckPlan">The TruckPlan containing GPS positions.</param>
        /// <returns>The total distance in kilometers.</returns>
        [HttpPost("distance")]
        public IActionResult CalculateDistance([FromBody] TruckPlan truckPlan)
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
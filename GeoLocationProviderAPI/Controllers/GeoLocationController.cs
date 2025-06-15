using Microsoft.AspNetCore.Mvc;
using Model;

namespace GeoLocationProviderAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeoLocationController : ControllerBase
    {
        private readonly ILogger<GeoLocationController> _logger;

        public GeoLocationController(ILogger<GeoLocationController> logger)
        {
            _logger = logger;
        }

        // Example: POST api/geolocation/resolve
        [HttpPost("resolve")]
        public IActionResult ResolveLocation([FromBody] GpsPosition position)
        {
            if (position == null)
                return BadRequest("Position is required.");

            // Dummy implementation: In a real scenario, call a geolocation provider here.
            var result = new
            {
                Country = GetCountryFromCoordinates(position.Latitude, position.Longitude),
                City = "Sample City",
                Latitude = position.Latitude,
                Longitude = position.Longitude,
                Timestamp = position.Timestamp
            };

            return Ok(result);
        }

        // Dummy method for demonstration
        private string GetCountryFromCoordinates(double latitude, double longitude)
        {
            // Replace with real geolocation logic or API call
            if (latitude > 0)
                return "Country North";
            else
                return "Country South";
        }
    }
}
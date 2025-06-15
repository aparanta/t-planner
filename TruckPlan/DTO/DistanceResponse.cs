namespace TruckPlanManager.DTO;

/// <summary>
/// Represents the response containing the calculated distance in kilometers.
/// </summary>
/// <remarks>
/// This class is used to return the distance calculated from a TruckPlan's GPS positions.
/// </remarks>
public class DistanceResponse
{
   public double DistanceKm { get; set; }
}
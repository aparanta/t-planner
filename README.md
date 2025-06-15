# TruckPlanSolution

This .NET solution manages Truck Plans, including:
- Domain models for drivers, trucks, GPS positions, and truck plans
- Distance calculation for a TruckPlan
- Country lookup from coordinates (external API)
- Missing : Persistence layer


## Projects
- `GeoLocationProviderAPI`: Placeholder REST API for an external service provide
- `Model`: Class library for domain models
- `TruckPlan`: REST API for for main logic

## Getting Started
1. Build the solution: `dotnet build TruckPlan.sln`
2. Run the app: `dotnet run --project TruckPlan/TruckPlan.csproj`

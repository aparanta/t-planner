# TruckPlanSolution

This .NET solution manages Truck Plans, including:
- Domain models for drivers, trucks, GPS positions, and truck plans
- Distance calculation for a TruckPlan
- Country lookup from coordinates (external API)
- Missing: : Function "How many kilometers did
drivers over the age of 50 drive in Germany in February 2018?"
- Missing : Persistence layer
- Missing: UI


## Projects
- `GeoLocationProviderAPI`: Placeholder REST API for an external service provide
- `Model`: Class library for domain models
- `TruckPlan`: REST API for for main application calls the placeholder REST API

## Getting Started
1. Build the solution: `dotnet build TruckPlan.sln`
2. Run the app: `dotnet run --project TruckPlan/TruckPlan.csproj`
3. Run GeoLocationProviderAPI if running Country lookup

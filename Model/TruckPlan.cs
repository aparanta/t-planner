using System;
using System.Collections.Generic;

namespace Model;

public class TruckPlan
{
    public Guid Id { get; set; }
    public Driver Driver { get; set; } = new Driver();
    public Truck Truck { get; set; } = new Truck();
    public DateTime Date { get; set; }
    public List<GpsPosition> Positions { get; set; } = new List<GpsPosition>();
}

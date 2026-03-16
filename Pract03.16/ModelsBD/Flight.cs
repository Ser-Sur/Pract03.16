using System;
using System.Collections.Generic;

namespace Pract03._16.ModelsBD;

public partial class Flight
{
    public int FlightId { get; set; }

    public string Destination { get; set; } = null!;

    public string DepartureTime { get; set; } = null!;

    public string ArrivalTime { get; set; } = null!;

    public int CountFreePlaces { get; set; }

    public string TypePlane { get; set; } = null!;

    public int Capacity { get; set; }
}

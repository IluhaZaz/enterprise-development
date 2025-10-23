namespace CarRental.Domain.Enums;

/// <summary>
/// Car body type options
/// </summary>
public enum CarBodyType
{
    /// <summary>
    /// A car body style with a rear door that opens upwards, typically with a compact design
    /// </summary>
    Hatchback,

    /// <summary>
    /// A station wagon with an extended roofline and a large cargo area
    /// </summary>
    Estate,

    /// <summary>
    /// A traditional three-box sedan with separate compartments for engine, passengers, and trunk
    /// </summary>
    Saloon,

    /// <summary>
    /// A large, rugged vehicle designed for off-road capability and passenger/cargo space
    /// </summary>
    SportsUtilityVehicle,

    /// <summary>
    /// A crossover between a car and an SUV, built on a unibody platform with SUV-like styling
    /// </summary>
    CrossoverUtilityVehicle,

    /// <summary>
    /// A multi-purpose vehicle with sliding doors and flexible seating, ideal for families
    /// </summary>
    Minivan,

    /// <summary>
    /// A two-door car with a fixed roof and sporty design, typically with a sloping rear
    /// </summary>
    Coupe,

    /// <summary>
    /// A car with a retractable or removable roof for open-air driving
    /// </summary>
    Convertible,

    /// <summary>
    /// A luxury vehicle with an extended wheelbase and partition between driver and passengers
    /// </summary>
    Limousine
}
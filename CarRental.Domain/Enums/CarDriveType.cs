namespace CarRental.Domain.Enums;

/// <summary>
/// Types of vehicle drivetrain configurations.
/// </summary>
public enum CarDriveType
{
    /// <summary>
    /// Front-wheel drive (FWD) — engine power is delivered to the front wheels only
    /// </summary>
    FrontWheel,

    /// <summary>
    /// Rear-wheel drive (RWD) — engine power is delivered to the rear wheels only
    /// </summary>
    RearWheel,

    /// <summary>
    /// All-wheel drive (AWD) — power is distributed to all four wheels, either permanently or on-demand
    /// </summary>
    AllWheel
}
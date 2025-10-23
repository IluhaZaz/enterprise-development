namespace CarRental.Domain.Enums;


/// <summary>
/// Types of vehicle transmission systems
/// </summary>
public enum CarTransmissionType
{
    /// <summary>
    /// Automatic transmission — gear shifts are performed automatically without driver input
    /// </summary>
    Automatic,

    /// <summary>
    /// Manual transmission — driver manually selects gears using a clutch pedal and gear stick
    /// </summary>
    Manual,

    /// <summary>
    /// Automated manual transmission (AMT) — a manual gearbox with automated clutch and shifting
    /// </summary>
    AutomatedManual,

    /// <summary>
    /// Continuously Variable Transmission (CVT) — uses a belt and pulley system to provide
    /// an infinite range of gear ratios, resulting in smooth acceleration and improved fuel economy
    /// </summary>
    ContinuouslyVariable
}
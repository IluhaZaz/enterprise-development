namespace CarRental.Domain.Entities;

/// <summary>
/// Contains data about client
/// </summary>
public class Client
{
    /// <summary>
    /// Unique identifier for client
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Client last name
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    /// Cleint's first name
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// Client's patronymic
    /// </summary>
    public required string Patronymic { get; set; }

    /// <summary>
    /// Client's birthday date
    /// </summary>
    public required DateTime BirthDate { get; set; }

    /// <summary>
    /// Client's driver license number
    /// </summary>
    public required string DriverLicense { get; set; }
}
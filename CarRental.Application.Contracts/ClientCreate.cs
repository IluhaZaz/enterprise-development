namespace CarRental.Application.Contracts;

/// <summary>
/// DTO for creating or updating Client object
/// </summary>
public class ClientCreate
{
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
    public required DateOnly BirthDate { get; set; }

    /// <summary>
    /// Client's driver license number
    /// </summary>
    public required string DriverLicense { get; set; }
}
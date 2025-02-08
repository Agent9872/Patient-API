namespace PatientApi.Models;

/// <summary>
/// Represents a patient.
/// </summary>
public class Patient
{
    /// <summary>
    /// Gets or sets the ID of the patient.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the first name of the patient.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the patient.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Gets or sets the date of birth of the patient.
    /// </summary>
    public DateTime DateOfBirth { get; set; }

    /// <summary>
    /// Gets or sets the gender of the patient.
    /// </summary>
    public string? Gender { get; set; }

    /// <summary>
    /// Gets or sets the contact information of the patient.
    /// </summary>
    public string? ContactInfo { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the patient is deleted.
    /// </summary>
    public bool IsDeleted { get; set; } = false;

    /// <summary>
    /// Gets or sets the date and time when the patient was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the list of patient records.
    /// </summary>
    public List<PatientRecord> Records { get; set; } = new();
}

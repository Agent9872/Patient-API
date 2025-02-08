namespace PatientApi.Models;

/// <summary>
/// Represents a patient record.
/// </summary>
public class PatientRecord
{
    /// <summary>
    /// Gets or sets the ID of the patient record.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the ID of the patient.
    /// </summary>
    public int PatientId { get; set; }

    /// <summary>
    /// Gets or sets the date when the patient record was created.
    /// </summary>
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the description of the patient record.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the notes of the patient record.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the patient associated with the record.
    /// </summary>
    public Patient? Patient { get; set; }
}

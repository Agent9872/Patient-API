using PatientApi.Models;

namespace PatientApi.Services;

/// <summary>
/// Interface for managing patients.
/// </summary>
public interface IPatientService
{
    /// <summary>
    /// Retrieves all patients.
    /// </summary>
    /// <returns>A collection of patients.</returns>
    IEnumerable<Patient> GetAllPatients();

    /// <summary>
    /// Retrieves a specific patient by ID.
    /// </summary>
    /// <param name="id">The ID of the patient.</param>
    /// <returns>The patient if found; otherwise, null.</returns>
    Patient? GetPatientById(int id);

    /// <summary>
    /// Creates a new patient.
    /// </summary>
    /// <param name="patient">The patient to create.</param>
    /// <returns>The created patient.</returns>
    Patient? CreatePatient(Patient patient);

    /// <summary>
    /// Updates an existing patient.
    /// </summary>
    /// <param name="id">The ID of the patient to update.</param>
    /// <param name="patient">The updated patient information.</param>
    /// <returns>The updated patient if successful; otherwise, null.</returns>
    Patient? UpdatePatient(int id, Patient patient);

    /// <summary>
    /// Soft deletes a patient by ID.
    /// </summary>
    /// <param name="id">The ID of the patient to delete.</param>
    /// <returns>True if the patient was successfully deleted; otherwise, false.</returns>
    bool SoftDeletePatient(int id);
}

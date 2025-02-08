using PatientApi.Models;

namespace PatientApi.Services;

/// <summary>
/// Interface for managing patient records.
/// </summary>
public interface IPatientRecordService
{
    /// <summary>
    /// Retrieves all records for a specific patient.
    /// </summary>
    /// <param name="patientId">The ID of the patient.</param>
    /// <returns>A collection of patient records.</returns>
    IEnumerable<PatientRecord> GetRecordsForPatient(int patientId);

    /// <summary>
    /// Retrieves a specific record by patient ID and record ID.
    /// </summary>
    /// <param name="patientId">The ID of the patient.</param>
    /// <param name="recordId">The ID of the record.</param>
    /// <returns>The patient record if found; otherwise, null.</returns>
    PatientRecord? GetRecordById(int patientId, int recordId);

    /// <summary>
    /// Creates a new record for a specific patient.
    /// </summary>
    /// <param name="record">The patient record to create.</param>
    /// <returns>The created patient record if successful; otherwise, null.</returns>
    PatientRecord? CreateRecord(PatientRecord record);

    /// <summary>
    /// Updates an existing record for a specific patient.
    /// </summary>
    /// <param name="patientId">The ID of the patient.</param>
    /// <param name="record">The patient record to update.</param>
    /// <returns>The updated patient record if successful; otherwise, null.</returns>
    PatientRecord? UpdateRecord(int patientId, PatientRecord record);
}

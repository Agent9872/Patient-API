using Microsoft.EntityFrameworkCore;
using PatientApi.Data;
using PatientApi.Models;

namespace PatientApi.Services;

/// <summary>
/// Service for managing patient records.
/// </summary>
public class PatientRecordService : IPatientRecordService
{
    private readonly Patient_APIDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="PatientRecordService"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public PatientRecordService(Patient_APIDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves all records for a specific patient.
    /// </summary>
    /// <param name="patientId">The ID of the patient.</param>
    /// <returns>A collection of patient records.</returns>
    public IEnumerable<PatientRecord> GetRecordsForPatient(int patientId)
    {
        var patient = _context.Patients
            .Include(p => p.Records)
            .FirstOrDefault(p => p.Id == patientId && !p.IsDeleted);

        return patient?.Records ?? new List<PatientRecord>();
    }

    /// <summary>
    /// Retrieves a specific record by patient ID and record ID.
    /// </summary>
    /// <param name="patientId">The ID of the patient.</param>
    /// <param name="recordId">The ID of the record.</param>
    /// <returns>The patient record if found; otherwise, null.</returns>
    public PatientRecord? GetRecordById(int patientId, int recordId) =>
        _context.PatientRecords
            .FirstOrDefault(r => r.Id == recordId && r.PatientId == patientId);

    /// <summary>
    /// Creates a new record for a specific patient.
    /// </summary>
    /// <param name="record">The patient record to create.</param>
    /// <returns>The created patient record if successful; otherwise, null.</returns>
    public PatientRecord? CreateRecord(PatientRecord record)
    {
        var patient = _context.Patients
            .FirstOrDefault(p => p.Id == record.PatientId && !p.IsDeleted);
        if (patient == null) return null;

        _context.PatientRecords.Add(record);
        _context.SaveChanges();
        return record;
    }

    /// <summary>
    /// Updates an existing record for a specific patient.
    /// </summary>
    /// <param name="patientId">The ID of the patient.</param>
    /// <param name="record">The patient record to update.</param>
    /// <returns>The updated patient record if successful; otherwise, null.</returns>
    public PatientRecord? UpdateRecord(int patientId, PatientRecord record)
    {
        var existingRecord = GetRecordById(patientId, record.Id);
        if (existingRecord == null) return null;

        existingRecord.Description = record.Description;
        existingRecord.Notes = record.Notes;
        _context.SaveChanges();
        return existingRecord;
    }
}

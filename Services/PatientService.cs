using PatientApi.Data;
using PatientApi.Models;

namespace PatientApi.Services;

/// <summary>
/// Service for managing patients.
/// </summary>
public class PatientService : IPatientService
{
    private readonly Patient_APIDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="PatientService"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public PatientService(Patient_APIDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves all patients.
    /// </summary>
    /// <returns>A collection of patients.</returns>
    public IEnumerable<Patient> GetAllPatients() =>
        _context.Patients.Where(p => !p.IsDeleted).ToList();

    /// <summary>
    /// Retrieves a specific patient by ID.
    /// </summary>
    /// <param name="id">The ID of the patient.</param>
    /// <returns>The patient if found; otherwise, null.</returns>
    public Patient? GetPatientById(int id) =>
        _context.Patients.FirstOrDefault(p => p.Id == id && !p.IsDeleted);

    /// <summary>
    /// Creates a new patient.
    /// </summary>
    /// <param name="patient">The patient to create.</param>
    /// <returns>The created patient.</returns>
    public Patient? CreatePatient(Patient patient)
    {
        _context.Patients.Add(patient);
        _context.SaveChanges();
        return patient;
    }

    /// <summary>
    /// Updates an existing patient.
    /// </summary>
    /// <param name="id">The ID of the patient to update.</param>
    /// <param name="patient">The updated patient information.</param>
    /// <returns>The updated patient if successful; otherwise, null.</returns>
    public Patient? UpdatePatient(int id, Patient patient)
    {
        var existingPatient = GetPatientById(id);
        if (existingPatient == null) return null;

        existingPatient.FirstName = patient.FirstName;
        existingPatient.LastName = patient.LastName;
        existingPatient.DateOfBirth = patient.DateOfBirth;
        existingPatient.Gender = patient.Gender;
        existingPatient.ContactInfo = patient.ContactInfo;

        _context.SaveChanges();
        return existingPatient;
    }

    /// <summary>
    /// Soft deletes a patient by ID.
    /// </summary>
    /// <param name="id">The ID of the patient to delete.</param>
    /// <returns>True if the patient was successfully deleted; otherwise, false.</returns>
    public bool SoftDeletePatient(int id)
    {
        var patient = GetPatientById(id);
        if (patient == null) return false;

        patient.IsDeleted = true;
        _context.SaveChanges();
        return true;
    }
}

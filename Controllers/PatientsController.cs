using Microsoft.AspNetCore.Mvc;
using PatientApi.Models;
using PatientApi.Services;

namespace PatientApi.Controllers;

/// <summary>
/// Controller for managing patients.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _patientService;

    /// <summary>
    /// Initializes a new instance of the <see cref="PatientsController"/> class.
    /// </summary>
    /// <param name="patientService">The patient service.</param>
    public PatientsController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    /// <summary>
    /// Retrieves all patients.
    /// </summary>
    /// <returns>A list of patients.</returns>
    [HttpGet]
    public IActionResult GetAllPatients() =>
        Ok(_patientService.GetAllPatients());

    /// <summary>
    /// Retrieves a specific patient by ID.
    /// </summary>
    /// <param name="id">The ID of the patient.</param>
    /// <returns>The patient with the specified ID.</returns>
    [HttpGet("{id}")]
    public IActionResult GetPatient(int id)
    {
        var patient = _patientService.GetPatientById(id);
        return patient != null ? Ok(patient) : NotFound();
    }

    /// <summary>
    /// Creates a new patient.
    /// </summary>
    /// <param name="patient">The patient to create.</param>
    /// <returns>The created patient.</returns>
    [HttpPost]
    public IActionResult CreatePatient([FromBody] Patient patient)
    {
        var createdPatient = _patientService.CreatePatient(patient);
        if (createdPatient == null)
        {
            return BadRequest("Unable to create patient.");
        }
        return CreatedAtAction(nameof(GetPatient), new { id = createdPatient.Id }, createdPatient);
    }

    /// <summary>
    /// Updates an existing patient.
    /// </summary>
    /// <param name="id">The ID of the patient to update.</param>
    /// <param name="patient">The updated patient data.</param>
    /// <returns>The updated patient.</returns>
    [HttpPut("{id}")]
    public IActionResult UpdatePatient(int id, [FromBody] Patient patient)
    {
        var updatedPatient = _patientService.UpdatePatient(id, patient);
        return updatedPatient != null ? Ok(updatedPatient) : NotFound();
    }

    /// <summary>
    /// Soft deletes a patient by ID.
    /// </summary>
    /// <param name="id">The ID of the patient to delete.</param>
    /// <returns>No content if the deletion was successful, otherwise NotFound.</returns>
    [HttpDelete("{id}")]
    public IActionResult DeletePatient(int id) =>
        _patientService.SoftDeletePatient(id) ? NoContent() : NotFound();
}

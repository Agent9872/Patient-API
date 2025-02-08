using Microsoft.AspNetCore.Mvc;
using PatientApi.Models;
using PatientApi.Services;

namespace PatientApi.Controllers;

/// <summary>
/// Controller for managing patient records.
/// </summary>
[Route("api/patients/{patientId}/[controller]")]
[ApiController]
public class RecordsController : ControllerBase
{
    private readonly IPatientRecordService _recordService;

    /// <summary>
    /// Initializes a new instance of the <see cref="RecordsController"/> class.
    /// </summary>
    /// <param name="recordService">The patient record service.</param>
    public RecordsController(IPatientRecordService recordService)
    {
        _recordService = recordService;
    }

    /// <summary>
    /// Retrieves all records for a specific patient.
    /// </summary>
    /// <param name="patientId">The ID of the patient.</param>
    /// <returns>A list of patient records.</returns>
    [HttpGet]
    public IActionResult GetRecords(int patientId) =>
        Ok(_recordService.GetRecordsForPatient(patientId));

    /// <summary>
    /// Retrieves a specific record by patient ID and record ID.
    /// </summary>
    /// <param name="patientId">The ID of the patient.</param>
    /// <param name="recordId">The ID of the record.</param>
    /// <returns>The patient record if found; otherwise, NotFound.</returns>
    [HttpGet("{recordId}")]
    public IActionResult GetRecord(int patientId, int recordId)
    {
        var record = _recordService.GetRecordById(patientId, recordId);
        return record != null ? Ok(record) : NotFound();
    }

    /// <summary>
    /// Creates a new record for a specific patient.
    /// </summary>
    /// <param name="patientId">The ID of the patient.</param>
    /// <param name="record">The patient record to create.</param>
    /// <returns>The created patient record if successful; otherwise, BadRequest.</returns>
    [HttpPost]
    public IActionResult CreateRecord(int patientId, [FromBody] PatientRecord record)
    {
        record.PatientId = patientId;
        var createdRecord = _recordService.CreateRecord(record);
        return createdRecord != null
            ? CreatedAtAction(nameof(GetRecord), new { patientId, recordId = createdRecord.Id }, createdRecord)
            : BadRequest("Patient not found or deleted.");
    }

    /// <summary>
    /// Updates an existing record for a specific patient.
    /// </summary>
    /// <param name="patientId">The ID of the patient.</param>
    /// <param name="recordId">The ID of the record.</param>
    /// <param name="record">The patient record to update.</param>
    /// <returns>The updated patient record if successful; otherwise, NotFound or BadRequest.</returns>
    [HttpPut("{recordId}")]
    public IActionResult UpdateRecord(int patientId, int recordId, [FromBody] PatientRecord record)
    {
        if (recordId != record.Id)
            return BadRequest("Record ID mismatch.");

        var updatedRecord = _recordService.UpdateRecord(patientId, record);
        return updatedRecord != null ? Ok(updatedRecord) : NotFound();
    }
}

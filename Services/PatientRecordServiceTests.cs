using Microsoft.EntityFrameworkCore;
using PatientApi.Data;
using PatientApi.Models;
using PatientApi.Services;
using Xunit;

namespace PatientApi.Tests.Services;

/// <summary>
/// Contains unit tests for the <see cref="PatientRecordService"/> class.
/// </summary>
public class PatientRecordServiceTests
{
    private readonly DbContextOptions<Patient_APIDbContext> _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="PatientRecordServiceTests"/> class.
    /// </summary>
    public PatientRecordServiceTests()
    {
        _options = new DbContextOptionsBuilder<Patient_APIDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    /// <summary>
    /// Tests that <see cref="PatientRecordService.CreateRecord"/> adds a record to a patient.
    /// </summary>
    [Fact]
    public void CreateRecord_AddsRecordToPatient()
    {
        // Arrange
        using var context = new Patient_APIDbContext(_options);
        var patient = new Patient { Id = 1 };
        context.Patients.Add(patient);
        context.SaveChanges();

        var service = new PatientRecordService(context);
        var record = new PatientRecord { PatientId = 1, Description = "Checkup" };

        // Act
        var createdRecord = service.CreateRecord(record);

        // Assert
        Assert.NotNull(createdRecord);
        Assert.Single(context.PatientRecords);
        Assert.Equal(1, createdRecord.PatientId);
    }

    /// <summary>
    /// Tests that <see cref="PatientRecordService.CreateRecord"/> returns null when the patient is deleted.
    /// </summary>
    [Fact]
    public void CreateRecord_ReturnsNull_WhenPatientIsDeleted()
    {
        // Arrange
        using var context = new Patient_APIDbContext(_options);
        var patient = new Patient { Id = 1, IsDeleted = true };
        context.Patients.Add(patient);
        context.SaveChanges();

        var service = new PatientRecordService(context);
        var record = new PatientRecord { PatientId = 1 };

        // Act
        var result = service.CreateRecord(record);

        // Assert
        Assert.Null(result);
    }

    /// <summary>
    /// Tests that <see cref="PatientRecordService.GetRecordsForPatient"/> returns all records for a patient.
    /// </summary>
    [Fact]
    public void GetRecordsForPatient_ReturnsAllRecords()
    {
        // Arrange
        using var context = new Patient_APIDbContext(_options);
        var patient = new Patient { Id = 1 };
        patient.Records.Add(new PatientRecord { Description = "Record 1" });
        patient.Records.Add(new PatientRecord { Description = "Record 2" });
        context.Patients.Add(patient);
        context.SaveChanges();

        var service = new PatientRecordService(context);

        // Act
        var records = service.GetRecordsForPatient(1).ToList();

        // Assert
        Assert.Equal(2, records.Count);
    }

    /// <summary>
    /// Tests that <see cref="PatientRecordService.UpdateRecord"/> modifies the description and notes of a record.
    /// </summary>
    [Fact]
    public void UpdateRecord_ModifiesDescriptionAndNotes()
    {
        // Arrange
        using var context = new Patient_APIDbContext(_options);
        var record = new PatientRecord { Id = 1, PatientId = 1, Description = "Old" };
        context.PatientRecords.Add(record);
        context.SaveChanges();

        var service = new PatientRecordService(context);
        var updatedRecord = new PatientRecord { Id = 1, Description = "New", Notes = "Updated" };

        // Act
        var result = service.UpdateRecord(1, updatedRecord);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("New", result.Description);
        Assert.Equal("Updated", result.Notes);
    }
}

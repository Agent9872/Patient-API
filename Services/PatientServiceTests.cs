using Microsoft.EntityFrameworkCore;
using PatientApi.Data;
using PatientApi.Models;
using PatientApi.Services;
using Xunit;

namespace PatientApi.Tests.Services;

/// <summary>
/// Contains unit tests for the <see cref="PatientService"/> class.
/// </summary>
public class PatientServiceTests
{
    private readonly DbContextOptions<Patient_APIDbContext> _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="PatientServiceTests"/> class.
    /// </summary>
    public PatientServiceTests()
    {
        // Use a fresh in-memory database for each test
        _options = new DbContextOptionsBuilder<Patient_APIDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    /// <summary>
    /// Tests that <see cref="PatientService.GetAllPatients"/> returns only non-deleted patients.
    /// </summary>
    [Fact]
    public void GetAllPatients_ReturnsOnlyNonDeletedPatients()
    {
        // Arrange
        using var context = new Patient_APIDbContext(_options);
        context.Patients.AddRange(
            new Patient { Id = 1, IsDeleted = false },
            new Patient { Id = 2, IsDeleted = true }
        );
        context.SaveChanges();

        var service = new PatientService(context);

        // Act
        var patients = service.GetAllPatients().ToList();

        // Assert
        Assert.Single(patients);
        Assert.Equal(1, patients[0].Id);
    }

    /// <summary>
    /// Tests that <see cref="PatientService.GetPatientById"/> returns a patient when it exists and is not deleted.
    /// </summary>
    [Fact]
    public void GetPatientById_ReturnsPatient_WhenExistsAndNotDeleted()
    {
        // Arrange
        using var context = new Patient_APIDbContext(_options);
        var patient = new Patient { Id = 1, IsDeleted = false };
        context.Patients.Add(patient);
        context.SaveChanges();

        var service = new PatientService(context);

        // Act
        var result = service.GetPatientById(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    /// <summary>
    /// Tests that <see cref="PatientService.GetPatientById"/> returns null when the patient is deleted.
    /// </summary>
    [Fact]
    public void GetPatientById_ReturnsNull_WhenPatientIsDeleted()
    {
        // Arrange
        using var context = new Patient_APIDbContext(_options);
        context.Patients.Add(new Patient { Id = 1, IsDeleted = true });
        context.SaveChanges();

        var service = new PatientService(context);

        // Act
        var result = service.GetPatientById(1);

        // Assert
        Assert.Null(result);
    }

    /// <summary>
    /// Tests that <see cref="PatientService.CreatePatient"/> adds a patient to the database.
    /// </summary>
    [Fact]
    public void CreatePatient_AddsPatientToDatabase()
    {
        // Arrange
        using var context = new Patient_APIDbContext(_options);
        var service = new PatientService(context);
        var newPatient = new Patient { FirstName = "John", LastName = "Doe" };

        // Act
        var createdPatient = service.CreatePatient(newPatient);

        // Assert
        Assert.NotNull(createdPatient);
        Assert.Equal(1, context.Patients.Count());
    }

    /// <summary>
    /// Tests that <see cref="PatientService.UpdatePatient"/> updates an existing patient.
    /// </summary>
    [Fact]
    public void UpdatePatient_UpdatesExistingPatient()
    {
        // Arrange
        using var context = new Patient_APIDbContext(_options);
        var patient = new Patient { Id = 1, FirstName = "OldName" };
        context.Patients.Add(patient);
        context.SaveChanges();

        var service = new PatientService(context);
        var updatedPatient = new Patient { FirstName = "NewName" };

        // Act
        var result = service.UpdatePatient(1, updatedPatient);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("NewName", context.Patients.Find(1)!.FirstName);
    }

    /// <summary>
    /// Tests that <see cref="PatientService.SoftDeletePatient"/> marks a patient as deleted.
    /// </summary>
    [Fact]
    public void SoftDeletePatient_MarksPatientAsDeleted()
    {
        // Arrange
        using var context = new Patient_APIDbContext(_options);
        var patient = new Patient { Id = 1, IsDeleted = false };
        context.Patients.Add(patient);
        context.SaveChanges();

        var service = new PatientService(context);

        // Act
        var result = service.SoftDeletePatient(1);

        // Assert
        Assert.True(result);
        Assert.True(context.Patients.Find(1)!.IsDeleted);
    }
}

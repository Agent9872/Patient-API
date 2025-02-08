using Microsoft.EntityFrameworkCore;
using PatientApi.Models;

namespace PatientApi.Data
{
    /// <summary>
    /// Represents the database context for the Patient API.
    /// </summary>
    public class Patient_APIDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Patient_APIDbContext"/> class.
        /// </summary>
        /// <param name="options">The options to be used by the DbContext.</param>
        public Patient_APIDbContext(DbContextOptions<Patient_APIDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the Patients DbSet.
        /// </summary>
        public DbSet<Patient> Patients { get; set; }

        /// <summary>
        /// Gets or sets the PatientRecords DbSet.
        /// </summary>
        public DbSet<PatientRecord> PatientRecords { get; set; }

        /// <summary>
        /// Configures the model that was discovered by convention from the entity types
        /// exposed in <see cref="DbSet{TEntity}"/> properties on your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>()
                .HasMany(p => p.Records)
                .WithOne(r => r.Patient)
                .HasForeignKey(r => r.PatientId);
        }
    }
}

using Microsoft.EntityFrameworkCore;

namespace Polyclinic.Domain;

public class PolyclinicDbContext(DbContextOptions<PolyclinicDbContext> options) : DbContext(options)
{
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>()
             .HasOne(a => a.Patient)
             .WithMany()
             .HasForeignKey("PatientId");

        modelBuilder.Entity<Appointment>()
             .HasOne(a => a.Doctor)
             .WithMany()
             .HasForeignKey("DoctorId");

        modelBuilder.Entity<Appointment>()
            .Property(d => d.Conclusion)
            .HasConversion(c => c.ToString(), c => (ConclusionTypes)Enum.Parse(typeof(ConclusionTypes), c));

        modelBuilder.Entity<Doctor>()
            .Property(d => d.Specialization)
            .HasConversion(s => s.ToString(), s => (SpecializationTypes)Enum.Parse(typeof(SpecializationTypes), s));
    }
}
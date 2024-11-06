using Microsoft.EntityFrameworkCore;

namespace Polyclinic.Domain;

public class PolyclinicDbContext(DbContextOptions<PolyclinicDbContext> options) : DbContext(options)
{
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

}

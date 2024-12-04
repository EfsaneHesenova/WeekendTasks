using MediplusMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace MediplusMVC.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<SliderItem> SliderItems { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<HospitalDoctor> HospitalDoctors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(a => a.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(a => a.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HospitalDoctor>()
                .HasOne(a=> a.Hospital)
                .WithMany(a => a.HospitalDoctors)
                .HasForeignKey(a => a.HospitalId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HospitalDoctor>()
                .HasOne(a => a.Doctor)
                .WithMany(a => a.HospitalDoctors)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);
        }


    }
}

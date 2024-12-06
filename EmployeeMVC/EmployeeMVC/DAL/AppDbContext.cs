using EmployeeMVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMVC.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Master> Masters { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Service> Services { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Master>()
                .HasOne(s => s.Service)
                .WithMany(s => s.Masters)
                .HasForeignKey(s => s.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(s => s.Service)
                .WithMany(s => s.Orders)
                .HasForeignKey(s => s.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(s => s.Master)
                .WithMany(s => s.Orders)
                .HasForeignKey(s => s.MasterId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

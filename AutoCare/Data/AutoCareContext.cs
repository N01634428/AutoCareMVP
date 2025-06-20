using AutoCare.API.Models;
using Microsoft.EntityFrameworkCore;
using AutoCare.Models;

namespace AutoCare.API.Data
{
    public class AutoCareContext : DbContext
    {
        public AutoCareContext(DbContextOptions<AutoCareContext> options) : base(options) { }

        // Remove the Services DbSet
        // public DbSet<Service> Services { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Remove seeding for services
        }
    }
}

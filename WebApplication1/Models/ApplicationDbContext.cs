using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        // Constructor to pass options to the base class
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets for your models
        public DbSet<Department>? Departments { get; set; }
        public DbSet<Instructor>? Instructors { get; set; }
        public DbSet<Trainee>? Trainees { get; set; }
        public DbSet<Course>? Courses { get; set; }
        public DbSet<crsResult>? crsResult { get; set; }
        public DbSet<Account>? Accounts { get; set; }

        // Optional: Override OnConfiguring if not using dependency injection
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=MAHMOUD-NASHAAT\\SQLEXPRESS;Database=UNN;Trusted_Connection=True;Encrypt=False");
        }
    }
}

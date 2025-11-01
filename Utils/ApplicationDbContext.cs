using Microsoft.EntityFrameworkCore;
using WorkoutGoalApi.Models;

namespace WorkoutGoalApi.Utils
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) // burada injection yapılmış kavram options 
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; } // DbSet, veritabanındaki tabloları temsil eder
        
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Goal> Goals { get; set; }



/*        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Staff)
                .WithMany()
                .HasForeignKey(a => a.StaffId)
                .OnDelete(DeleteBehavior.Restrict);
        } */
    }
}
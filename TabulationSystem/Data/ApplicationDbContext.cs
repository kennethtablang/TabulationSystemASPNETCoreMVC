using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TabulationSystem.Models;

namespace TabulationSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //this is are DbSets needed for the EF Core
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Criteria> Criteria { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventCategory> EventCategories { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<Year> Years { get; set; }


        //using the fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ApplicationUser and Role
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // ApplicationUser and ManagedEvents
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.ManagedEvents)
                .WithOne(e => e.AdminUser)
                .HasForeignKey(e => e.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // ApplicationUser and AuditLogs
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.AuditLogs)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // ApplicationUser and Notifications
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Notifications)
                .WithOne(n => n.User)
                .HasForeignKey(n => n.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Candidate and ApplicationUser (AdminUser)
            modelBuilder.Entity<Candidate>()
                .HasOne(c => c.AdminUser)
                .WithMany()
                .HasForeignKey(c => c.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Candidate and Year
            modelBuilder.Entity<Candidate>()
                .HasOne(c => c.Year)
                .WithMany(y => y.Candidates)
                .HasForeignKey(c => c.YearId)
                .OnDelete(DeleteBehavior.Restrict);

            // Criteria and ApplicationUser (AdminUser)
            modelBuilder.Entity<Criteria>()
                .HasOne(c => c.AdminUser)
                .WithMany()
                .HasForeignKey(c => c.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Event and ApplicationUser (AdminUser)
            modelBuilder.Entity<Event>()
                .HasOne(e => e.AdminUser)
                .WithMany()
                .HasForeignKey(e => e.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // EventCategory and Event
            modelBuilder.Entity<EventCategory>()
                .HasOne(ec => ec.Event)
                .WithMany(e => e.EventCategories)
                .HasForeignKey(ec => ec.EventId)
                .OnDelete(DeleteBehavior.Restrict);
 
            // Score and Event
            modelBuilder.Entity<Score>()
                .HasOne(s => s.Event)
                .WithMany(e => e.Scores)
                .HasForeignKey(s => s.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            // Score and Criteria
            modelBuilder.Entity<Score>()
                .HasOne(s => s.Criteria)
                .WithMany(c => c.Scores)
                .HasForeignKey(s => s.CriteriaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Score and Judge (ApplicationUser)
            modelBuilder.Entity<Score>()
                .HasOne(s => s.Judge)
                .WithMany(u => u.AssignedScores)
                .HasForeignKey(s => s.JudgeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Score and Candidate
            modelBuilder.Entity<Score>()
                .HasOne(s => s.Candidate)
                .WithMany(c => c.Scores)
                .HasForeignKey(s => s.CandidateId)
                .OnDelete(DeleteBehavior.Restrict);

            // Score and EventCategory
            modelBuilder.Entity<Score>()
                .HasOne(s => s.Category)
                .WithMany(ec => ec.Scores)
                .HasForeignKey(s => s.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }

    }
}

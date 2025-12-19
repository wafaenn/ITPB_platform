using ITBS_Platform.Api.Models;
using ITBS_Platform.Models;
using Microsoft.EntityFrameworkCore;

namespace ITBS_Platform.Data
{
    public class ApplicationDbContext : DbContext
    {
        // ✔ Constructeur utilisé par WinForms
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // ✔ Constructeur utilisé par EF Core pour les migrations
        public ApplicationDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // IMPORTANT : mettre ici la même chaîne que dans appsettingsDb.json
                optionsBuilder.UseSqlServer(
                    "Server=.;Database=ITBS_Platform;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        // Tables
        // Tables existantes
     public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

        // Tables réelles supplémentaires
        public DbSet<Course> Courses { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Participation> Participations { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, RoleName = "Admin" },
                new Role { Id = 2, RoleName = "Formateur" },
                new Role { Id = 3, RoleName = "Etudiant" }
            );

            modelBuilder.Entity<Participation>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId);

            // Participation ↔ Event
            modelBuilder.Entity<Participation>()
                .HasOne(p => p.Event)
                .WithMany()
                .HasForeignKey(p => p.EventId)
                .OnDelete(DeleteBehavior.NoAction);

            // Participation ↔ Course
            modelBuilder.Entity<Participation>()
                .HasOne(p => p.Course)
                .WithMany()
                .HasForeignKey(p => p.CourseId)
                .OnDelete(DeleteBehavior.NoAction);

            // Un étudiant ne peut pas être inscrit deux fois AU MÊME ÉVÉNEMENT
            modelBuilder.Entity<Participation>()
                .HasIndex(p => new { p.EventId, p.UserId })
                .IsUnique();

            // Un étudiant ne peut pas être inscrit deux fois À LA MÊME FORMATION
            modelBuilder.Entity<Participation>()
                .HasIndex(p => new { p.CourseId, p.UserId })
                .IsUnique();


        }


    }
}

using ITBS_Platform.Models;
using Microsoft.EntityFrameworkCore;

namespace ITBS_Platform.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public ApplicationDbContext() { }

        // Tables
        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Formation> Formations => Set<Formation>();
        public DbSet<Inscription> Inscriptions => Set<Inscription>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=.;Database=ITBS_Platform;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relations
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Inscription>()
                .HasOne(i => i.Etudiant)
                .WithMany(u => u.InscriptionsAsEtudiant)
                .HasForeignKey(i => i.EtudiantId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Inscription>()
                .HasOne(i => i.Formation)
                .WithMany(f => f.Inscriptions)
                .HasForeignKey(i => i.FormationId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed des 3 rôles
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "Formateur" },
                new Role { Id = 3, Name = "Etudiant" }
            );

            // Compte admin prêt à l'emploi
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FullName = "Administrateur",
                    Email = "admin@itbs.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    RoleId = 1
                }
            );
        }
    }
}
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
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId);
        }
    }
}

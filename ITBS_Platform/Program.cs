/*using ITBS_Platform.Data;
using ITBS_Platform.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;



namespace ITBS_Platform
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            // ApplicationConfiguration.Initialize();
            // Application.Run(new Form1());
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettingsDb.json", optional: false, reloadOnChange: true);

            IConfiguration configuration = builder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            using (var context = new ApplicationDbContext(optionsBuilder.Options))
            {
                context.Database.EnsureCreated(); // Crée la base de données si elle n'existe pas
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            Application.Run(new Form1());  // Démarre ton formulaire principal WinForms
        }
    }
}*/using ITBS_Platform.Data;
using ITBS_Platform.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ITBS_Platform
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Charger la configuration depuis appsettingsDb.json
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettingsDb.json", optional: false, reloadOnChange: true);

            IConfiguration configuration = builder.Build();

            // Configurer EF Core
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            // Créer le DbContext
            var context = new ApplicationDbContext(optionsBuilder.Options);

            // Créer AuthService avec un vrai DbContext
            var authService = new AuthService(context);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Démarrer Form1 en lui envoyant le AuthService
            Application.Run(new Form1());
        }
    }
}

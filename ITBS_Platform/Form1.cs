using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using ITBS_Platform.Data;
using ITBS_Platform.Services;
using Microsoft.Extensions.Configuration.Json;


namespace ITBS_Platform
{
    public partial class Form1 : Form
    {
        private readonly AuthService _authService;

        public Form1()
        {
            InitializeComponent();

            // Charger la configuration
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettingsDb.json");

            var config = builder.Build();

            // Construire le DbContext
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));

            var context = new ApplicationDbContext(optionsBuilder.Options);

            // Initialiser AuthService
            _authService = new AuthService(context);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Rien à mettre ici pour l'instant
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string email = emailTxt.Text;
            string password = passwordTxt.Text;

            // Vérification de base
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Veuillez entrer l'email et le mot de passe.");
                return;
            }

            // Appel du service de login
            var user = await _authService.LoginAsync(email, password);

            if (user != null)
            {
                MessageBox.Show("Connexion réussie !");

                var dashboard = new DashboardForm();
                dashboard.Show();

                this.Hide();
            }
            else
            {
                MessageBox.Show("Email ou mot de passe incorrect !");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var signup = new SignupForm(_authService);
            signup.ShowDialog();
        
    }
    }
}

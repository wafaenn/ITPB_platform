using ITBS_Platform.Services;
using System;
using System.Windows.Forms;

namespace ITBS_Platform
{
    public partial class SignupForm : Form
    {
        private readonly AuthService _authService;

        public SignupForm(AuthService authService)
        {
            InitializeComponent();
            _authService = authService;
        }
        private void SignupForm_Load(object sender, EventArgs e)
        {
            // Rien à mettre ici pour le moment
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Les mots de passe ne correspondent pas !");
                return;
            }

            bool success = await _authService.CreateUserAsync(
                txtFullName.Text,
                txtEmail.Text,
                txtPassword.Text,
                1
            );

            if (success)
            {
                MessageBox.Show("Compte créé avec succès !");
                var dashboard = new DashboardForm();
                dashboard.Show();

                this.Hide(); // Cacher la page signup
            }
            else
                MessageBox.Show("Email déjà utilisé !");
        }
    }
}

using ITBS_Platform.Models;
using ITBS_Platform.Services;
using System;
using System.Windows.Forms;

namespace ITBS_Platform
{
    public partial class LoginForm : Form
    {
        private readonly authService _authService;

        public LoginForm(authService authService)
        {
            _authService = authService;
            InitializeComponent();
        }

        private void Login_Load(object? sender, EventArgs e)
        {
            // Configuration initiale du formulaire
            this.Text = "ITBS Platform - Connexion";
            passwordTxt.PasswordChar = '•'; // Masquer le mot de passe
        }

        private async void button1_Click(object? sender, EventArgs e)
        {
            string email = emailTxt.Text.Trim();
            string password = passwordTxt.Text;

            // Validation des champs
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Veuillez remplir tous les champs", "Erreur",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Désactiver le bouton pendant la connexion
                login.Enabled = false;
                login.Text = "Connexion...";

                User? authenticatedUser = await _authService.LoginAsync(email, password);

                if (authenticatedUser != null)
                {
                    // Connexion réussie
                    MainForm mainForm = new MainForm(authenticatedUser, _authService);
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Email ou mot de passe incorrect", "Erreur de connexion",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // Réactiver le bouton
                    login.Enabled = true;
                    login.Text = "login";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur de connexion : {ex.Message}", "Erreur",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Réactiver le bouton en cas d'erreur
                login.Enabled = true;
                login.Text = "login";
            }
        }

        private void linkLabel1_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            // Navigation vers le formulaire d'inscription
            SignupForm signupForm = new SignupForm(_authService);
            signupForm.Show();
            this.Hide();
        }
    }
}
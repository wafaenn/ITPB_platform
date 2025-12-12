using ITBS_Platform.Models;
using ITBS_Platform.Services;
using System;
using System.Windows.Forms;

namespace ITBS_Platform
{
    public partial class LoginForm : Form
    {
        // Champ pour le service d'authentification (injection de dépendances)
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

            // ✅ GESTION DU CYCLE DE VIE : Si le formulaire de connexion est fermé, l'application s'arrête.
            this.FormClosed += (s, args) => Application.Exit();
        }

        private async void button1_Click(object? sender, EventArgs e)
        {
            // Note: Nous utilisons l'opérateur '!' sur les TextBoxes (emailTxt, passwordTxt)
            // car ils sont déclarés comme non-nullables dans LoginForm.Designer.cs,
            // garantissant qu'ils sont disponibles après InitializeComponent().
            string email = emailTxt.Text.Trim();
            string password = passwordTxt.Text;

            // Validation des champs
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Veuillez remplir tous les champs", "Erreur",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Désactiver le bouton et indiquer le chargement
            login.Enabled = false;
            login.Text = "Connexion...";

            User? authenticatedUser = null;

            try
            {
                // Tenter la connexion de manière asynchrone
                authenticatedUser = await _authService.LoginAsync(email, password);

                if (authenticatedUser != null)
                {
                    // Connexion réussie
                    MainForm mainForm = new MainForm(authenticatedUser, _authService);
                    mainForm.Show();
                    this.Hide();

                    // Note: Le bloc finally ne sera pas atteint si la connexion réussit et this.Hide() est appelé.
                    // Si this.Hide() est appelé, nous ne réactivons pas le bouton.
                }
                else
                {
                    MessageBox.Show("Email ou mot de passe incorrect", "Erreur de connexion",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur est survenue lors de la connexion : {ex.Message}", "Erreur Système",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // ✅ Le bloc finally garantit que le bouton est réactivé
                // UNIQUEMENT si la connexion a échoué (authenticatedUser est null) et que le formulaire est toujours visible.
                if (authenticatedUser == null && this.Visible)
                {
                    login.Enabled = true;
                    login.Text = "login";
                }
            }
        }

        private void linkLabel1_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            // Navigation vers le formulaire d'inscription
            // Assurez-vous que SignupForm est correctement défini.
            // Si vous n'avez pas encore de SignupForm, vous pouvez laisser le message d'information.

            // Si SignupForm existe:
            SignupForm signupForm = new SignupForm(_authService);
            signupForm.Show();
            this.Hide();

            // Sinon (à décommenter si SignupForm n'est pas prêt):
            // MessageBox.Show("Fonctionnalité d'inscription en cours de développement.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
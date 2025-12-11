using ITBS_Platform.Models;
using ITBS_Platform.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ITBS_Platform
{
    public partial class SignupForm : Form
    {
        private readonly authService _authService;
        private List<Role> _roles = new List<Role>();

        public SignupForm(authService authService)
        {
            InitializeComponent();
            _authService = authService;
        }

        private async void SignupForm_Load(object? sender, EventArgs e)
        {
            this.Text = "Créer un compte";
            this.BackColor = Color.FromArgb(245, 245, 248);

            StyleLabel(labelFullName);
            StyleLabel(labelEmail);
            StyleLabel(labelPassword);
            StyleLabel(labelConfirm);
            StyleLabel(labelRole);

            StyleTextBox(txtFullName);
            StyleTextBox(txtEmail);
            StyleTextBox(txtPassword);
            StyleTextBox(txtConfirmPassword);

            // Masquer le mot de passe
            txtPassword.PasswordChar = '●';
            txtConfirmPassword.PasswordChar = '●';

            // Style du ComboBox
            cmbRole.Font = new Font("Segoe UI", 11);
            cmbRole.BackColor = Color.White;
            cmbRole.ForeColor = Color.Black;

            // Bouton
            btnRegister.Text = "Créer un compte";
            btnRegister.BackColor = Color.FromArgb(0, 120, 215);
            btnRegister.ForeColor = Color.White;
            btnRegister.Font = new Font("Segoe UI Semibold", 12);
            btnRegister.FlatStyle = FlatStyle.Flat;
            btnRegister.FlatAppearance.BorderSize = 0;
            btnRegister.Cursor = Cursors.Hand;

            // ✅ Charger les rôles depuis la base de données
            await ChargerRoles();

            this.CenterToScreen();
        }

        private async Task ChargerRoles()
        {
            try
            {
                _roles = await _authService.GetAllRolesAsync();

                if (_roles == null || _roles.Count == 0)
                {
                    MessageBox.Show("Aucun rôle trouvé dans la base de données.\n" +
                                   "Veuillez exécuter la commande : Update-Database",
                                   "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ✅ MÉTHODE 1 : Utiliser DataSource (recommandée)
                cmbRole.DataSource = null; // Réinitialiser
                cmbRole.DataSource = _roles;
                cmbRole.DisplayMember = "Name"; // Affiche "Admin", "Formateur", "Etudiant"
                cmbRole.ValueMember = "Id";     // Valeur = ID du rôle

                // Sélectionner "Etudiant" par défaut
                var etudiantRole = _roles.Find(r => r.Name == "Etudiant");
                if (etudiantRole != null)
                {
                    cmbRole.SelectedItem = etudiantRole;
                }
                else if (_roles.Count > 0)
                {
                    cmbRole.SelectedIndex = 0; // Sélectionner le premier si "Etudiant" n'existe pas
                }

                /* ✅ MÉTHODE 2 : Utiliser Items.Add (alternative)
                cmbRole.Items.Clear();
                foreach (var role in _roles)
                {
                    cmbRole.Items.Add(role);
                }
                cmbRole.DisplayMember = "Name";
                
                // Sélectionner "Etudiant" par défaut
                int etudiantIndex = _roles.FindIndex(r => r.Name == "Etudiant");
                if (etudiantIndex >= 0)
                {
                    cmbRole.SelectedIndex = etudiantIndex;
                }
                else if (_roles.Count > 0)
                {
                    cmbRole.SelectedIndex = 0;
                }
                */
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des rôles : {ex.Message}\n\n" +
                               $"Détails : {ex.InnerException?.Message}",
                               "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StyleLabel(Label lbl)
        {
            lbl.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            lbl.ForeColor = Color.FromArgb(50, 50, 65);
        }

        private void StyleTextBox(TextBox txt)
        {
            txt.Font = new Font("Segoe UI", 11);
            txt.BorderStyle = BorderStyle.FixedSingle;
            txt.BackColor = Color.White;
            txt.ForeColor = Color.Black;
        }

        private async void button1_Click(object? sender, EventArgs e)
        {
            // Validation des champs
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Veuillez entrer votre nom complet", "Erreur",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Veuillez entrer votre email", "Erreur",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            // Validation simple de l'email
            if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
            {
                MessageBox.Show("Veuillez entrer un email valide", "Erreur",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Veuillez entrer un mot de passe", "Erreur",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            if (txtPassword.Text.Length < 6)
            {
                MessageBox.Show("Le mot de passe doit contenir au moins 6 caractères",
                               "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Les mots de passe ne correspondent pas !", "Erreur",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmPassword.Focus();
                return;
            }

            if (cmbRole.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un rôle", "Erreur",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbRole.Focus();
                return;
            }

            try
            {
                btnRegister.Enabled = false;
                btnRegister.Text = "Création en cours...";
                btnRegister.Cursor = Cursors.WaitCursor;

                Role selectedRole = (Role)cmbRole.SelectedItem;

                bool success = await _authService.CreateUserAsync(
                    txtFullName.Text.Trim(),
                    txtEmail.Text.Trim(),
                    txtPassword.Text,
                    selectedRole.Id
                );

                if (success)
                {
                    MessageBox.Show(
                        $"Compte {selectedRole.Name} créé avec succès !\n\n" +
                        $"Nom : {txtFullName.Text}\n" +
                        $"Email : {txtEmail.Text}\n" +
                        $"Rôle : {selectedRole.Name}\n\n" +
                        "Vous pouvez maintenant vous connecter.",
                        "Succès",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    LoginForm loginForm = new LoginForm(_authService);
                    loginForm.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Cet email est déjà utilisé !", "Erreur",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnRegister.Enabled = true;
                    btnRegister.Text = "Créer un compte";
                    btnRegister.Cursor = Cursors.Hand;
                    txtEmail.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la création du compte :\n{ex.Message}\n\n" +
                               $"Détails : {ex.InnerException?.Message}",
                               "Erreur",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
                btnRegister.Enabled = true;
                btnRegister.Text = "Créer un compte";
                btnRegister.Cursor = Cursors.Hand;
            }
        }
    }
}
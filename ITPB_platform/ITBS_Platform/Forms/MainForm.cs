using ITBS_Platform.Models;
using ITBS_Platform.Services;
using ITBS_Platform.Controls.Admin;
using ITBS_Platform.Controls.Etudiant;
using ITBS_Platform.Controls.Formateur;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ITBS_Platform
{
    public partial class MainForm : Form
    {
        private readonly User _currentUser;
        private readonly authService _authService;
        private Panel contentPanel = new Panel();

        public MainForm(User currentUser, authService authService)
        {
            InitializeComponent();
            _currentUser = currentUser;
            _authService = authService;
            CréerInterface();
        }

        private void CréerInterface()
        {
            this.Text = $"ITBS Platform - {_currentUser.FullName} ({_currentUser.Role?.Name})";
            this.Size = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(35, 35, 50);

            // === SIDEBAR GAUCHE ===
            Panel sidebar = new Panel();
            sidebar.Width = 260;
            sidebar.Dock = DockStyle.Left;
            sidebar.BackColor = Color.FromArgb(28, 28, 42);

            Label logo = new Label();
            logo.Text = "ITBS";
            logo.ForeColor = Color.FromArgb(100, 88, 255);
            logo.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            logo.TextAlign = ContentAlignment.MiddleCenter;
            logo.Dock = DockStyle.Top;
            logo.Height = 100;
            sidebar.Controls.Add(logo);

            ListBox menu = new ListBox();
            menu.BackColor = Color.FromArgb(28, 28, 42);
            menu.ForeColor = Color.White;
            menu.Font = new Font("Segoe UI", 12);
            menu.BorderStyle = BorderStyle.None;
            menu.ItemHeight = 50;
            menu.Dock = DockStyle.Fill;

            // Menu différent selon le rôle
            menu.Items.Add("Tableau de bord");
            menu.Items.Add("Formations");

            if (_currentUser.Role?.Name == "Admin")
            {
                menu.Items.Add("Gestion utilisateurs");
                menu.Items.Add("Valider inscriptions");
            }
            else if (_currentUser.Role?.Name == "Formateur")
            {
                menu.Items.Add("Mes formations");
            }

            menu.Items.Add("Mes inscriptions");
            menu.Items.Add("Déconnexion");
            menu.SelectedIndex = 0;

            menu.SelectedIndexChanged += (s, e) =>
            {
                string? choix = menu.SelectedItem?.ToString();
                if (choix == null) return;

                contentPanel.Controls.Clear();

                if (choix == "Déconnexion")
                {
                    this.Hide();
                    new LoginForm(_authService).Show();
                    return;
                }

                // ✅ Charger le bon dashboard selon le rôle ET le choix du menu
                if (choix == "Tableau de bord")
                {
                    ChargerDashboard();
                }
                else
                {
                    // Pour les autres menus, afficher simplement le titre
                    Label titre = new Label();
                    titre.Text = choix;
                    titre.ForeColor = Color.White;
                    titre.Font = new Font("Segoe UI", 20, FontStyle.Bold);
                    titre.Location = new Point(40, 40);
                    contentPanel.Controls.Add(titre);
                }
            };

            sidebar.Controls.Add(menu);
            this.Controls.Add(sidebar);

            // === ZONE DE CONTENU ===
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.BackColor = Color.FromArgb(35, 35, 50);
            this.Controls.Add(contentPanel);

            // ✅ Charger le dashboard au démarrage
            ChargerDashboard();
        }

        private void ChargerDashboard()
        {
            contentPanel.Controls.Clear();

            UserControl? dashboard = null;

            // ✅ Charger le bon UserControl selon le rôle
            switch (_currentUser.Role?.Name)
            {
                case "Admin":
                    dashboard = new AdminDashboardControl();
                    break;

                case "Formateur":
                    dashboard = new FormateurDashboard();
                    break;

                case "Etudiant":
                    dashboard = new EtudiantDashboardControl();
                    break;

                default:
                    Label errorLabel = new Label
                    {
                        Text = "Rôle non reconnu",
                        ForeColor = Color.Red,
                        Font = new Font("Segoe UI", 16, FontStyle.Bold),
                        Location = new Point(40, 40),
                        AutoSize = true
                    };
                    contentPanel.Controls.Add(errorLabel);
                    return;
            }

            if (dashboard != null)
            {
                dashboard.Dock = DockStyle.Fill;
                contentPanel.Controls.Add(dashboard);
            }
        }
    }
}
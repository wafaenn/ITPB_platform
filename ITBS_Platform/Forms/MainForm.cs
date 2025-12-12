using ITBS_Platform.Models;
using ITBS_Platform.Services;
using ITBS_Platform.Controls;
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
        private Panel contentPanel;

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
            this.Size = new Size(1440, 900);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(18, 18, 18);
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = new Size(1200, 900);

            // Sidebar à gauche
            SidebarControl sidebar = new SidebarControl();
            sidebar.MenuItemClicked += Sidebar_MenuItemClicked;
            sidebar.Dock = DockStyle.Left;
            this.Controls.Add(sidebar);

            // Panel de contenu qui remplit le reste
            contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(18, 18, 18),
                AutoScroll = true
            };
            this.Controls.Add(contentPanel);

            // Charger le dashboard au démarrage
            ChargerDashboard();
        }
        private bool PeutAcceder(string menu)
        {
            return menu switch
            {
                "Dashboard" => true,
                "Utilisateurs" => _currentUser.Role?.Name == "Admin",

                "Programmes" => _currentUser.Role?.Name == "Admin"
                                || _currentUser.Role?.Name == "Formateur"
                                || _currentUser.Role?.Name == "Etudiant",

                "Attestations" => _currentUser.Role?.Name == "Admin"
                                  || _currentUser.Role?.Name == "Formateur"
                                  || _currentUser.Role?.Name == "Etudiant", // pour étudiant, lecture seulement

                "Evaluations" => _currentUser.Role?.Name == "Admin"
                                 || _currentUser.Role?.Name == "Formateur"
                                 || _currentUser.Role?.Name == "Etudiant", // pour étudiant, lecture seulement

                "Notifications" => true, // tous

                _ => false
            };
        }

        private void Sidebar_MenuItemClicked(object? sender, string menuName)
        {
            contentPanel.Controls.Clear();

            if (!PeutAcceder(menuName))
            {
                MessageBox.Show("Accès non autorisé", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ChargerDashboard();
                return;
            }

            switch (menuName)
            {
                case "Dashboard":
                    ChargerDashboard();
                    break;

                case "Utilisateurs":
                    GestionUtilisateursControl usersControl = new GestionUtilisateursControl();
                    usersControl.Dock = DockStyle.Fill;
                    contentPanel.Controls.Add(usersControl);
                    break;

                case "Programmes":
                    ProgrammesControl programmesControl = new ProgrammesControl();
                    programmesControl.Dock = DockStyle.Fill;
                    contentPanel.Controls.Add(programmesControl);
                    break;

                case "Attestations":
                    AttestationsControl attestationsControl = new AttestationsControl();
                    attestationsControl.Dock = DockStyle.Fill;
                    contentPanel.Controls.Add(attestationsControl);
                    break;

                case "Evaluations":
                    EvaluationsControl evaluationsControl = new EvaluationsControl();
                    evaluationsControl.Dock = DockStyle.Fill;
                    contentPanel.Controls.Add(evaluationsControl);
                    break;

                case "Notifications":
                    NotificationsControl notificationsControl = new NotificationsControl();
                    notificationsControl.Dock = DockStyle.Fill;
                    contentPanel.Controls.Add(notificationsControl);
                    break;

                case "Deconnexion":
                    var result = MessageBox.Show(
                        "Voulez-vous vraiment vous déconnecter ?",
                        "Déconnexion",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (result == DialogResult.Yes)
                    {
                        this.Hide();
                        LoginForm loginForm = new LoginForm(_authService);
                        loginForm.FormClosed += (s, args) => this.Close();
                        loginForm.Show();
                    }
                    break;

                default:
                    AfficherPageEnConstruction(menuName);
                    break;
            }
        }

        public void NaviguerVers(UserControl nouvelleVue)
        {
            contentPanel.Controls.Clear();
            nouvelleVue.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(nouvelleVue);
        }
        private void ChargerDashboard()
        {
            contentPanel.Controls.Clear();

            if (_currentUser.Role == null)
            {
                Label errorLabel = new Label
                {
                    Text = "Rôle non défini",
                    ForeColor = Color.Red,
                    Font = new Font("Segoe UI", 16, FontStyle.Bold),
                    Location = new Point(280, 40),
                    AutoSize = true
                };
                contentPanel.Controls.Add(errorLabel);
                return;
            }

            string role = _currentUser.Role.Name.Trim(); // supprimer espaces éventuels

            UserControl? dashboard = null;

            switch (role.ToLower()) // comparer en minuscule pour éviter les problèmes de casse
            {
                case "admin":
                    dashboard = new AdminDashboardControl();
                    break;

                case "formateur":
                    dashboard = new FormateurDashboard();
                    break;

                case "etudiant":
                    dashboard = new EtudiantDashboardControl();
                    break;

                default:
                    Label errorLabel = new Label
                    {
                        Text = "Rôle non reconnu",
                        ForeColor = Color.Red,
                        Font = new Font("Segoe UI", 16, FontStyle.Bold),
                        Location = new Point(280, 40),
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


        private void AfficherPageEnConstruction(string nomPage)
        {
            Panel panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(18, 18, 18)
            };

            Label titre = new Label
            {
                Text = nomPage,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 28, FontStyle.Bold),
                Location = new Point(300, 50),
                AutoSize = true
            };
            panel.Controls.Add(titre);

            Label message = new Label
            {
                Text = "🚧 Cette page est en cours de développement...",
                ForeColor = Color.FromArgb(150, 150, 150),
                Font = new Font("Segoe UI", 16),
                Location = new Point(300, 120),
                AutoSize = true
            };
            panel.Controls.Add(message);

            Label emoji = new Label
            {
                Text = "⚙️",
                ForeColor = Color.FromArgb(100, 200, 200),
                Font = new Font("Segoe UI", 64),
                Location = new Point(300, 200),
                AutoSize = true
            };
            panel.Controls.Add(emoji);

            contentPanel.Controls.Add(panel);
        }
    }
}
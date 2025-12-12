using System;
using System.Drawing;
using System.Windows.Forms;

namespace ITBS_Platform.Controls
{
    public partial class SidebarControl : UserControl
    {
        public event EventHandler<string>? MenuItemClicked;

        public SidebarControl()
        {
            InitializeComponent();
            ConfigurerSidebar();
        }

        private void ConfigurerSidebar()
        {
            this.BackColor = Color.FromArgb(45, 45, 45);
            this.Dock = DockStyle.Left;
            this.Width = 250;

            // ========== LOGO ITBS ==========
            Label lblLogo = new Label
            {
                Text = "ITBS",
                ForeColor = Color.FromArgb(100, 200, 200),
                Font = new Font("Segoe UI", 32, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true
            };
            this.Controls.Add(lblLogo);

            // ========== MENU ITEMS ==========
            int startY = 120;
            int spacing = 80;

            // Dashboard
            Panel btnDashboard = CreerMenuItem("⊞", "Dashboard", 0, startY, "Dashboard");
            this.Controls.Add(btnDashboard);

            // Utilisateurs
            Panel btnUtilisateurs = CreerMenuItem("👥", "Utilisateurs", 0, startY + spacing, "Utilisateurs");
            this.Controls.Add(btnUtilisateurs);

            // Programmes
            Panel btnProgrammes = CreerMenuItem("📚", "Programmes", 0, startY + spacing * 2, "Programmes");
            this.Controls.Add(btnProgrammes);

            // Attestations
            Panel btnAttestations = CreerMenuItem("📜", "Attestations", 0, startY + spacing * 3, "Attestations");
            this.Controls.Add(btnAttestations);

            // Evaluations
            Panel btnEvaluations = CreerMenuItem("📊", "Evaluations", 0, startY + spacing * 4, "Evaluations");
            this.Controls.Add(btnEvaluations);

            // Notifications
            Panel btnNotifications = CreerMenuItem("🔔", "Notifications", 0, startY + spacing * 5, "Notifications");
            this.Controls.Add(btnNotifications);

            // Déconnexion (en bas de la sidebar)
            Panel btnDeconnexion = CreerMenuItem("🚪", "Déconnexion", 0, 700, "Deconnexion");
            btnDeconnexion.BackColor = Color.FromArgb(80, 40, 40);
            this.Controls.Add(btnDeconnexion);
        }

        private Panel CreerMenuItem(string icone, string texte, int x, int y, string action)
        {
            Panel menuItem = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(250, 60),
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
                Tag = action  // Stocker l'action dans le Tag
            };

            // Icône
            Label lblIcone = new Label
            {
                Text = icone,
                Font = new Font("Segoe UI", 16),
                ForeColor = Color.White,
                Location = new Point(15, 15),
                AutoSize = true,
                Cursor = Cursors.Hand,
                Tag = action
            };
            menuItem.Controls.Add(lblIcone);

            // Texte
            Label lblTexte = new Label
            {
                Text = texte,
                Font = new Font("Segoe UI", 13),
                ForeColor = Color.White,
                Location = new Point(55, 20),
                AutoSize = true,
                Cursor = Cursors.Hand,
                Tag = action
            };
            menuItem.Controls.Add(lblTexte);

            // Gestionnaire de clic pour le panel
            menuItem.Click += (s, e) => OnMenuItemClick(action);

            // Gestionnaire de clic pour les labels
            lblIcone.Click += (s, e) => OnMenuItemClick(action);
            lblTexte.Click += (s, e) => OnMenuItemClick(action);

            // Effets hover pour le panel
            menuItem.MouseEnter += (s, e) => menuItem.BackColor = Color.FromArgb(60, 60, 60);
            menuItem.MouseLeave += (s, e) =>
            {
                if (action == "Deconnexion")
                    menuItem.BackColor = Color.FromArgb(80, 40, 40);
                else
                    menuItem.BackColor = Color.Transparent;
            };

            // Effets hover pour les labels
            lblIcone.MouseEnter += (s, e) => menuItem.BackColor = Color.FromArgb(60, 60, 60);
            lblIcone.MouseLeave += (s, e) =>
            {
                if (action == "Deconnexion")
                    menuItem.BackColor = Color.FromArgb(80, 40, 40);
                else
                    menuItem.BackColor = Color.Transparent;
            };

            lblTexte.MouseEnter += (s, e) => menuItem.BackColor = Color.FromArgb(60, 60, 60);
            lblTexte.MouseLeave += (s, e) =>
            {
                if (action == "Deconnexion")
                    menuItem.BackColor = Color.FromArgb(80, 40, 40);
                else
                    menuItem.BackColor = Color.Transparent;
            };

            return menuItem;
        }

        private void OnMenuItemClick(string menuName)
        {
            MenuItemClicked?.Invoke(this, menuName);
        }
    }
}
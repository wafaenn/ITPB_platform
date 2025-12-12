using System;
using System.Drawing;
using System.Windows.Forms;

namespace ITBS_Platform.Controls.Admin
{
    public partial class GestionUtilisateursControl : UserControl
    {
        public GestionUtilisateursControl()
        {
            InitializeComponent();
            ConfigurerInterface();
        }

        private void ConfigurerInterface()
        {
            this.BackColor = Color.FromArgb(18, 18, 18);
            this.Dock = DockStyle.Fill;
            this.AutoScroll = true;
            this.Padding = new Padding(40, 20, 40, 20);

            // ========== HEADER PANEL ==========
            Panel headerPanel = new Panel
            {
                Location = new Point(200, 0),
                Size = new Size(1400, 80),
                BackColor = Color.Transparent
            };

            // TITRE
            Label titre = new Label
            {
                Text = "Utilisateurs",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 28, FontStyle.Regular),
                Location = new Point(115, 20),
                AutoSize = true
            };
            headerPanel.Controls.Add(titre);

            // BOUTON ADD USER
            Button btnAddUser = new Button
            {
                Text = "✚ Add a new User",
                Location = new Point(1100, 15),
                Size = new Size(200, 50),
                BackColor = Color.FromArgb(70, 130, 220),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnAddUser.FlatAppearance.BorderSize = 0;
            btnAddUser.Click += (s, e) => MessageBox.Show("Ajouter un utilisateur");
            headerPanel.Controls.Add(btnAddUser);

            this.Controls.Add(headerPanel);

            // ========== BARRE DE RECHERCHE ET FILTRES ==========
            Panel searchFilterPanel = new Panel
            {
                Location = new Point(300, 100),
                Size = new Size(1400, 60),
                BackColor = Color.Transparent
            };

            // Barre de recherche
            Panel searchPanel = new Panel
            {
                Location = new Point(0, 5),
                Size = new Size(320, 50),
                BackColor = Color.FromArgb(35, 35, 35)
            };

            TextBox searchBox = new TextBox
            {
                Location = new Point(15, 15),
                Size = new Size(250, 30),
                BackColor = Color.FromArgb(35, 35, 35),
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 12),
                BorderStyle = BorderStyle.None,
                Text = "Search..."
            };
            searchBox.GotFocus += (s, e) => { if (searchBox.Text == "Search...") searchBox.Text = ""; };
            searchBox.LostFocus += (s, e) => { if (string.IsNullOrWhiteSpace(searchBox.Text)) searchBox.Text = "Search..."; };
            searchPanel.Controls.Add(searchBox);

            Label searchIcon = new Label
            {
                Text = "🔍",
                Location = new Point(280, 13),
                AutoSize = true,
                Font = new Font("Segoe UI", 16),
                ForeColor = Color.Gray
            };
            searchPanel.Controls.Add(searchIcon);

            searchFilterPanel.Controls.Add(searchPanel);

            // Filtres à droite
            ComboBox filterRole = CreerComboBox("By Role", 920, 5);
            searchFilterPanel.Controls.Add(filterRole);

            ComboBox sortName = CreerComboBox("Name (asc) ↑", 1060, 5);
            searchFilterPanel.Controls.Add(sortName);

            this.Controls.Add(searchFilterPanel);

            // ========== TABLEAU ==========
            Panel tablePanel = new Panel
            {
                Location = new Point(300, 180),
                Size = new Size(1300, 500),
                BackColor = Color.FromArgb(28, 28, 28),
                AutoScroll = true
            };

            // En-têtes
            string[] headers = { "FullName", "Email", "Rôle", "Actions" };
            int[] positions = { 20, 450, 800, 1050 };

            for (int i = 0; i < headers.Length; i++)
            {
                Label header = new Label
                {
                    Text = headers[i],
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    Location = new Point(positions[i], 20),
                    AutoSize = true
                };
                tablePanel.Controls.Add(header);
            }

            // Ligne de séparation
            Panel separatorLine = new Panel
            {
                Location = new Point(0, 55),
                Size = new Size(1300, 1),
                BackColor = Color.FromArgb(60, 60, 60)
            };
            tablePanel.Controls.Add(separatorLine);

            this.Controls.Add(tablePanel);

            // ========== PAGINATION ==========
            Panel paginationPanel = new Panel
            {
                Location = new Point(40, 700),
                Size = new Size(1300, 50),
                BackColor = Color.FromArgb(28, 28, 28)
            };

            Label rowsPerPage = new Label
            {
                Text = "Rows per page:",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10),
                Location = new Point(900, 15),
                AutoSize = true
            };
            paginationPanel.Controls.Add(rowsPerPage);

            ComboBox rowsCombo = new ComboBox
            {
                Location = new Point(1020, 12),
                Size = new Size(60, 30),
                BackColor = Color.FromArgb(35, 35, 35),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            rowsCombo.Items.AddRange(new object[] { "5", "10", "25" });
            rowsCombo.SelectedIndex = 1;
            paginationPanel.Controls.Add(rowsCombo);

            Label pageInfo = new Label
            {
                Text = "0-0 of 0",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10),
                Location = new Point(1100, 15),
                AutoSize = true
            };
            paginationPanel.Controls.Add(pageInfo);

            Button btnPrev = new Button
            {
                Text = "‹",
                Location = new Point(1200, 10),
                Size = new Size(40, 30),
                BackColor = Color.FromArgb(35, 35, 35),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnPrev.FlatAppearance.BorderSize = 0;
            paginationPanel.Controls.Add(btnPrev);

            Button btnNext = new Button
            {
                Text = "›",
                Location = new Point(1250, 10),
                Size = new Size(40, 30),
                BackColor = Color.FromArgb(35, 35, 35),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnNext.FlatAppearance.BorderSize = 0;
            paginationPanel.Controls.Add(btnNext);

            this.Controls.Add(paginationPanel);
        }

        private ComboBox CreerComboBox(string text, int x, int y)
        {
            ComboBox combo = new ComboBox
            {
                Location = new Point(x, y),
                Size = new Size(180, 50),
                BackColor = Color.FromArgb(35, 35, 35),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10),
                FlatStyle = FlatStyle.Flat,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            combo.Items.Add(text);
            combo.SelectedIndex = 0;
            return combo;
        }
    }
}
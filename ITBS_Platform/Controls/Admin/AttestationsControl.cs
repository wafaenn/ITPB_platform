using System;
using System.Drawing;
using System.Windows.Forms;

namespace ITBS_Platform.Controls.Admin
{
    public partial class AttestationsControl : UserControl
    {
        public AttestationsControl()
        {
            InitializeComponent();
            ConfigurerInterface();
        }

        private void ConfigurerInterface()
        {
            this.BackColor = Color.FromArgb(18, 18, 18);
            this.Dock = DockStyle.Fill;
            this.AutoScroll = true;

            // ========== TITRE ==========
            Label titre = new Label
            {
                Text = "Attestations",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 30, FontStyle.Regular),
                Location = new Point(255, 20),
                AutoSize = true
            };
            this.Controls.Add(titre);

            // ========== BARRE DE RECHERCHE ==========
            Panel searchPanel = new Panel
            {
                Location = new Point(275, 100),
                Size = new Size(300, 50),
                BackColor = Color.FromArgb(35, 35, 35)
            };

            TextBox searchBox = new TextBox
            {
                Location = new Point(15, 12),
                Size = new Size(230, 30),
                BackColor = Color.FromArgb(35, 35, 35),
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 11),
                BorderStyle = BorderStyle.None,
                Text = "Search..."
            };
            searchPanel.Controls.Add(searchBox);

            Label searchIcon = new Label
            {
                Text = "🔍",
                Location = new Point(260, 12),
                AutoSize = true,
                Font = new Font("Segoe UI", 14)
            };
            searchPanel.Controls.Add(searchIcon);

            this.Controls.Add(searchPanel);

            // ========== FILTRE ==========
            ComboBox filterParticipant = CreerComboBox("participant (asc) ↑", 1090, 100);
            this.Controls.Add(filterParticipant);

            // ========== TABLEAU ==========
            Panel tablePanel = new Panel
            {
                Location = new Point(240, 180),
                Size = new Size(1500, 500),
                BackColor = Color.FromArgb(28, 28, 28),
                AutoScroll = true
            };

            // En-têtes
            string[] headers = { "Participant", "training", "client", "Actions" };
            int[] positions = { 20, 450, 850, 1150 };

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

            // Lignes de skeleton (placeholder)
            for (int i = 0; i < 5; i++)
            {
                int yPos = 80 + (i * 60);

                // Participant
                Panel skeleton1 = new Panel
                {
                    Location = new Point(20, yPos),
                    Size = new Size(400, 20),
                    BackColor = Color.FromArgb(40, 40, 40)
                };
                tablePanel.Controls.Add(skeleton1);

                // Training
                Panel skeleton2 = new Panel
                {
                    Location = new Point(450, yPos),
                    Size = new Size(350, 20),
                    BackColor = Color.FromArgb(40, 40, 40)
                };
                tablePanel.Controls.Add(skeleton2);

                // Client
                Panel skeleton3 = new Panel
                {
                    Location = new Point(850, yPos),
                    Size = new Size(250, 20),
                    BackColor = Color.FromArgb(40, 40, 40)
                };
                tablePanel.Controls.Add(skeleton3);

                // Actions placeholder
                Panel skeleton4 = new Panel
                {
                    Location = new Point(1150, yPos),
                    Size = new Size(100, 20),
                    BackColor = Color.FromArgb(40, 40, 40)
                };
                tablePanel.Controls.Add(skeleton4);
            }

            this.Controls.Add(tablePanel);

            // ========== PAGINATION ==========
            Panel paginationPanel = new Panel
            {
                Location = new Point(40, 600),
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
                Size = new Size(210, 50),
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
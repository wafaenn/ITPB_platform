using System;
using System.Drawing;
using System.Windows.Forms;

namespace ITBS_Platform.Controls.Admin
{
    public partial class ProgrammesControl : UserControl
    {
        private const int MARGIN_LEFT = 300;

        private const int CONTENT_WIDTH = 1300;
        public ProgrammesControl()
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
                Text = "Programmes",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 28, FontStyle.Regular),
                Location = new Point(255, 20),
                AutoSize = true
            };
            this.Controls.Add(titre);

            // ========== BOUTON EXPORT PDF ==========
            Button btnExport = new Button
            {
                Text = "📄 Export to PDF",
                Location = new Point(1375, 30),
                Size = new Size(170, 45),
                BackColor = Color.FromArgb(60, 60, 60),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnExport.FlatAppearance.BorderSize = 0;
            btnExport.Click += (s, e) => MessageBox.Show("Export PDF");
            this.Controls.Add(btnExport);

            // ========== BARRE DE RECHERCHE ==========
            Panel searchPanel = new Panel
            {
                Location = new Point(285, 100),
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
            ComboBox filterTheme = CreerComboBox("Theme (asc) ↑", 1320, 100);
            this.Controls.Add(filterTheme);

            // ========== TABLEAU ==========
            Panel tablePanel = new Panel
            {
                Location = new Point(240, 180),
                Size = new Size(1500, 500),
                BackColor = Color.FromArgb(28, 28, 28),
                AutoScroll = true
            };

            // En-têtes
            string[] headers = { "Ref", "Title", "Theme", "Start Date", "End Date", "Days", "Trainer", "Trainer Phone", "CIN", "Client", "Client Phone", "Status" };
            int[] positions = { 20, 100, 260, 350, 450, 550, 610, 710, 840, 910, 1000, 1120 };

            for (int i = 0; i < headers.Length; i++)
            {
                Label header = new Label
                {
                    Text = headers[i],
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    Location = new Point(positions[i], 20),
                    AutoSize = true
                };
                tablePanel.Controls.Add(header);
            }

            // Ligne de séparation
            Panel separatorLine = new Panel
            {
                Location = new Point(0, 55),
                Size = new Size(1500, 1),
                BackColor = Color.FromArgb(60, 60, 60)
            };
            tablePanel.Controls.Add(separatorLine);

            // Lignes de skeleton (placeholder)
            for (int i = 0; i < 5; i++)
            {
                int yPos = 80 + (i * 70);
                for (int j = 0; j < headers.Length - 1; j++)
                {
                    Panel skeleton = new Panel
                    {
                        Location = new Point(positions[j], yPos),
                        Size = new Size(60, 20),
                        BackColor = Color.FromArgb(40, 40, 40)
                    };
                    tablePanel.Controls.Add(skeleton);
                }
            }

            this.Controls.Add(tablePanel);

            // ========== PAGINATION ==========
            Panel paginationPanel = new Panel
            {
                Location = new Point(40, 700),
                Size = new Size(1500, 50),
                BackColor = Color.FromArgb(28, 28, 28)
            };

            Label rowsPerPage = new Label
            {
                Text = "Rows per page:",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10),
                Location = new Point(1100, 15),
                AutoSize = true
            };
            paginationPanel.Controls.Add(rowsPerPage);

            ComboBox rowsCombo = new ComboBox
            {
                Location = new Point(1220, 12),
                Size = new Size(60, 30),
                BackColor = Color.FromArgb(35, 35, 35),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            rowsCombo.Items.AddRange(new object[] { "5", "10", "25" });
            rowsCombo.SelectedIndex = 0;
            paginationPanel.Controls.Add(rowsCombo);

            Label pageInfo = new Label
            {
                Text = "0-0 of 0",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10),
                Location = new Point(1300, 15),
                AutoSize = true
            };
            paginationPanel.Controls.Add(pageInfo);

            Button btnPrev = new Button
            {
                Text = "‹",
                Location = new Point(1400, 10),
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
                Location = new Point(1450, 10),
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
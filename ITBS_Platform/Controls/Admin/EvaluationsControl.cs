using System;
using System.Drawing;
using System.Windows.Forms;

namespace ITBS_Platform.Controls.Admin
{
    public partial class EvaluationsControl : UserControl
    {
        // Constante pour la marge latérale (40px)
        private const int MARGIN_LEFT = 255;
       
        private const int CONTENT_WIDTH = 1300;
        

        public EvaluationsControl()
        {
            InitializeComponent();
            ConfigurerInterface();
        }

        private void ConfigurerInterface()
        {
            this.BackColor = Color.FromArgb(18, 18, 18);
            this.Dock = DockStyle.Fill;
            this.AutoScroll = true;

            // Utilisation d'un FlowLayoutPanel pour l'alignement vertical des blocs
            FlowLayoutPanel contentPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true, // S'ajuste à la hauteur du contenu
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false, // Assure que les éléments sont empilés verticalement
                Padding = new Padding(MARGIN_LEFT, 40, 0, 20), // Marge à gauche, haut et bas
                BackColor = Color.Transparent
            };

            // ========== TITRE (Reste en positionnement absolu sur le UserControl pour être simple) ==========
            // NOTE : Le titre est laissé en dehors du FlowLayoutPanel pour éviter un décalage vertical excessif.
            // On le place directement sur le UserControl, et le FlowLayoutPanel commence juste après.
            Label titre = new Label
            {
                Text = "Évaluation",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 30, FontStyle.Regular),
                Location = new Point(MARGIN_LEFT, 20),
                AutoSize = true
            };
            this.Controls.Add(titre);


            // Ajustement de la position du FlowLayoutPanel pour commencer après le titre
            contentPanel.Location = new Point(0, titre.Bottom + 10); // Commence sous le titre

            // ========== ONGLETS ==========
            Panel tabsPanel = new Panel
            {
                // Pas de Location, géré par le FlowLayoutPanel
                Size = new Size(CONTENT_WIDTH, 50),
                BackColor = Color.Transparent,
                Margin = new Padding(0, 40, 0, 0) // Marge supérieure (espace entre le titre et les onglets)
            };

            Button tabMyEval = new Button
            {
                Text = "MON ÉVALUATIONS (1)",
                Location = new Point(0, 0), // Positionnement interne au panel
                Size = new Size(250, 50),
                BackColor = Color.Transparent,
                ForeColor = Color.FromArgb(70, 130, 220),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            tabMyEval.FlatAppearance.BorderSize = 0;
            tabMyEval.FlatAppearance.BorderColor = Color.FromArgb(70, 130, 220);
            tabsPanel.Controls.Add(tabMyEval);

            // Ligne bleue sous l'onglet actif
            Panel activeLine = new Panel
            {
                Location = new Point(0, 48),
                Size = new Size(250, 2),
                BackColor = Color.FromArgb(70, 130, 220)
            };
            tabsPanel.Controls.Add(activeLine);

           

            contentPanel.Controls.Add(tabsPanel); // Ajout au FlowLayoutPanel

            // ========== TABLEAU (DATAGRID/LISTE) ==========
            Panel tablePanel = new Panel
            {
                // Pas de Location, géré par le FlowLayoutPanel
                Size = new Size(CONTENT_WIDTH, 400),
                BackColor = Color.FromArgb(28, 28, 28),
                AutoScroll = true,
                Margin = new Padding(0, 0, 0, 0) // Pas de marge supplémentaire
            };

            // En-têtes
            string[] headers = { "Formation", "Évaluation", "Notation", "Statu", "Soumis" };
            int[] positions = { 20, 250, 480, 810, 1070, 1220 };

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
                Size = new Size(CONTENT_WIDTH, 1),
                BackColor = Color.FromArgb(60, 60, 60)
            };
            tablePanel.Controls.Add(separatorLine);

            // ========== EXEMPLE DE LIGNE (Répéter pour chaque ligne de données) ==========
            int yPos = 80;

            // Training
            Label lblTraining = new Label
            {
                Text = "Ref:",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10),
                Location = new Point(20, yPos),
                AutoSize = true
            };
            tablePanel.Controls.Add(lblTraining);

            // Evaluating
            Panel badgeTrainer = new Panel
            {
                Location = new Point(250, yPos - 5),
                Size = new Size(80, 30),
                BackColor = Color.FromArgb(50, 50, 60)
            };
            Label lblTrainer = new Label
            {
                Text = "Formation",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9),
                Location = new Point(15, 7),
                AutoSize = true
            };
            badgeTrainer.Controls.Add(lblTrainer);
            tablePanel.Controls.Add(badgeTrainer);

            Label lblNA = new Label
            {
                Text = "N/A",
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 9),
                Location = new Point(250, yPos + 30),
                AutoSize = true
            };
            tablePanel.Controls.Add(lblNA);

            // Rating - étoiles
            Panel starsPanel = new Panel
            {
                Location = new Point(480, yPos),
                Size = new Size(150, 30),
                BackColor = Color.Transparent
            };
            string stars = "⭐⭐⭐⭐⭐";
            Label lblStars = new Label
            {
                Text = stars + " 5.0",
                ForeColor = Color.FromArgb(255, 180, 0),
                Font = new Font("Segoe UI", 12),
                AutoSize = true
            };
            starsPanel.Controls.Add(lblStars);
            tablePanel.Controls.Add(starsPanel);

            // Status
            Panel statusBadge = new Panel
            {
                Location = new Point(810, yPos - 5),
                Size = new Size(100, 30),
                BackColor = Color.FromArgb(50, 150, 80)
            };
            Label lblStatus = new Label
            {
                Text = "soumis",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Location = new Point(15, 7),
                AutoSize = true
            };
            statusBadge.Controls.Add(lblStatus);
            tablePanel.Controls.Add(statusBadge);

            // Submitted
            Label lblDate = new Label
            {
                Text = "04/06/2025",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10),
                Location = new Point(1070, yPos),
                AutoSize = true
            };
            tablePanel.Controls.Add(lblDate);

        

            contentPanel.Controls.Add(tablePanel); // Ajout au FlowLayoutPanel

            // ========== PAGINATION ==========
            Panel paginationPanel = new Panel
            {
                // Pas de Location, géré par le FlowLayoutPanel
                Size = new Size(CONTENT_WIDTH, 50),
                BackColor = Color.FromArgb(28, 28, 28),
                Margin = new Padding(0, 20, 0, 0) // Marge supérieure pour séparer du tableau
            };

            // Éléments de pagination positionnés à droite (avec Location)
            Label rowsPerPage = new Label
            {
                Text = "Lignes par page :",
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
                // Apparence pour FlatStyle (doit être configuré dans le designer pour un style plat complet)
                // FlatStyle = FlatStyle.Flat 
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

            contentPanel.Controls.Add(paginationPanel); // Ajout au FlowLayoutPanel

            this.Controls.Add(contentPanel); // Ajout du FlowLayoutPanel au UserControl
            contentPanel.BringToFront(); // S'assurer que le contenu est visible (important si le titre est en position absolue)

        }
    }
}
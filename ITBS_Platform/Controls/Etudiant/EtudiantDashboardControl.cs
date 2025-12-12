using System;
using System.Drawing;
using System.Windows.Forms;

namespace ITBS_Platform.Controls.Etudiant
{
    public partial class EtudiantDashboardControl : UserControl
    {
        public EtudiantDashboardControl()
        {
            InitializeComponent();
            ConfigurerInterface();
        }

        private void ConfigurerInterface()
        {
            this.BackColor = Color.FromArgb(35, 35, 50);
            this.Dock = DockStyle.Fill;
            this.AutoScroll = true;

            // ========== TITRE ==========
            Label titre = new Label
            {
                Text = "Tableau de bord Étudiant",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 28, FontStyle.Bold),
                Location = new Point(320, 30),
                AutoSize = true
            };
            this.Controls.Add(titre);

            // ========== STATISTIQUES (3 Cartes) ==========
            int cardY = 100;
            int cardSpacing = 20;
            int cardWidth = 280;
            int cardHeight = 140;

            // Carte 1 : Formations inscrites
            Panel card1 = CreerCarte("Mes Inscriptions", "7", "📚", Color.FromArgb(100, 88, 255), 250, cardY, cardWidth, cardHeight);
            this.Controls.Add(card1);

            // Carte 2 : Formations en cours
            Panel card2 = CreerCarte("En cours", "3", "🎯", Color.FromArgb(52, 199, 89), 340 + cardWidth + cardSpacing, cardY, cardWidth, cardHeight);
            this.Controls.Add(card2);

            // Carte 3 : Formations terminées
            Panel card3 = CreerCarte("Terminées", "4", "✅", Color.FromArgb(255, 149, 0), 440 + (cardWidth + cardSpacing) * 2, cardY, cardWidth, cardHeight);
            this.Controls.Add(card3);

            // ========== ACTIONS RAPIDES ==========
            Label lblActions = new Label
            {
                Text = "Actions rapides",
                ForeColor = Color.Lavender,
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                Location = new Point(280, 270),
                AutoSize = true
            };
            this.Controls.Add(lblActions);

            Button btnExplorerFormations = CreerBoutonAction("🔍 Explorer les formations", 240, 320);
            btnExplorerFormations.Click += (s, e) => MessageBox.Show("Redirection vers catalogue formations");
            this.Controls.Add(btnExplorerFormations);

            Button btnMesInscriptions = CreerBoutonAction("📋 Mes inscriptions", 580, 320);
            btnMesInscriptions.Click += (s, e) => MessageBox.Show("Redirection vers mes inscriptions");
            this.Controls.Add(btnMesInscriptions);

            Button btnCertificats = CreerBoutonAction("🎓 Mes certificats", 920, 320);
            btnCertificats.Click += (s, e) => MessageBox.Show("Redirection vers certificats");
            this.Controls.Add(btnCertificats);

            // ========== MES FORMATIONS ==========
            Label lblFormations = new Label
            {
                Text = "Mes formations",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                Location = new Point(300, 420),
                AutoSize = true
            };
            this.Controls.Add(lblFormations);

            Panel panelFormations = new Panel
            {
                Location = new Point(300, 470),
                Size = new Size(1400, 250),
                BackColor = Color.FromArgb(28, 28, 42),
                AutoScroll = true
            };

            string[][] formations = {
                new[] { "Python Avancé", "En cours", "65%" },
                new[] { "C# pour débutants", "En cours", "80%" },
                new[] { "JavaScript ES6", "Terminée", "100%" },
                new[] { "React & Redux", "En cours", "40%" },
                new[] { "SQL & Bases de données", "Terminée", "100%" }
            };

            int yPos = 20;
            foreach (var formation in formations)
            {
                Panel formationCard = CreerCarteFormation(formation[0], formation[1], formation[2], 20, yPos);
                panelFormations.Controls.Add(formationCard);
                yPos += 60;
            }

            this.Controls.Add(panelFormations);
        }

        private Panel CreerCarte(string titre, string valeur, string emoji, Color couleur, int x, int y, int width, int height)
        {
            Panel carte = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(width, height),
                BackColor = couleur
            };

            Label lblEmoji = new Label
            {
                Text = emoji,
                Font = new Font("Segoe UI", 26),
                ForeColor = Color.White,
                Location = new Point(15, 15),
                AutoSize = true
            };
            carte.Controls.Add(lblEmoji);

            Label lblValeur = new Label
            {
                Text = valeur,
                Font = new Font("Segoe UI", 32, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(190, 15),
                AutoSize = true
            };
            carte.Controls.Add(lblValeur);

            Label lblTitre = new Label
            {
                Text = titre,
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.FromArgb(230, 230, 230),
                Location = new Point(15, 105),
                AutoSize = true
            };
            carte.Controls.Add(lblTitre);

            return carte;
        }

        private Button CreerBoutonAction(string texte, int x, int y)
        {
            Button btn = new Button
            {
                Text = texte,
                Location = new Point(x, y),
                Size = new Size(300, 60),
                BackColor = Color.FromArgb(45, 45, 60),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(55, 55, 70);
            return btn;
        }

        private Panel CreerCarteFormation(string titre, string statut, string progression, int x, int y)
        {
            Panel card = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(950, 50),
                BackColor = Color.FromArgb(45, 45, 60)
            };

            Label lblTitre = new Label
            {
                Text = titre,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(15, 15),
                AutoSize = true
            };
            card.Controls.Add(lblTitre);

            Color statutColor = statut == "En cours" ? Color.FromArgb(52, 199, 89) : Color.Gray;

            Label lblStatut = new Label
            {
                Text = statut,
                ForeColor = Color.White,
                BackColor = statutColor,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Location = new Point(400, 12),
                Size = new Size(100, 25),
                TextAlign = ContentAlignment.MiddleCenter
            };
            card.Controls.Add(lblStatut);

            Label lblProgression = new Label
            {
                Text = $"Progression: {progression}",
                ForeColor = Color.FromArgb(200, 200, 200),
                Font = new Font("Segoe UI", 10),
                Location = new Point(550, 17),
                AutoSize = true
            };
            card.Controls.Add(lblProgression);

            Button btnAcceder = new Button
            {
                Text = "Accéder",
                Location = new Point(800, 10),
                Size = new Size(90, 30),
                BackColor = Color.FromArgb(100, 88, 255),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnAcceder.FlatAppearance.BorderSize = 0;
            btnAcceder.Click += (s, e) => MessageBox.Show($"Accès à : {titre}");
            card.Controls.Add(btnAcceder);

            return card;
        }
    }
}
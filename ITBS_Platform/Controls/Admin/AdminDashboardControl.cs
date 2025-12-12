using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace ITBS_Platform.Controls.Admin
{
    public partial class AdminDashboardControl : UserControl
    {
        public AdminDashboardControl()
        {
            InitializeComponent();
            ConfigurerInterface();
        }

        private void ConfigurerInterface()
        {
            this.BackColor = Color.FromArgb(18, 18, 18);
            this.Dock = DockStyle.Fill;
            this.AutoScroll = true;

            // ========== TITRE PRINCIPAL ==========
            Label titre = new Label
            {
                Text = "ADMIN DASHBOARD",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 24, FontStyle.Regular),
                Location = new Point(280, 30),
                AutoSize = true
            };
            this.Controls.Add(titre);

            // ========== STATISTIQUES (5 Cartes en ligne) ==========
            int cardY = 90;
            int cardSpacing = 15;
            int cardWidth = 235;
            int cardHeight = 60;
            int startX = 280;

            // Carte 1 : Les événements total
            Panel card1 = CreerCarteModerne("11", "Les événements total", "📅", Color.FromArgb(139, 69, 69), startX, cardY, cardWidth, cardHeight);
            this.Controls.Add(card1);

            // Carte 2 : Complet
            Panel card2 = CreerCarteModerne("0", "Complet", "✅", Color.FromArgb(60, 60, 60), startX + (cardWidth + cardSpacing), cardY, cardWidth, cardHeight);
            this.Controls.Add(card2);

            // Carte 3 : Activé
            Panel card3 = CreerCarteModerne("1", "Activé", "📊", Color.FromArgb(70, 80, 100), startX + (cardWidth + cardSpacing) * 2, cardY, cardWidth, cardHeight);
            this.Controls.Add(card3);

            // Carte 4 : En attente
            Panel card4 = CreerCarteModerne("1", "En attente", "📦", Color.FromArgb(120, 80, 50), startX + (cardWidth + cardSpacing) * 3, cardY, cardWidth, cardHeight);
            this.Controls.Add(card4);

            // Carte 5 : Utilisateurs
            Panel card5 = CreerCarteModerne("50", "utilisateurs", "👥", Color.FromArgb(50, 80, 120), startX + (cardWidth + cardSpacing) * 4, cardY, cardWidth, cardHeight);
            this.Controls.Add(card5);

            // ========== SECTION APERÇU DE LA PROGRESSION ==========
            Label lblProgression = new Label
            {
                Text = "Aperçu de la progression de la formation",
                ForeColor = Color.FromArgb(220, 100, 80),
                Font = new Font("Segoe UI", 12, FontStyle.Regular),
                Location = new Point(280, 180),
                AutoSize = true
            };
            this.Controls.Add(lblProgression);

            // Trois panneaux d'informations
            Panel panelTaux = CreerPanelInfo("Taux global d'achèvement :", "97.5 %", 280, 215, 200, 80);
            this.Controls.Add(panelTaux);

            Panel panelProgramme = CreerPanelInfo("Programme de formation actifs :", "🔵 En cours", 500, 215, 220, 80);
            this.Controls.Add(panelProgramme);

            Panel panelDemandes = CreerPanelInfo("Demandes en attente", "🔴 La attente d'approbation", 740, 215, 220, 80);
            this.Controls.Add(panelDemandes);

            // ========== GRAPHIQUES EN BAS ==========
            int graphY = 320;

            // Panel 1 : Aperçu des statistiques
            Panel panelGraph1 = CreerPanelGraphique("Aperçu des statistiques de formation", Color.FromArgb(80, 50, 30), 280, graphY, 230, 180);
            AjouterGraphiqueCamembert(panelGraph1);
            this.Controls.Add(panelGraph1);

            // Panel 2 : Analyse de l'investissement
            Panel panelGraph2 = CreerPanelGraphique("Analyse de l'investissement en formation", Color.FromArgb(30, 50, 50), 530, graphY, 220, 180);
            AjouterListeFormations(panelGraph2);
            this.Controls.Add(panelGraph2);

            // Panel 3 : Statistiques des utilisateurs
            Panel panelGraph3 = CreerPanelGraphique("Aperçu des statistiques des utilisateurs", Color.FromArgb(80, 50, 30), 770, graphY, 220, 180);
            AjouterGraphiqueUtilisateurs(panelGraph3);
            this.Controls.Add(panelGraph3);

            // ========== GRAPHIQUE ÉVOLUTION DES DEMANDES ==========
            Label lblEvolution = new Label
            {
                Text = "Évolution des demandes de formation",
                ForeColor = Color.FromArgb(220, 100, 80),
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                Location = new Point(280, 520),
                AutoSize = true
            };
            this.Controls.Add(lblEvolution);

            Panel panelEvolution = CreerPanelGraphique("", Color.FromArgb(25, 25, 25), 280, 550, 420, 150);
            AjouterGraphiqueEvolution(panelEvolution);
            this.Controls.Add(panelEvolution);

            // Panel Training Focus Areas
            Panel panelTraining = CreerPanelGraphique("Training Focus Areas", Color.FromArgb(25, 25, 25), 720, 550, 270, 150);
            AjouterGraphiqueBarres(panelTraining);
            this.Controls.Add(panelTraining);
        }

        private Panel CreerCarteModerne(string valeur, string titre, string emoji, Color couleur, int x, int y, int width, int height)
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
                Font = new Font("Segoe UI", 14),
                ForeColor = Color.White,
                Location = new Point(width - 35, 8),
                AutoSize = true
            };
            carte.Controls.Add(lblEmoji);

            Label lblValeur = new Label
            {
                Text = valeur,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(10, 8),
                AutoSize = true
            };
            carte.Controls.Add(lblValeur);

            Label lblTitre = new Label
            {
                Text = titre,
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.FromArgb(200, 200, 200),
                Location = new Point(10, 38),
                AutoSize = true,
                MaximumSize = new Size(width - 20, 0)
            };
            carte.Controls.Add(lblTitre);

            return carte;
        }

        private Panel CreerPanelInfo(string titre, string valeur, int x, int y, int width, int height)
        {
            Panel panel = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(width, height),
                BackColor = Color.FromArgb(30, 30, 30)
            };

            Label lblTitre = new Label
            {
                Text = titre,
                ForeColor = Color.FromArgb(180, 180, 180),
                Font = new Font("Segoe UI", 9),
                Location = new Point(15, 15),
                AutoSize = true
            };
            panel.Controls.Add(lblTitre);

            Label lblValeur = new Label
            {
                Text = valeur,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(15, 40),
                AutoSize = true
            };
            panel.Controls.Add(lblValeur);

            return panel;
        }

        private Panel CreerPanelGraphique(string titre, Color couleur, int x, int y, int width, int height)
        {
            Panel panel = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(width, height),
                BackColor = couleur
            };

            if (!string.IsNullOrEmpty(titre))
            {
                Label lblTitre = new Label
                {
                    Text = titre,
                    ForeColor = Color.FromArgb(200, 150, 100),
                    Font = new Font("Segoe UI", 10),
                    Location = new Point(15, 12),
                    AutoSize = true,
                    MaximumSize = new Size(width - 30, 0)
                };
                panel.Controls.Add(lblTitre);
            }

            return panel;
        }

        private void AjouterGraphiqueCamembert(Panel panel)
        {
            // Simulation d'un graphique camembert
            Label legend1 = new Label
            {
                Text = "🟢 Confirmées : 14.5%",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 8),
                Location = new Point(15, 130),
                AutoSize = true
            };
            panel.Controls.Add(legend1);

            Label legend2 = new Label
            {
                Text = "🔵 Terminées : 40.1%",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 8),
                Location = new Point(15, 150),
                AutoSize = true
            };
            panel.Controls.Add(legend2);
        }

        private void AjouterListeFormations(Panel panel)
        {
            string[] formations = {
                "D'une moyenne : 165 jours",
                "Formation la plus courte : 3 jours",
                "Formation la plus longue : 90 jours",
                "Nombre total de jours : 173 jours"
            };

            int yPos = 45;
            foreach (var formation in formations)
            {
                Label lbl = new Label
                {
                    Text = formation,
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 8),
                    Location = new Point(15, yPos),
                    AutoSize = true,
                    MaximumSize = new Size(190, 0)
                };
                panel.Controls.Add(lbl);
                yPos += 25;
            }
        }

        private void AjouterGraphiqueUtilisateurs(Panel panel)
        {
            // Légendes
            Label legend1 = new Label
            {
                Text = "🟠 Formateurs : 14%",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 8),
                Location = new Point(15, 130),
                AutoSize = true
            };
            panel.Controls.Add(legend1);

            Label legend2 = new Label
            {
                Text = "🟡 Étudiants",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 8),
                Location = new Point(15, 150),
                AutoSize = true
            };
            panel.Controls.Add(legend2);
        }

        private void AjouterGraphiqueEvolution(Panel panel)
        {
            Label legend = new Label
            {
                Text = "pending : 0\nconfirmed : 1\ncompleted : 1\ncancelled : 0",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 8),
                Location = new Point(panel.Width - 100, 20),
                AutoSize = true
            };
            panel.Controls.Add(legend);
        }

        private void AjouterGraphiqueBarres(Panel panel)
        {
            Label lblCount = new Label
            {
                Text = "Count : 1 trainings",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 8),
                Location = new Point(15, panel.Height - 30),
                AutoSize = true
            };
            panel.Controls.Add(lblCount);
        }
    }
}
using System;
using System.Drawing;
using System.Windows.Forms;

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
            this.BackColor = Color.FromArgb(35, 35, 50);
            this.Dock = DockStyle.Fill;
            this.AutoScroll = true;

            // ========== TITRE ==========
            Label titre = new Label
            {
                Text = "Tableau de bord Administrateur",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 28, FontStyle.Bold),
                Location = new Point(40, 30),
                AutoSize = true
            };
            this.Controls.Add(titre);

            // ========== STATISTIQUES (4 Cartes) ==========
            int cardY = 100;
            int cardSpacing = 20;
            int cardWidth = 250;
            int cardHeight = 140;

            // Carte 1 : Total Utilisateurs
            Panel card1 = CreerCarte("Utilisateurs", "156", "👥", Color.FromArgb(100, 88, 255), 40, cardY, cardWidth, cardHeight);
            this.Controls.Add(card1);

            // Carte 2 : Formations
            Panel card2 = CreerCarte("Formations", "23", "📚", Color.FromArgb(52, 199, 89), 40 + cardWidth + cardSpacing, cardY, cardWidth, cardHeight);
            this.Controls.Add(card2);

            // Carte 3 : Inscriptions en attente
            Panel card3 = CreerCarte("En attente", "12", "⏳", Color.FromArgb(255, 149, 0), 40 + (cardWidth + cardSpacing) * 2, cardY, cardWidth, cardHeight);
            this.Controls.Add(card3);

            // Carte 4 : Formateurs actifs
            Panel card4 = CreerCarte("Formateurs", "8", "👨‍🏫", Color.FromArgb(0, 199, 190), 40 + (cardWidth + cardSpacing) * 3, cardY, cardWidth, cardHeight);
            this.Controls.Add(card4);

            // ========== ACTIONS RAPIDES ==========
            Label lblActions = new Label
            {
                Text = "Actions rapides",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                Location = new Point(40, 270),
                AutoSize = true
            };
            this.Controls.Add(lblActions);

            // Boutons d'actions
            Button btnGererUtilisateurs = CreerBoutonAction("👥 Gérer les utilisateurs", 40, 320);
            btnGererUtilisateurs.Click += (s, e) => MessageBox.Show("Redirection vers gestion utilisateurs");
            this.Controls.Add(btnGererUtilisateurs);

            Button btnValiderInscriptions = CreerBoutonAction("✅ Valider les inscriptions", 320, 320);
            btnValiderInscriptions.Click += (s, e) => MessageBox.Show("Redirection vers validation inscriptions");
            this.Controls.Add(btnValiderInscriptions);

            Button btnCreerFormation = CreerBoutonAction("➕ Créer une formation", 600, 320);
            btnCreerFormation.Click += (s, e) => MessageBox.Show("Redirection vers création formation");
            this.Controls.Add(btnCreerFormation);

            // ========== LISTE DES DERNIÈRES ACTIVITÉS ==========
            Label lblActivites = new Label
            {
                Text = "Activités récentes",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                Location = new Point(40, 420),
                AutoSize = true
            };
            this.Controls.Add(lblActivites);

            Panel panelActivites = new Panel
            {
                Location = new Point(40, 470),
                Size = new Size(1000, 200),
                BackColor = Color.FromArgb(28, 28, 42),
                AutoScroll = true
            };

            // Ajouter quelques activités exemple
            string[] activites = {
                "📝 Nouvel utilisateur inscrit : Jean Dupont",
                "✅ Formation 'Python Avancé' validée",
                "👤 Nouveau formateur ajouté : Marie Martin",
                "📧 10 nouvelles inscriptions en attente",
                "🎓 Formation 'C# Basics' terminée"
            };

            int yPos = 10;
            foreach (var activite in activites)
            {
                Label lblActivite = new Label
                {
                    Text = activite,
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 11),
                    Location = new Point(20, yPos),
                    AutoSize = true
                };
                panelActivites.Controls.Add(lblActivite);
                yPos += 35;
            }

            this.Controls.Add(panelActivites);
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
                Font = new Font("Segoe UI", 32),
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
                Location = new Point(15, 60),
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
                Size = new Size(250, 60),
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
    }
}
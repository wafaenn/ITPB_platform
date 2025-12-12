using System;
using System.Drawing;
using System.Windows.Forms;

namespace ITBS_Platform.Controls.Formateur
{
    public partial class FormateurDashboard : UserControl
    {
        public FormateurDashboard()
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
                Text = "Tableau de bord Formateur",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 28, FontStyle.Bold),
                Location = new Point(240, 30),
                AutoSize = true
            };
            this.Controls.Add(titre);

            // ========== STATISTIQUES (3 Cartes) ==========
            int cardY = 100;
            int cardSpacing = 20;
            int cardWidth = 280;
            int cardHeight = 140;

            // Carte 1 : Mes Formations
            Panel card1 = CreerCarte("Mes Formations", "5", "📚", Color.FromArgb(60, 60, 60), 250, cardY, cardWidth, cardHeight);
            this.Controls.Add(card1);

            // Carte 2 : Étudiants inscrits
            Panel card2 = CreerCarte("Étudiants", "42", "👨‍🎓", Color.FromArgb(100, 98, 25), 340 + cardWidth + cardSpacing, cardY, cardWidth, cardHeight);
            this.Controls.Add(card2);

            // Carte 3 : Formations actives
            Panel card3 = CreerCarte("En cours", "3", "🎯", Color.FromArgb(255, 149, 0), 440 + (cardWidth + cardSpacing) * 2, cardY, cardWidth, cardHeight);
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
            Button btnCreerFormation = CreerBoutonAction("➕ Créer une nouvelle formation", 240, 320);
            btnCreerFormation.Click += (s, e) => NaviguerVers(new CreerFormation());
            this.Controls.Add(btnCreerFormation);

            Button btnVoirEtudiants = CreerBoutonAction("👥 Voir mes étudiants", 580, 320);
            btnVoirEtudiants.Click += (s, e) => NaviguerVers(new VoirEtudiants());
            this.Controls.Add(btnVoirEtudiants);

            Button btnModifierFormation = CreerBoutonAction("✏️ Modifier une formation", 920, 320);
            btnModifierFormation.Click += (s, e) => NaviguerVers(new ModifierFormation());
            this.Controls.Add(btnModifierFormation);

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

            // Liste des formations (exemple)
            Panel panelFormations = new Panel
            {
                Location = new Point(300, 470),
                Size = new Size(1400, 250),
                BackColor = Color.FromArgb(28, 28, 42),
                AutoScroll = true
            };

            string[][] formations = {
                new[] { "Python Avancé", "15 étudiants", "En cours" },
                new[] { "C# pour débutants", "22 étudiants", "En cours" },
                new[] { "JavaScript ES6", "18 étudiants", "Terminée" },
                new[] { "React & Redux", "12 étudiants", "En attente" },
                new[] { "SQL & Bases de données", "20 étudiants", "En cours" }
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
                Font = new Font("Segoe UI", 16),
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
        private void NaviguerVers(UserControl nouvelleVue)
        {
            // Trouver le MainForm parent
            MainForm? mainForm = this.FindForm() as MainForm;

            if (mainForm != null)
            {
                mainForm.NaviguerVers(nouvelleVue);
            }
            else
            {
                MessageBox.Show("Erreur : Impossible de trouver le formulaire principal", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private Panel CreerCarteFormation(string titre, string etudiants, string statut, int x, int y)
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

            Label lblEtudiants = new Label
            {
                Text = etudiants,
                ForeColor = Color.FromArgb(200, 200, 200),
                Font = new Font("Segoe UI", 10),
                Location = new Point(400, 17),
                AutoSize = true
            };
            card.Controls.Add(lblEtudiants);

            Color statutColor = statut == "En cours" ? Color.FromArgb(52, 199, 89) :
                               statut == "Terminée" ? Color.Gray :
                               Color.FromArgb(255, 149, 0);

            Label lblStatut = new Label
            {
                Text = statut,
                ForeColor = Color.White,
                BackColor = statutColor,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Location = new Point(650, 12),
                Size = new Size(100, 25),
                TextAlign = ContentAlignment.MiddleCenter
            };
            card.Controls.Add(lblStatut);

            Button btnVoir = new Button
            {
                Text = "Voir",
                Location = new Point(800, 10),
                Size = new Size(70, 30),
                BackColor = Color.FromArgb(100, 88, 255),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnVoir.FlatAppearance.BorderSize = 0;
            btnVoir.Click += (s, e) => MessageBox.Show($"Voir détails de : {titre}");
            card.Controls.Add(btnVoir);

            return card;
        }
    }
}
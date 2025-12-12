using System;
using System.Drawing;
using System.Windows.Forms;

namespace ITBS_Platform.Controls.Formateur
{
    public partial class VoirEtudiants : UserControl
    {
        public VoirEtudiants()
        {
            InitializeComponent();
            ConfigurerInterface();
        }

        private void ConfigurerInterface()
        {
            this.BackColor = Color.FromArgb(35, 35, 50);
            this.Dock = DockStyle.Fill;
            this.AutoScroll = true;

            // Bouton retour
            Button btnRetour = new Button
            {
                Text = "← Retour",
                Location = new Point(250, 30),
                Size = new Size(120, 40),
                BackColor = Color.FromArgb(60, 60, 75),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnRetour.FlatAppearance.BorderSize = 0;
            btnRetour.Click += (s, e) =>
            {
                MainForm? mainForm = this.FindForm() as MainForm;
                if (mainForm != null)
                {
                    mainForm.NaviguerVers(new FormateurDashboard());
                }
            };
            this.Controls.Add(btnRetour);

            // Titre
            Label titre = new Label
            {
                Text = "Mes Étudiants",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 28, FontStyle.Bold),
                Location = new Point(250, 90),
                AutoSize = true
            };
            this.Controls.Add(titre);

            // Statistiques rapides
            int cardY = 160;
            Panel cardTotal = CreerCarteStatistique("Total Étudiants", "42", Color.FromArgb(100, 88, 255), 260, cardY);
            this.Controls.Add(cardTotal);

            Panel cardActifs = CreerCarteStatistique("Actifs", "38", Color.FromArgb(52, 199, 89), 580, cardY);
            this.Controls.Add(cardActifs);

            Panel cardInactifs = CreerCarteStatistique("Inactifs", "4", Color.Gray, 900, cardY);
            this.Controls.Add(cardInactifs);

            // Barre de recherche
            Label lblRecherche = new Label
            {
                Text = "Rechercher un étudiant",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(250, 330),
                AutoSize = true
            };
            this.Controls.Add(lblRecherche);

            TextBox txtRecherche = new TextBox
            {
                Location = new Point(260, 365),
                Size = new Size(400, 35),
                BackColor = Color.FromArgb(60, 60, 75),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11),
                BorderStyle = BorderStyle.FixedSingle,
                Text = "🔍 Rechercher par nom, email..."
            };
            txtRecherche.GotFocus += (s, e) =>
            {
                if (txtRecherche.Text.StartsWith("🔍"))
                    txtRecherche.Text = "";
            };
            this.Controls.Add(txtRecherche);

            // Filtre par formation
            ComboBox cmbFiltre = new ComboBox
            {
                Location = new Point(680, 365),
                Size = new Size(250, 35),
                BackColor = Color.FromArgb(60, 60, 75),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbFiltre.Items.AddRange(new object[] {
                "Toutes les formations",
                "Python Avancé",
                "C# pour débutants",
                "JavaScript ES6",
                "React & Redux",
                "SQL & Bases de données"
            });
            cmbFiltre.SelectedIndex = 0;
            this.Controls.Add(cmbFiltre);

            // Liste des étudiants
            Label lblListe = new Label
            {
                Text = "Liste des étudiants",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                Location = new Point(340, 430),
                AutoSize = true
            };
            this.Controls.Add(lblListe);

            // Panel contenant la liste
            Panel panelEtudiants = new Panel
            {
                Location = new Point(40, 480),
                Size = new Size(1100, 350),
                BackColor = Color.FromArgb(28, 28, 42),
                AutoScroll = true
            };

            // Données d'exemple
            string[][] etudiants = {
                new[] { "Ahmed Ben Ali", "ahmed.benali@email.com", "Python Avancé", "Actif", "85%" },
                new[] { "Fatma Gharbi", "fatma.gharbi@email.com", "C# pour débutants", "Actif", "92%" },
                new[] { "Mohamed Trabelsi", "mohamed.t@email.com", "JavaScript ES6", "Actif", "78%" },
                new[] { "Salma Khaled", "salma.k@email.com", "React & Redux", "Inactif", "45%" },
                new[] { "Youssef Mansour", "youssef.m@email.com", "Python Avancé", "Actif", "88%" },
                new[] { "Amira Zouari", "amira.z@email.com", "SQL & Bases de données", "Actif", "95%" },
                new[] { "Karim Abdelli", "karim.a@email.com", "C# pour débutants", "Actif", "70%" },
                new[] { "Nadia Hamdi", "nadia.h@email.com", "JavaScript ES6", "Inactif", "30%" }
            };

            int yPos = 15;
            foreach (var etudiant in etudiants)
            {
                Panel etudiantCard = CreerCarteEtudiant(
                    etudiant[0],
                    etudiant[1],
                    etudiant[2],
                    etudiant[3],
                    etudiant[4],
                    15,
                    yPos
                );
                panelEtudiants.Controls.Add(etudiantCard);
                yPos += 85;
            }

            this.Controls.Add(panelEtudiants);
        }

        private Panel CreerCarteStatistique(string titre, string valeur, Color couleur, int x, int y)
        {
            Panel carte = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(220, 130),
                BackColor = couleur
            };

            Label lblValeur = new Label
            {
                Text = valeur,
                Font = new Font("Segoe UI", 36, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 25),
                AutoSize = true
            };
            carte.Controls.Add(lblValeur);

            Label lblTitre = new Label
            {
                Text = titre,
                Font = new Font("Segoe UI", 14),
                ForeColor = Color.FromArgb(230, 230, 230),
                Location = new Point(20, 85),
                AutoSize = true
            };
            carte.Controls.Add(lblTitre);

            return carte;
        }

        private Panel CreerCarteEtudiant(string nom, string email, string formation, string statut, string progression, int x, int y)
        {
            Panel card = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(1060, 70),
                BackColor = Color.FromArgb(45, 45, 60)
            };

            // Nom
            Label lblNom = new Label
            {
                Text = nom,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(20, 12),
                AutoSize = true
            };
            card.Controls.Add(lblNom);

            // Email
            Label lblEmail = new Label
            {
                Text = email,
                ForeColor = Color.FromArgb(180, 180, 180),
                Font = new Font("Segoe UI", 9),
                Location = new Point(20, 38),
                AutoSize = true
            };
            card.Controls.Add(lblEmail);

            // Formation
            Label lblFormation = new Label
            {
                Text = formation,
                ForeColor = Color.FromArgb(200, 200, 200),
                Font = new Font("Segoe UI", 10),
                Location = new Point(350, 25),
                AutoSize = true
            };
            card.Controls.Add(lblFormation);

            // Statut
            Color statutColor = statut == "Actif" ? Color.FromArgb(52, 199, 89) : Color.Gray;
            Label lblStatut = new Label
            {
                Text = statut,
                ForeColor = Color.White,
                BackColor = statutColor,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Location = new Point(620, 22),
                Size = new Size(80, 25),
                TextAlign = ContentAlignment.MiddleCenter
            };
            card.Controls.Add(lblStatut);

            // Progression
            Label lblProgression = new Label
            {
                Text = $"Progression: {progression}",
                ForeColor = Color.FromArgb(200, 200, 200),
                Font = new Font("Segoe UI", 10),
                Location = new Point(730, 25),
                AutoSize = true
            };
            card.Controls.Add(lblProgression);

            // Bouton Détails
            Button btnDetails = new Button
            {
                Text = "Détails",
                Location = new Point(920, 20),
                Size = new Size(100, 30),
                BackColor = Color.FromArgb(100, 88, 255),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnDetails.FlatAppearance.BorderSize = 0;
            btnDetails.Click += (s, e) => MessageBox.Show($"Détails de l'étudiant : {nom}");
            card.Controls.Add(btnDetails);

            return card;
        }
    }
}
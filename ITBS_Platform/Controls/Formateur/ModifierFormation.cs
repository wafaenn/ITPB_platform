using System;
using System.Drawing;
using System.Windows.Forms;

namespace ITBS_Platform.Controls.Formateur
{
    public partial class ModifierFormation : UserControl
    {
        public ModifierFormation()
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
                Text = "Modifier une formation",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 28, FontStyle.Bold),
                Location = new Point(250, 90),
                AutoSize = true
            };
            this.Controls.Add(titre);

            // Section 1 : Sélection de la formation
            Label lblSelection = new Label
            {
                Text = "Sélectionner une formation à modifier",
                ForeColor = Color.Lavender,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(260, 160),
                AutoSize = true
            };
            this.Controls.Add(lblSelection);

            ComboBox cmbFormations = new ComboBox
            {
                Location = new Point(260, 200),
                Size = new Size(500, 40),
                BackColor = Color.FromArgb(60, 60, 75),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Name = "cmbFormations"
            };
            cmbFormations.Items.AddRange(new object[] {
                "Python Avancé",
                "C# pour débutants",
                "JavaScript ES6",
                "React & Redux",
                "SQL & Bases de données"
            });
            cmbFormations.SelectedIndexChanged += (s, e) =>
            {
                if (cmbFormations.SelectedIndex >= 0)
                {
                    ChargerDonneesFormation(cmbFormations.SelectedItem.ToString());
                }
            };
            this.Controls.Add(cmbFormations);

            // Panel du formulaire de modification
            Panel panelForm = new Panel
            {
                Location = new Point(250, 270),
                Size = new Size(900, 550),
                BackColor = Color.FromArgb(45, 45, 60),
                AutoScroll = true,
                Name = "panelForm",
                Visible = false
            };

            int yPos = 30;
            int labelX = 30;
            int inputX = 30;
            int inputWidth = 840;

            // Titre de la formation
            Label lblTitre = CreerLabel("Titre de la formation *", labelX, yPos);
            panelForm.Controls.Add(lblTitre);
            yPos += 30;

            TextBox txtTitre = CreerTextBox(inputX, yPos, inputWidth);
            txtTitre.Name = "txtTitre";
            panelForm.Controls.Add(txtTitre);
            yPos += 50;

            // Description
            Label lblDescription = CreerLabel("Description *", labelX, yPos);
            panelForm.Controls.Add(lblDescription);
            yPos += 30;

            TextBox txtDescription = CreerTextBox(inputX, yPos, inputWidth, 100, true);
            txtDescription.Name = "txtDescription";
            panelForm.Controls.Add(txtDescription);
            yPos += 120;

            // Catégorie
            Label lblCategorie = CreerLabel("Catégorie *", labelX, yPos);
            panelForm.Controls.Add(lblCategorie);
            yPos += 30;

            ComboBox cmbCategorie = CreerComboBox(inputX, yPos, inputWidth);
            cmbCategorie.Items.AddRange(new object[] {
                "Programmation",
                "Web Development",
                "Data Science",
                "Design",
                "Marketing"
            });
            cmbCategorie.Name = "cmbCategorie";
            panelForm.Controls.Add(cmbCategorie);
            yPos += 50;

            // Niveau
            Label lblNiveau = CreerLabel("Niveau *", labelX, yPos);
            panelForm.Controls.Add(lblNiveau);
            yPos += 30;

            ComboBox cmbNiveau = CreerComboBox(inputX, yPos, inputWidth);
            cmbNiveau.Items.AddRange(new object[] {
                "Débutant",
                "Intermédiaire",
                "Avancé"
            });
            cmbNiveau.Name = "cmbNiveau";
            panelForm.Controls.Add(cmbNiveau);
            yPos += 50;

            // Durée
            Label lblDuree = CreerLabel("Durée (en heures) *", labelX, yPos);
            panelForm.Controls.Add(lblDuree);
            yPos += 30;

            NumericUpDown numDuree = new NumericUpDown
            {
                Location = new Point(inputX, yPos),
                Size = new Size(200, 35),
                BackColor = Color.FromArgb(60, 60, 75),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11),
                Minimum = 1,
                Maximum = 500,
                Value = 10,
                Name = "numDuree"
            };
            panelForm.Controls.Add(numDuree);
            yPos += 50;

            // Prix
            Label lblPrix = CreerLabel("Prix (TND) *", labelX, yPos);
            panelForm.Controls.Add(lblPrix);
            yPos += 30;

            NumericUpDown numPrix = new NumericUpDown
            {
                Location = new Point(inputX, yPos),
                Size = new Size(200, 35),
                BackColor = Color.FromArgb(60, 60, 75),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11),
                Minimum = 0,
                Maximum = 10000,
                Value = 0,
                DecimalPlaces = 2,
                Name = "numPrix"
            };
            panelForm.Controls.Add(numPrix);
            yPos += 70;

            // Boutons
            Button btnSupprimer = new Button
            {
                Text = "🗑️ Supprimer",
                Location = new Point(inputX, yPos),
                Size = new Size(150, 45),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnSupprimer.FlatAppearance.BorderSize = 0;
            btnSupprimer.Click += BtnSupprimer_Click;
            panelForm.Controls.Add(btnSupprimer);

            Button btnAnnuler = new Button
            {
                Text = "Annuler",
                Location = new Point(inputX + 170, yPos),
                Size = new Size(150, 45),
                BackColor = Color.FromArgb(60, 60, 75),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnAnnuler.FlatAppearance.BorderSize = 0;
            btnAnnuler.Click += (s, e) =>
            {
                this.Parent.Controls.Clear();
                this.Parent.Controls.Add(new FormateurDashboard());
            };
            panelForm.Controls.Add(btnAnnuler);

            Button btnModifier = new Button
            {
                Text = "💾 Enregistrer",
                Location = new Point(inputX + 340, yPos),
                Size = new Size(200, 45),
                BackColor = Color.FromArgb(52, 199, 89),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnModifier.FlatAppearance.BorderSize = 0;
            btnModifier.Click += BtnModifier_Click;
            panelForm.Controls.Add(btnModifier);

            this.Controls.Add(panelForm);
        }

        private void ChargerDonneesFormation(string formationNom)
        {
            Panel panelForm = this.Controls["panelForm"] as Panel;
            panelForm.Visible = true;

            // Données d'exemple selon la formation sélectionnée
            TextBox txtTitre = panelForm.Controls["txtTitre"] as TextBox;
            TextBox txtDescription = panelForm.Controls["txtDescription"] as TextBox;
            ComboBox cmbCategorie = panelForm.Controls["cmbCategorie"] as ComboBox;
            ComboBox cmbNiveau = panelForm.Controls["cmbNiveau"] as ComboBox;
            NumericUpDown numDuree = panelForm.Controls["numDuree"] as NumericUpDown;
            NumericUpDown numPrix = panelForm.Controls["numPrix"] as NumericUpDown;

            // Exemple de chargement des données
            txtTitre.Text = formationNom;
            txtDescription.Text = $"Description complète de la formation {formationNom}. Cette formation couvre tous les aspects essentiels...";
            cmbCategorie.SelectedIndex = 0;
            cmbNiveau.SelectedIndex = 1;
            numDuree.Value = 40;
            numPrix.Value = 299.99m;
        }

        private void BtnModifier_Click(object sender, EventArgs e)
        {
            Panel panelForm = this.Controls["panelForm"] as Panel;
            TextBox txtTitre = panelForm.Controls["txtTitre"] as TextBox;
            ComboBox cmbFormations = this.Controls["cmbFormations"] as ComboBox;

            if (string.IsNullOrWhiteSpace(txtTitre.Text))
            {
                MessageBox.Show("Veuillez saisir le titre de la formation.", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Ici, ajoutez le code pour sauvegarder dans la base de données
            MessageBox.Show($"Formation '{txtTitre.Text}' modifiée avec succès !", "Succès",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Parent.Controls.Clear();
            this.Parent.Controls.Add(new FormateurDashboard());
        }

        private void BtnSupprimer_Click(object sender, EventArgs e)
        {
            ComboBox cmbFormations = this.Controls["cmbFormations"] as ComboBox;

            if (cmbFormations.SelectedIndex < 0)
                return;

            DialogResult result = MessageBox.Show(
                $"Êtes-vous sûr de vouloir supprimer la formation '{cmbFormations.SelectedItem}' ?\n\nCette action est irréversible.",
                "Confirmation de suppression",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                // Ici, ajoutez le code pour supprimer de la base de données
                MessageBox.Show($"Formation '{cmbFormations.SelectedItem}' supprimée avec succès.", "Succès",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Parent.Controls.Clear();
                this.Parent.Controls.Add(new FormateurDashboard());
            }
        }

        private Label CreerLabel(string texte, int x, int y)
        {
            return new Label
            {
                Text = texte,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(x, y),
                AutoSize = true
            };
        }

        private TextBox CreerTextBox(int x, int y, int width, int height = 35, bool multiline = false)
        {
            return new TextBox
            {
                Location = new Point(x, y),
                Size = new Size(width, height),
                BackColor = Color.FromArgb(60, 60, 75),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11),
                BorderStyle = BorderStyle.None,
                Multiline = multiline,
                ScrollBars = multiline ? ScrollBars.Vertical : ScrollBars.None
            };
        }

        private ComboBox CreerComboBox(int x, int y, int width)
        {
            return new ComboBox
            {
                Location = new Point(x, y),
                Size = new Size(width, 35),
                BackColor = Color.FromArgb(60, 60, 75),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11),
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat
            };
        }
    }
}
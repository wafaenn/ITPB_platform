using System;
using System.Drawing;
using System.Windows.Forms;

namespace ITBS_Platform.Controls.Formateur
{
    public partial class CreerFormation : UserControl
    {
        public CreerFormation()
        {
            InitializeComponent();
            ConfigurerInterface();
        }

        private void ConfigurerInterface()
        {
            this.BackColor = Color.FromArgb(35, 35, 50);
            this.Dock = DockStyle.Fill;
            this.AutoScroll = true;

            // Titre
            Label titre = new Label
            {
                Text = "Créer une nouvelle formation",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 28, FontStyle.Bold),
                Location = new Point(250, 30),
                AutoSize = true
            };
            this.Controls.Add(titre);

            // Panel principal du formulaire
            Panel panelForm = new Panel
            {
                Location = new Point(240, 100),
                Size = new Size(900, 600),
                BackColor = Color.FromArgb(45, 45, 60),
                AutoScroll = true
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

            // Durée (en heures)
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
            Button btnAnnuler = new Button
            {
                Text = "Annuler",
                Location = new Point(inputX, yPos),
                Size = new Size(150, 45),
                BackColor = Color.FromArgb(60, 60, 75),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnAnnuler.FlatAppearance.BorderSize = 0;
            btnAnnuler.Click += (s, e) =>
            {
                if (MessageBox.Show("Voulez-vous vraiment annuler ?", "Confirmation",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.Parent.Controls.Clear();
                    this.Parent.Controls.Add(new FormateurDashboard());
                }
            };
            panelForm.Controls.Add(btnAnnuler);

            Button btnCreer = new Button
            {
                Text = "Créer la formation",
                Location = new Point(inputX + 170, yPos),
                Size = new Size(200, 45),
                BackColor = Color.FromArgb(52, 199, 89),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnCreer.FlatAppearance.BorderSize = 0;
            btnCreer.Click += BtnCreer_Click;
            panelForm.Controls.Add(btnCreer);

            this.Controls.Add(panelForm);
        }

        private void BtnCreer_Click(object sender, EventArgs e)
        {
            // Récupérer les contrôles
            Panel panelForm = this.Controls[1] as Panel;
            TextBox txtTitre = panelForm.Controls["txtTitre"] as TextBox;
            TextBox txtDescription = panelForm.Controls["txtDescription"] as TextBox;
            ComboBox cmbCategorie = panelForm.Controls["cmbCategorie"] as ComboBox;
            ComboBox cmbNiveau = panelForm.Controls["cmbNiveau"] as ComboBox;
            NumericUpDown numDuree = panelForm.Controls["numDuree"] as NumericUpDown;
            NumericUpDown numPrix = panelForm.Controls["numPrix"] as NumericUpDown;

            // Validation
            if (string.IsNullOrWhiteSpace(txtTitre.Text))
            {
                MessageBox.Show("Veuillez saisir le titre de la formation.", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Veuillez saisir la description.", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbCategorie.SelectedIndex == -1)
            {
                MessageBox.Show("Veuillez sélectionner une catégorie.", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbNiveau.SelectedIndex == -1)
            {
                MessageBox.Show("Veuillez sélectionner un niveau.", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Ici, vous ajouterez le code pour enregistrer dans la base de données
            MessageBox.Show($"Formation '{txtTitre.Text}' créée avec succès !", "Succès",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Retour au dashboard
            MainForm? mainForm = this.FindForm() as MainForm;
            if (mainForm != null)
            {
                mainForm.NaviguerVers(new FormateurDashboard());
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
            ComboBox cmb = new ComboBox
            {
                Location = new Point(x, y),
                Size = new Size(width, 35),
                BackColor = Color.FromArgb(60, 60, 75),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11),
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat
            };
            return cmb;
        }
    }
}
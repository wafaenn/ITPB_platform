namespace ITBS_Platform
{
    partial class SignupForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            labelFullName = new Label();
            labelPassword = new Label();
            labelEmail = new Label();
            labelConfirm = new Label();
            labelRole = new Label(); // ← NOUVEAU
            btnRegister = new Button();
            txtFullName = new TextBox();
            txtEmail = new TextBox();
            txtPassword = new TextBox();
            txtConfirmPassword = new TextBox();
            cmbRole = new ComboBox(); // ← NOUVEAU
            SuspendLayout();
            // 
            // labelFullName
            // 
            labelFullName.AutoSize = true;
            labelFullName.Location = new Point(39, 68);
            labelFullName.Name = "labelFullName";
            labelFullName.Size = new Size(108, 20);
            labelFullName.Text = "Nom complet :";
            // 
            // labelEmail
            // 
            labelEmail.AutoSize = true;
            labelEmail.Location = new Point(39, 132);
            labelEmail.Text = "Email :";
            // 
            // labelPassword
            // 
            labelPassword.AutoSize = true;
            labelPassword.Location = new Point(39, 198);
            labelPassword.Text = "Mot de passe :";
            // 
            // labelConfirm
            // 
            labelConfirm.AutoSize = true;
            labelConfirm.Location = new Point(39, 273);
            labelConfirm.Text = "Confirmer :";
            // 
            // labelRole ← NOUVEAU
            // 
            labelRole.AutoSize = true;
            labelRole.Location = new Point(39, 338);
            labelRole.Text = "Rôle :";
            // 
            // txtFullName
            // 
            txtFullName.Location = new Point(232, 68);
            txtFullName.Size = new Size(304, 27);
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(232, 125);
            txtEmail.Size = new Size(304, 27);
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(232, 198);
            txtPassword.Size = new Size(304, 27);
            txtPassword.PasswordChar = '*';
            // 
            // txtConfirmPassword
            // 
            txtConfirmPassword.Location = new Point(232, 266);
            txtConfirmPassword.Size = new Size(304, 27);
            txtConfirmPassword.PasswordChar = '*';
            // 
            // cmbRole ← NOUVEAU
            // 
            cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRole.FormattingEnabled = true;
            cmbRole.Location = new Point(232, 335);
            cmbRole.Name = "cmbRole";
            cmbRole.Size = new Size(304, 28);
            cmbRole.TabIndex = 9;
            // 
            // btnRegister
            // 
            btnRegister.Location = new Point(257, 410);
            btnRegister.Size = new Size(224, 50);
            btnRegister.Text = "Créer un compte";
            btnRegister.Click += button1_Click;
            // 
            // SignupForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            ClientSize = new Size(800, 520);
            Controls.Add(labelFullName);
            Controls.Add(labelEmail);
            Controls.Add(labelPassword);
            Controls.Add(labelConfirm);
            Controls.Add(labelRole); // ← NOUVEAU
            Controls.Add(txtFullName);
            Controls.Add(txtEmail);
            Controls.Add(txtPassword);
            Controls.Add(txtConfirmPassword);
            Controls.Add(cmbRole); // ← NOUVEAU
            Controls.Add(btnRegister);
            Name = "SignupForm";
            Text = "Création de compte";
            Load += SignupForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private Label labelFullName;
        private Label labelPassword;
        private Label labelEmail;
        private Label labelConfirm;
        private Label labelRole; // ← NOUVEAU
        private TextBox txtFullName;
        private TextBox txtEmail;
        private TextBox txtPassword;
        private TextBox txtConfirmPassword;
        private ComboBox cmbRole; // ← NOUVEAU
        private Button btnRegister;

        #endregion
    }
}
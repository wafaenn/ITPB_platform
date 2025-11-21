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
            btnRegister = new Button();

            txtFullName = new TextBox();
            txtEmail = new TextBox();
            txtPassword = new TextBox();
            txtConfirmPassword = new TextBox();

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
            // labelPassword
            // 
            labelPassword.AutoSize = true;
            labelPassword.Location = new Point(39, 198);
            labelPassword.Text = "Mot de passe :";
            // 
            // labelEmail
            // 
            labelEmail.AutoSize = true;
            labelEmail.Location = new Point(39, 132);
            labelEmail.Text = "Email :";
            // 
            // labelConfirm
            // 
            labelConfirm.AutoSize = true;
            labelConfirm.Location = new Point(39, 273);
            labelConfirm.Text = "Confirmer :";
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
            // btnRegister
            // 
            btnRegister.Location = new Point(257, 351);
            btnRegister.Size = new Size(224, 50);
            btnRegister.Text = "Créer un compte";
            btnRegister.Click += button1_Click;
            // 
            // SignupForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            ClientSize = new Size(800, 450);

            Controls.Add(labelFullName);
            Controls.Add(labelEmail);
            Controls.Add(labelPassword);
            Controls.Add(labelConfirm);
            Controls.Add(txtFullName);
            Controls.Add(txtEmail);
            Controls.Add(txtPassword);
            Controls.Add(txtConfirmPassword);
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

        private TextBox txtFullName;
        private TextBox txtEmail;
        private TextBox txtPassword;
        private TextBox txtConfirmPassword;

        private Button btnRegister;

        #endregion
    }
}

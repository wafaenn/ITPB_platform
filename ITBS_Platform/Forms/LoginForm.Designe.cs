namespace ITBS_Platform
{
    partial class LoginForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer? components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            login = new Button();
            label1 = new Label();
            addrs = new Label();
            mddps = new Label();
            emailTxt = new TextBox();
            passwordTxt = new TextBox();
            btnSignup = new LinkLabel();
            SuspendLayout();
            // 
            // login
            // 
            login.Location = new Point(298, 275);
            login.Name = "login";
            login.Size = new Size(193, 66);
            login.TabIndex = 0;
            login.Text = "login ";
            login.UseVisualStyleBackColor = true;
            login.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(95, 133);
            label1.Name = "label1";
            label1.Size = new Size(0, 20);
            label1.TabIndex = 1;
            // 
            // addrs
            // 
            addrs.AutoSize = true;
            addrs.Location = new Point(90, 117);
            addrs.Name = "addrs";
            addrs.Size = new Size(75, 20);
            addrs.TabIndex = 2;
            addrs.Text = "addresse :";
            // 
            // mddps
            // 
            mddps.AutoSize = true;
            mddps.Location = new Point(90, 175);
            mddps.Name = "mddps";
            mddps.Size = new Size(109, 20);
            mddps.TabIndex = 3;
            mddps.Text = "mot de passe  :";
            // 
            // emailTxt
            // 
            emailTxt.Location = new Point(298, 114);
            emailTxt.Name = "emailTxt";
            emailTxt.Size = new Size(304, 27);
            emailTxt.TabIndex = 4;
            // 
            // passwordTxt
            // 
            passwordTxt.Location = new Point(298, 175);
            passwordTxt.Name = "passwordTxt";
            passwordTxt.Size = new Size(304, 27);
            passwordTxt.TabIndex = 5;
            // 
            // btnSignup
            // 
            btnSignup.AutoSize = true;
            btnSignup.Location = new Point(592, 382);
            btnSignup.Name = "btnSignup";
            btnSignup.Size = new Size(119, 20);
            btnSignup.TabIndex = 6;
            btnSignup.TabStop = true;
            btnSignup.Text = "Créer un compte";
            btnSignup.LinkClicked += linkLabel1_LinkClicked;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnSignup);
            Controls.Add(passwordTxt);
            Controls.Add(emailTxt);
            Controls.Add(mddps);
            Controls.Add(addrs);
            Controls.Add(label1);
            Controls.Add(login);
            Name = "LoginForm";
            Text = "LoginForm";
            Load += Login_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        // Déclarations corrigées pour CS8618 avec l'opérateur 'null!'
        private Button login = null!;
        private Label label1 = null!;
        private Label addrs = null!;
        private Label mddps = null!;
        private TextBox emailTxt = null!;
        private TextBox passwordTxt = null!;
        private LinkLabel btnSignup = null!;
    }
}
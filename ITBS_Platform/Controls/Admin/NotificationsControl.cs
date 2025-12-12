using System;
using System.Drawing;
using System.Windows.Forms;

namespace ITBS_Platform.Controls.Admin
{
    public partial class NotificationsControl : UserControl
    {
        public NotificationsControl()
        {
            InitializeComponent();
            ConfigurerInterface();
        }

        private void ConfigurerInterface()
        {
            this.BackColor = Color.FromArgb(18, 18, 18);
            this.Dock = DockStyle.Fill;
            this.AutoScroll = true;

            // ========== TITRE ==========
            Label titre = new Label
            {
                Text = "Notifications",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 30, FontStyle.Regular),
                Location = new Point(255, 20),
                AutoSize = true
            };
            this.Controls.Add(titre);

            // ========== LISTE DES NOTIFICATIONS ==========
            string[] notifications = {
                "Nouvelle demande de formation : SDGHJ",
                "Nouvelle demande de formation : test de demande",
               "Nouvelle demande de formation : aya",
                "Nouvelle demande de formation : PYTHON",
                "Nouvelle demande de formation : NOM TEST",
                "Nouvelle demande de formation : TEST TEST"
            };

            int yPos = 100;
            foreach (var notif in notifications)
            {
                Panel notifPanel = CreerNotification(notif, yPos);
                this.Controls.Add(notifPanel);
                yPos += 90;
            }
        }

        private Panel CreerNotification(string message, int yPosition)
        {
            Panel notifPanel = new Panel
            {
                Location = new Point(240, yPosition),
                Size = new Size(1300, 70),
                BackColor = Color.FromArgb(25, 35, 45)
            };

            // Icône info bleue
            Label iconInfo = new Label
            {
                Text = "ℹ",
                ForeColor = Color.FromArgb(70, 130, 220),
                Font = new Font("Segoe UI", 28),
                Location = new Point(20, 15),
                Size = new Size(40, 40)
            };
            notifPanel.Controls.Add(iconInfo);

            // Titre de la notification
            Label lblTitle = new Label
            {
                Text = "Nouvelle demande de formation",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(80, 10),
                AutoSize = true
            };
            notifPanel.Controls.Add(lblTitle);

            // Message de la notification
            Label lblMessage = new Label
            {
                Text = message,
                ForeColor = Color.FromArgb(150, 200, 250),
                Font = new Font("Segoe UI", 10),
                Location = new Point(80, 35),
                AutoSize = true
            };
            notifPanel.Controls.Add(lblMessage);

            // Bouton fermer (X)
            Label btnClose = new Label
            {
                Text = "✖",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14),
                Location = new Point(1260, 25),
                AutoSize = true,
                Cursor = Cursors.Hand
            };
            btnClose.Click += (s, e) => notifPanel.Visible = false;
            notifPanel.Controls.Add(btnClose);

            // Effet hover sur le bouton X
            btnClose.MouseEnter += (s, e) => btnClose.ForeColor = Color.Red;
            btnClose.MouseLeave += (s, e) => btnClose.ForeColor = Color.White;

            return notifPanel;
        }
    }
}
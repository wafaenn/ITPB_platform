namespace ITBS_Platform
{
    partial class MainForm
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

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "ITBS Platform";
            this.ResumeLayout(false);
        }
    }
}
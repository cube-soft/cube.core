namespace Cube.Forms.Demo
{
    partial class DemoVersion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DemoVersion));
            this.VersionView = new Cube.Forms.VersionView();
            this.SuspendLayout();
            // 
            // VersionView
            // 
            this.VersionView.BackColor = System.Drawing.Color.White;
            this.VersionView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VersionView.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.VersionView.Location = new System.Drawing.Point(0, 0);
            this.VersionView.Name = "VersionView";
            this.VersionView.Publisher = "CubeSoft, Inc.";
            this.VersionView.Size = new System.Drawing.Size(384, 312);
            this.VersionView.TabIndex = 0;
            this.VersionView.ProductTitle = "Cube.Forms Demo";
            this.VersionView.Version = "1.0.0";
            this.VersionView.Year = 2015;
            // 
            // DemoVersion
            // 
            this.ClientSize = new System.Drawing.Size(384, 312);
            this.Controls.Add(this.VersionView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DemoVersion";
            this.Text = "VersionForm Demo";
            this.ResumeLayout(false);

        }

        #endregion

        private VersionView VersionView;
    }
}
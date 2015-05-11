namespace Cube.Forms.Demo
{
    partial class DemoStockIcons
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
            this.IconListView = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // IconListView
            // 
            this.IconListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.IconListView.Location = new System.Drawing.Point(0, 26);
            this.IconListView.Name = "IconListView";
            this.IconListView.Size = new System.Drawing.Size(640, 374);
            this.IconListView.TabIndex = 9;
            this.IconListView.UseCompatibleStateImageBehavior = false;
            // 
            // DemoStockIcons
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 400);
            this.Controls.Add(this.IconListView);
            this.Name = "DemoStockIcons";
            this.ShowInTaskbar = false;
            this.Text = "DemoStockIcons";
            this.Controls.SetChildIndex(this.IconListView, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView IconListView;
    }
}
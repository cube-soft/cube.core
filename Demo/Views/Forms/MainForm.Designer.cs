namespace Cube.Forms.Demo
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.LayoutPanel = new Cube.Forms.TableLayoutPanel();
            this.Separator = new Cube.Forms.PictureBox();
            this.ContentsControl = new Cube.Forms.FlowLayoutPanel();
            this.FooterControl = new Cube.Forms.StatusStrip();
            this.HeaderControl = new Cube.Forms.Demo.CustomCaptionControl();
            this.LayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Separator)).BeginInit();
            this.SuspendLayout();
            // 
            // LayoutPanel
            // 
            this.LayoutPanel.ColumnCount = 1;
            this.LayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LayoutPanel.Controls.Add(this.HeaderControl, 0, 0);
            this.LayoutPanel.Controls.Add(this.Separator, 0, 1);
            this.LayoutPanel.Controls.Add(this.ContentsControl, 0, 2);
            this.LayoutPanel.Controls.Add(this.FooterControl, 0, 3);
            this.LayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.LayoutPanel.Name = "LayoutPanel";
            this.LayoutPanel.RowCount = 4;
            this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.LayoutPanel.Size = new System.Drawing.Size(350, 300);
            this.LayoutPanel.TabIndex = 0;
            // 
            // Separator
            // 
            this.Separator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.Separator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Separator.Location = new System.Drawing.Point(0, 30);
            this.Separator.Margin = new System.Windows.Forms.Padding(0);
            this.Separator.Name = "Separator";
            this.Separator.Size = new System.Drawing.Size(350, 1);
            this.Separator.TabIndex = 1;
            this.Separator.TabStop = false;
            // 
            // ContentsControl
            // 
            this.ContentsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContentsControl.Location = new System.Drawing.Point(0, 31);
            this.ContentsControl.Margin = new System.Windows.Forms.Padding(0);
            this.ContentsControl.Name = "ContentsControl";
            this.ContentsControl.Size = new System.Drawing.Size(350, 244);
            this.ContentsControl.TabIndex = 2;
            // 
            // FooterControl
            // 
            this.FooterControl.Location = new System.Drawing.Point(0, 278);
            this.FooterControl.Name = "FooterControl";
            this.FooterControl.Size = new System.Drawing.Size(350, 22);
            this.FooterControl.TabIndex = 3;
            this.FooterControl.Text = "statusStrip1";
            // 
            // HeaderControl
            // 
            this.HeaderControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.HeaderControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HeaderControl.Location = new System.Drawing.Point(0, 0);
            this.HeaderControl.Margin = new System.Windows.Forms.Padding(0);
            this.HeaderControl.Name = "HeaderControl";
            this.HeaderControl.Size = new System.Drawing.Size(350, 30);
            this.HeaderControl.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(350, 300);
            this.Controls.Add(this.LayoutPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximumSize = new System.Drawing.Size(1920, 1160);
            this.Name = "MainForm";
            this.Text = "Cube.Forms.Demo";
            this.LayoutPanel.ResumeLayout(false);
            this.LayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Separator)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel LayoutPanel;
        private CustomCaptionControl HeaderControl;
        private PictureBox Separator;
        private FlowLayoutPanel ContentsControl;
        private StatusStrip FooterControl;
    }
}
namespace Cube.Forms.Demo
{
    partial class VersionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VersionForm));
            this.LayoutPanel = new Cube.Forms.TableLayoutPanel();
            this.HeaderCaptionControl = new Cube.Forms.Demo.CustomCaptionControl();
            this.MainVersionControl = new Cube.Forms.Views.Controls.VersionControl();
            this.ExitButton = new Cube.Forms.Button();
            this.LayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // LayoutPanel
            // 
            this.LayoutPanel.ColumnCount = 1;
            this.LayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LayoutPanel.Controls.Add(this.HeaderCaptionControl, 0, 0);
            this.LayoutPanel.Controls.Add(this.MainVersionControl, 0, 1);
            this.LayoutPanel.Controls.Add(this.ExitButton, 0, 2);
            this.LayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.LayoutPanel.Name = "LayoutPanel";
            this.LayoutPanel.RowCount = 3;
            this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.LayoutPanel.Size = new System.Drawing.Size(380, 250);
            this.LayoutPanel.TabIndex = 0;
            // 
            // HeaderCaptionControl
            // 
            this.HeaderCaptionControl.Active = true;
            this.HeaderCaptionControl.CloseBox = true;
            this.HeaderCaptionControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HeaderCaptionControl.Location = new System.Drawing.Point(0, 0);
            this.HeaderCaptionControl.Margin = new System.Windows.Forms.Padding(0);
            this.HeaderCaptionControl.MaximizeBox = true;
            this.HeaderCaptionControl.MinimizeBox = true;
            this.HeaderCaptionControl.Name = "HeaderCaptionControl";
            this.HeaderCaptionControl.Size = new System.Drawing.Size(380, 30);
            this.HeaderCaptionControl.TabIndex = 0;
            this.HeaderCaptionControl.WindowState = System.Windows.Forms.FormWindowState.Normal;
            // 
            // MainVersionControl
            // 
            this.MainVersionControl.BackColor = System.Drawing.SystemColors.Window;
            this.MainVersionControl.Copyright = "Copyright © 2010 CubeSoft, Inc.";
            this.MainVersionControl.Description = "";
            this.MainVersionControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainVersionControl.Image = ((System.Drawing.Image)(resources.GetObject("MainVersionControl.Image")));
            this.MainVersionControl.Location = new System.Drawing.Point(0, 30);
            this.MainVersionControl.Margin = new System.Windows.Forms.Padding(0);
            this.MainVersionControl.Name = "MainVersionControl";
            this.MainVersionControl.Padding = new System.Windows.Forms.Padding(20);
            this.MainVersionControl.Product = "Cube.Forms";
            this.MainVersionControl.Size = new System.Drawing.Size(380, 160);
            this.MainVersionControl.TabIndex = 2;
            this.MainVersionControl.Uri = null;
            this.MainVersionControl.Version = "Version 1.3.0.0";
            // 
            // ExitButton
            // 
            this.ExitButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ExitButton.Location = new System.Drawing.Point(130, 205);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(120, 30);
            this.ExitButton.TabIndex = 3;
            this.ExitButton.Text = "OK";
            this.ExitButton.UseVisualStyleBackColor = true;
            // 
            // VersionForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(380, 250);
            this.Controls.Add(this.LayoutPanel);
            this.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(128)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VersionForm";
            this.ShowInTaskbar = false;
            this.Sizable = false;
            this.SizeGrip = 1;
            this.Text = "バージョン情報";
            this.LayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel LayoutPanel;
        private CustomCaptionControl HeaderCaptionControl;
        private Cube.Forms.Views.Controls.VersionControl MainVersionControl;
        private Button ExitButton;
    }
}
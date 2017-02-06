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
            this.DemoButton1 = new Cube.Forms.FlatButton();
            this.DemoButton2 = new Cube.Forms.FlatButton();
            this.DemoButton3 = new Cube.Forms.FlatButton();
            this.DemoButton4 = new Cube.Forms.FlatButton();
            this.VersionButton = new Cube.Forms.FlatButton();
            this.FooterControl = new Cube.Forms.StatusStrip();
            this.HeaderCaptionControl = new Cube.Forms.Demo.CustomCaptionControl();
            this.LayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Separator)).BeginInit();
            this.ContentsControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // LayoutPanel
            // 
            this.LayoutPanel.ColumnCount = 1;
            this.LayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LayoutPanel.Controls.Add(this.HeaderCaptionControl, 0, 0);
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
            this.LayoutPanel.Size = new System.Drawing.Size(350, 280);
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
            this.ContentsControl.Controls.Add(this.DemoButton1);
            this.ContentsControl.Controls.Add(this.DemoButton2);
            this.ContentsControl.Controls.Add(this.DemoButton3);
            this.ContentsControl.Controls.Add(this.DemoButton4);
            this.ContentsControl.Controls.Add(this.VersionButton);
            this.ContentsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContentsControl.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.ContentsControl.Location = new System.Drawing.Point(0, 31);
            this.ContentsControl.Margin = new System.Windows.Forms.Padding(0);
            this.ContentsControl.Name = "ContentsControl";
            this.ContentsControl.Padding = new System.Windows.Forms.Padding(20);
            this.ContentsControl.Size = new System.Drawing.Size(350, 224);
            this.ContentsControl.TabIndex = 2;
            // 
            // DemoButton1
            // 
            this.DemoButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.DemoButton1.Content = "Buttons";
            this.DemoButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DemoButton1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.DemoButton1.FlatAppearance.BorderSize = 0;
            this.DemoButton1.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.DemoButton1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.DemoButton1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.DemoButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DemoButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.DemoButton1.Image = null;
            this.DemoButton1.Location = new System.Drawing.Point(20, 20);
            this.DemoButton1.Margin = new System.Windows.Forms.Padding(0);
            this.DemoButton1.Name = "DemoButton1";
            this.DemoButton1.Size = new System.Drawing.Size(310, 30);
            this.DemoButton1.Styles.MouseDownStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.DemoButton1.Styles.MouseDownStyle.BorderSize = 1;
            this.DemoButton1.Styles.MouseOverStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.DemoButton1.Styles.MouseOverStyle.BorderSize = 1;
            this.DemoButton1.Styles.NormalStyle.BorderSize = 1;
            this.DemoButton1.TabIndex = 0;
            this.DemoButton1.UseVisualStyleBackColor = false;
            // 
            // DemoButton2
            // 
            this.DemoButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.DemoButton2.Content = "WebBrowser";
            this.DemoButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DemoButton2.Dock = System.Windows.Forms.DockStyle.Top;
            this.DemoButton2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.DemoButton2.FlatAppearance.BorderSize = 0;
            this.DemoButton2.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.DemoButton2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.DemoButton2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.DemoButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DemoButton2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.DemoButton2.Image = null;
            this.DemoButton2.Location = new System.Drawing.Point(20, 58);
            this.DemoButton2.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.DemoButton2.Name = "DemoButton2";
            this.DemoButton2.Size = new System.Drawing.Size(310, 30);
            this.DemoButton2.Styles.MouseDownStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.DemoButton2.Styles.MouseDownStyle.BorderSize = 1;
            this.DemoButton2.Styles.MouseOverStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.DemoButton2.Styles.MouseOverStyle.BorderSize = 1;
            this.DemoButton2.Styles.NormalStyle.BorderSize = 1;
            this.DemoButton2.TabIndex = 1;
            this.DemoButton2.UseVisualStyleBackColor = false;
            // 
            // DemoButton3
            // 
            this.DemoButton3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.DemoButton3.Content = "NotifyForm";
            this.DemoButton3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DemoButton3.Dock = System.Windows.Forms.DockStyle.Top;
            this.DemoButton3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.DemoButton3.FlatAppearance.BorderSize = 0;
            this.DemoButton3.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.DemoButton3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.DemoButton3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.DemoButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DemoButton3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.DemoButton3.Image = null;
            this.DemoButton3.Location = new System.Drawing.Point(20, 96);
            this.DemoButton3.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.DemoButton3.Name = "DemoButton3";
            this.DemoButton3.Size = new System.Drawing.Size(310, 30);
            this.DemoButton3.Styles.MouseDownStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.DemoButton3.Styles.MouseDownStyle.BorderSize = 1;
            this.DemoButton3.Styles.MouseOverStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.DemoButton3.Styles.MouseOverStyle.BorderSize = 1;
            this.DemoButton3.Styles.NormalStyle.BorderSize = 1;
            this.DemoButton3.TabIndex = 2;
            this.DemoButton3.UseVisualStyleBackColor = false;
            // 
            // DemoButton4
            // 
            this.DemoButton4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.DemoButton4.Content = "StockIcons";
            this.DemoButton4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DemoButton4.Dock = System.Windows.Forms.DockStyle.Top;
            this.DemoButton4.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.DemoButton4.FlatAppearance.BorderSize = 0;
            this.DemoButton4.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.DemoButton4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.DemoButton4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.DemoButton4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DemoButton4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.DemoButton4.Image = null;
            this.DemoButton4.Location = new System.Drawing.Point(20, 134);
            this.DemoButton4.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.DemoButton4.Name = "DemoButton4";
            this.DemoButton4.Size = new System.Drawing.Size(310, 30);
            this.DemoButton4.Styles.MouseDownStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.DemoButton4.Styles.MouseDownStyle.BorderSize = 1;
            this.DemoButton4.Styles.MouseOverStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.DemoButton4.Styles.MouseOverStyle.BorderSize = 1;
            this.DemoButton4.Styles.NormalStyle.BorderSize = 1;
            this.DemoButton4.TabIndex = 3;
            this.DemoButton4.UseVisualStyleBackColor = false;
            // 
            // VersionButton
            // 
            this.VersionButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.VersionButton.Content = "Version";
            this.VersionButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.VersionButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.VersionButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.VersionButton.FlatAppearance.BorderSize = 0;
            this.VersionButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.VersionButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.VersionButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.VersionButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.VersionButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.VersionButton.Image = null;
            this.VersionButton.Location = new System.Drawing.Point(20, 172);
            this.VersionButton.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.VersionButton.Name = "VersionButton";
            this.VersionButton.Size = new System.Drawing.Size(310, 30);
            this.VersionButton.Styles.MouseDownStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.VersionButton.Styles.MouseDownStyle.BorderSize = 1;
            this.VersionButton.Styles.MouseOverStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.VersionButton.Styles.MouseOverStyle.BorderSize = 1;
            this.VersionButton.Styles.NormalStyle.BorderSize = 1;
            this.VersionButton.TabIndex = 4;
            this.VersionButton.UseVisualStyleBackColor = false;
            // 
            // FooterControl
            // 
            this.FooterControl.Location = new System.Drawing.Point(0, 258);
            this.FooterControl.Name = "FooterControl";
            this.FooterControl.Size = new System.Drawing.Size(350, 22);
            this.FooterControl.TabIndex = 3;
            this.FooterControl.Text = "statusStrip1";
            // 
            // HeaderCaptionControl
            // 
            this.HeaderCaptionControl.Active = true;
            this.HeaderCaptionControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.HeaderCaptionControl.CloseBox = true;
            this.HeaderCaptionControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HeaderCaptionControl.Location = new System.Drawing.Point(0, 0);
            this.HeaderCaptionControl.Margin = new System.Windows.Forms.Padding(0);
            this.HeaderCaptionControl.MaximizeBox = true;
            this.HeaderCaptionControl.MinimizeBox = true;
            this.HeaderCaptionControl.Name = "HeaderCaptionControl";
            this.HeaderCaptionControl.Size = new System.Drawing.Size(350, 30);
            this.HeaderCaptionControl.TabIndex = 0;
            this.HeaderCaptionControl.WindowState = System.Windows.Forms.FormWindowState.Normal;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(350, 280);
            this.Controls.Add(this.LayoutPanel);
            this.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximumSize = new System.Drawing.Size(1920, 1160);
            this.MinimumSize = new System.Drawing.Size(300, 0);
            this.Name = "MainForm";
            this.Text = "Cube.Forms.Demo";
            this.LayoutPanel.ResumeLayout(false);
            this.LayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Separator)).EndInit();
            this.ContentsControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel LayoutPanel;
        private CustomCaptionControl HeaderCaptionControl;
        private PictureBox Separator;
        private FlowLayoutPanel ContentsControl;
        private StatusStrip FooterControl;
        private FlatButton DemoButton1;
        private FlatButton DemoButton2;
        private FlatButton DemoButton3;
        private FlatButton DemoButton4;
        private FlatButton VersionButton;
    }
}
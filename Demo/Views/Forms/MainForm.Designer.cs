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
            Cube.Forms.ButtonStyle buttonStyle1 = new Cube.Forms.ButtonStyle();
            Cube.Forms.ButtonStyle buttonStyle2 = new Cube.Forms.ButtonStyle();
            Cube.Forms.ButtonStyle buttonStyle3 = new Cube.Forms.ButtonStyle();
            Cube.Forms.ButtonStyle buttonStyle4 = new Cube.Forms.ButtonStyle();
            Cube.Forms.ButtonStyle buttonStyle5 = new Cube.Forms.ButtonStyle();
            Cube.Forms.ButtonStyle buttonStyle6 = new Cube.Forms.ButtonStyle();
            Cube.Forms.ButtonStyle buttonStyle7 = new Cube.Forms.ButtonStyle();
            Cube.Forms.ButtonStyle buttonStyle8 = new Cube.Forms.ButtonStyle();
            Cube.Forms.ButtonStyle buttonStyle9 = new Cube.Forms.ButtonStyle();
            Cube.Forms.ButtonStyle buttonStyle10 = new Cube.Forms.ButtonStyle();
            Cube.Forms.ButtonStyle buttonStyle11 = new Cube.Forms.ButtonStyle();
            Cube.Forms.ButtonStyle buttonStyle12 = new Cube.Forms.ButtonStyle();
            Cube.Forms.ButtonStyle buttonStyle13 = new Cube.Forms.ButtonStyle();
            Cube.Forms.ButtonStyle buttonStyle14 = new Cube.Forms.ButtonStyle();
            Cube.Forms.ButtonStyle buttonStyle15 = new Cube.Forms.ButtonStyle();
            Cube.Forms.ButtonStyle buttonStyle16 = new Cube.Forms.ButtonStyle();
            Cube.Forms.ButtonStyle buttonStyle17 = new Cube.Forms.ButtonStyle();
            Cube.Forms.ButtonStyle buttonStyle18 = new Cube.Forms.ButtonStyle();
            Cube.Forms.ButtonStyle buttonStyle19 = new Cube.Forms.ButtonStyle();
            Cube.Forms.ButtonStyle buttonStyle20 = new Cube.Forms.ButtonStyle();
            Cube.Forms.ButtonStyle buttonStyle21 = new Cube.Forms.ButtonStyle();
            Cube.Forms.ButtonStyle buttonStyle22 = new Cube.Forms.ButtonStyle();
            Cube.Forms.ButtonStyle buttonStyle23 = new Cube.Forms.ButtonStyle();
            Cube.Forms.ButtonStyle buttonStyle24 = new Cube.Forms.ButtonStyle();
            Cube.Forms.ButtonStyle buttonStyle25 = new Cube.Forms.ButtonStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.LayoutPanel = new Cube.Forms.TableLayoutPanel();
            this.HeaderControl = new Cube.Forms.Demo.CustomCaptionControl();
            this.Separator = new Cube.Forms.PictureBox();
            this.ContentsControl = new Cube.Forms.FlowLayoutPanel();
            this.DemoButton1 = new Cube.Forms.FlatButton();
            this.DemoButton2 = new Cube.Forms.FlatButton();
            this.DemoButton3 = new Cube.Forms.FlatButton();
            this.DemoButton4 = new Cube.Forms.FlatButton();
            this.VersionButton = new Cube.Forms.FlatButton();
            this.FooterControl = new Cube.Forms.StatusStrip();
            this.LayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Separator)).BeginInit();
            this.ContentsControl.SuspendLayout();
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
            this.LayoutPanel.Size = new System.Drawing.Size(350, 280);
            this.LayoutPanel.TabIndex = 0;
            // 
            // HeaderControl
            // 
            this.HeaderControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.HeaderControl.CloseBox = true;
            this.HeaderControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HeaderControl.Active = true;
            this.HeaderControl.Location = new System.Drawing.Point(0, 0);
            this.HeaderControl.Margin = new System.Windows.Forms.Padding(0);
            this.HeaderControl.MaximizeBox = true;
            this.HeaderControl.MinimizeBox = true;
            this.HeaderControl.Name = "HeaderControl";
            this.HeaderControl.Size = new System.Drawing.Size(350, 30);
            this.HeaderControl.TabIndex = 0;
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
            buttonStyle1.BackColor = System.Drawing.Color.Empty;
            buttonStyle1.BackgroundImage = null;
            buttonStyle1.BorderColor = System.Drawing.Color.Empty;
            buttonStyle1.ContentColor = System.Drawing.Color.Empty;
            buttonStyle1.Image = null;
            this.DemoButton1.Styles.Checked = buttonStyle1;
            buttonStyle2.BackColor = System.Drawing.Color.Empty;
            buttonStyle2.BackgroundImage = null;
            buttonStyle2.BorderColor = System.Drawing.Color.Empty;
            buttonStyle2.ContentColor = System.Drawing.Color.Empty;
            buttonStyle2.Image = null;
            this.DemoButton1.Styles.Disabled = buttonStyle2;
            buttonStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            buttonStyle3.BackgroundImage = null;
            buttonStyle3.BorderColor = System.Drawing.SystemColors.ControlDark;
            buttonStyle3.BorderSize = 1;
            buttonStyle3.ContentColor = System.Drawing.Color.Empty;
            buttonStyle3.Image = null;
            this.DemoButton1.Styles.MouseDown = buttonStyle3;
            buttonStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            buttonStyle4.BackgroundImage = null;
            buttonStyle4.BorderColor = System.Drawing.SystemColors.ControlDark;
            buttonStyle4.BorderSize = 1;
            buttonStyle4.ContentColor = System.Drawing.Color.Empty;
            buttonStyle4.Image = null;
            this.DemoButton1.Styles.MouseOver = buttonStyle4;
            buttonStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            buttonStyle5.BackgroundImage = null;
            buttonStyle5.BorderColor = System.Drawing.SystemColors.ControlDark;
            buttonStyle5.BorderSize = 1;
            buttonStyle5.ContentColor = System.Drawing.SystemColors.WindowText;
            buttonStyle5.Image = null;
            this.DemoButton1.Styles.Normal = buttonStyle5;
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
            buttonStyle6.BackColor = System.Drawing.Color.Empty;
            buttonStyle6.BackgroundImage = null;
            buttonStyle6.BorderColor = System.Drawing.Color.Empty;
            buttonStyle6.ContentColor = System.Drawing.Color.Empty;
            buttonStyle6.Image = null;
            this.DemoButton2.Styles.Checked = buttonStyle6;
            buttonStyle7.BackColor = System.Drawing.Color.Empty;
            buttonStyle7.BackgroundImage = null;
            buttonStyle7.BorderColor = System.Drawing.Color.Empty;
            buttonStyle7.ContentColor = System.Drawing.Color.Empty;
            buttonStyle7.Image = null;
            this.DemoButton2.Styles.Disabled = buttonStyle7;
            buttonStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            buttonStyle8.BackgroundImage = null;
            buttonStyle8.BorderColor = System.Drawing.SystemColors.ControlDark;
            buttonStyle8.BorderSize = 1;
            buttonStyle8.ContentColor = System.Drawing.Color.Empty;
            buttonStyle8.Image = null;
            this.DemoButton2.Styles.MouseDown = buttonStyle8;
            buttonStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            buttonStyle9.BackgroundImage = null;
            buttonStyle9.BorderColor = System.Drawing.SystemColors.ControlDark;
            buttonStyle9.BorderSize = 1;
            buttonStyle9.ContentColor = System.Drawing.Color.Empty;
            buttonStyle9.Image = null;
            this.DemoButton2.Styles.MouseOver = buttonStyle9;
            buttonStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            buttonStyle10.BackgroundImage = null;
            buttonStyle10.BorderColor = System.Drawing.SystemColors.ControlDark;
            buttonStyle10.BorderSize = 1;
            buttonStyle10.ContentColor = System.Drawing.SystemColors.WindowText;
            buttonStyle10.Image = null;
            this.DemoButton2.Styles.Normal = buttonStyle10;
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
            buttonStyle11.BackColor = System.Drawing.Color.Empty;
            buttonStyle11.BackgroundImage = null;
            buttonStyle11.BorderColor = System.Drawing.Color.Empty;
            buttonStyle11.ContentColor = System.Drawing.Color.Empty;
            buttonStyle11.Image = null;
            this.DemoButton3.Styles.Checked = buttonStyle11;
            buttonStyle12.BackColor = System.Drawing.Color.Empty;
            buttonStyle12.BackgroundImage = null;
            buttonStyle12.BorderColor = System.Drawing.Color.Empty;
            buttonStyle12.ContentColor = System.Drawing.Color.Empty;
            buttonStyle12.Image = null;
            this.DemoButton3.Styles.Disabled = buttonStyle12;
            buttonStyle13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            buttonStyle13.BackgroundImage = null;
            buttonStyle13.BorderColor = System.Drawing.SystemColors.ControlDark;
            buttonStyle13.BorderSize = 1;
            buttonStyle13.ContentColor = System.Drawing.Color.Empty;
            buttonStyle13.Image = null;
            this.DemoButton3.Styles.MouseDown = buttonStyle13;
            buttonStyle14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            buttonStyle14.BackgroundImage = null;
            buttonStyle14.BorderColor = System.Drawing.SystemColors.ControlDark;
            buttonStyle14.BorderSize = 1;
            buttonStyle14.ContentColor = System.Drawing.Color.Empty;
            buttonStyle14.Image = null;
            this.DemoButton3.Styles.MouseOver = buttonStyle14;
            buttonStyle15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            buttonStyle15.BackgroundImage = null;
            buttonStyle15.BorderColor = System.Drawing.SystemColors.ControlDark;
            buttonStyle15.BorderSize = 1;
            buttonStyle15.ContentColor = System.Drawing.SystemColors.WindowText;
            buttonStyle15.Image = null;
            this.DemoButton3.Styles.Normal = buttonStyle15;
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
            buttonStyle16.BackColor = System.Drawing.Color.Empty;
            buttonStyle16.BackgroundImage = null;
            buttonStyle16.BorderColor = System.Drawing.Color.Empty;
            buttonStyle16.ContentColor = System.Drawing.Color.Empty;
            buttonStyle16.Image = null;
            this.DemoButton4.Styles.Checked = buttonStyle16;
            buttonStyle17.BackColor = System.Drawing.Color.Empty;
            buttonStyle17.BackgroundImage = null;
            buttonStyle17.BorderColor = System.Drawing.Color.Empty;
            buttonStyle17.ContentColor = System.Drawing.Color.Empty;
            buttonStyle17.Image = null;
            this.DemoButton4.Styles.Disabled = buttonStyle17;
            buttonStyle18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            buttonStyle18.BackgroundImage = null;
            buttonStyle18.BorderColor = System.Drawing.SystemColors.ControlDark;
            buttonStyle18.BorderSize = 1;
            buttonStyle18.ContentColor = System.Drawing.Color.Empty;
            buttonStyle18.Image = null;
            this.DemoButton4.Styles.MouseDown = buttonStyle18;
            buttonStyle19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            buttonStyle19.BackgroundImage = null;
            buttonStyle19.BorderColor = System.Drawing.SystemColors.ControlDark;
            buttonStyle19.BorderSize = 1;
            buttonStyle19.ContentColor = System.Drawing.Color.Empty;
            buttonStyle19.Image = null;
            this.DemoButton4.Styles.MouseOver = buttonStyle19;
            buttonStyle20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            buttonStyle20.BackgroundImage = null;
            buttonStyle20.BorderColor = System.Drawing.SystemColors.ControlDark;
            buttonStyle20.BorderSize = 1;
            buttonStyle20.ContentColor = System.Drawing.SystemColors.WindowText;
            buttonStyle20.Image = null;
            this.DemoButton4.Styles.Normal = buttonStyle20;
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
            buttonStyle21.BackColor = System.Drawing.Color.Empty;
            buttonStyle21.BackgroundImage = null;
            buttonStyle21.BorderColor = System.Drawing.Color.Empty;
            buttonStyle21.ContentColor = System.Drawing.Color.Empty;
            buttonStyle21.Image = null;
            this.VersionButton.Styles.Checked = buttonStyle21;
            buttonStyle22.BackColor = System.Drawing.Color.Empty;
            buttonStyle22.BackgroundImage = null;
            buttonStyle22.BorderColor = System.Drawing.Color.Empty;
            buttonStyle22.ContentColor = System.Drawing.Color.Empty;
            buttonStyle22.Image = null;
            this.VersionButton.Styles.Disabled = buttonStyle22;
            buttonStyle23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            buttonStyle23.BackgroundImage = null;
            buttonStyle23.BorderColor = System.Drawing.SystemColors.ControlDark;
            buttonStyle23.BorderSize = 1;
            buttonStyle23.ContentColor = System.Drawing.Color.Empty;
            buttonStyle23.Image = null;
            this.VersionButton.Styles.MouseDown = buttonStyle23;
            buttonStyle24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            buttonStyle24.BackgroundImage = null;
            buttonStyle24.BorderColor = System.Drawing.SystemColors.ControlDark;
            buttonStyle24.BorderSize = 1;
            buttonStyle24.ContentColor = System.Drawing.Color.Empty;
            buttonStyle24.Image = null;
            this.VersionButton.Styles.MouseOver = buttonStyle24;
            buttonStyle25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            buttonStyle25.BackgroundImage = null;
            buttonStyle25.BorderColor = System.Drawing.SystemColors.ControlDark;
            buttonStyle25.BorderSize = 1;
            buttonStyle25.ContentColor = System.Drawing.SystemColors.WindowText;
            buttonStyle25.Image = null;
            this.VersionButton.Styles.Normal = buttonStyle25;
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
        private CustomCaptionControl HeaderControl;
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
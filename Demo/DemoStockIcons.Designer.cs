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
            this.SizableLayoutPanel = new Cube.Forms.TableLayoutPanel();
            this.IconListView = new Cube.Forms.ListView();
            this.TitleBar = new Cube.Forms.CaptionControl();
            this.CloseButton = new Cube.Forms.FlatButton();
            this.TitlePictureBox = new Cube.Forms.PictureBox();
            this.LogoPictureBox = new Cube.Forms.PictureBox();
            this.Splitter = new Cube.Forms.PictureBox();
            this.SizableLayoutPanel.SuspendLayout();
            this.TitleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TitlePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Splitter)).BeginInit();
            this.SuspendLayout();
            // 
            // SizableLayoutPanel
            // 
            this.SizableLayoutPanel.ColumnCount = 1;
            this.SizableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.SizableLayoutPanel.Controls.Add(this.IconListView, 0, 2);
            this.SizableLayoutPanel.Controls.Add(this.TitleBar, 0, 0);
            this.SizableLayoutPanel.Controls.Add(this.Splitter, 0, 1);
            this.SizableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SizableLayoutPanel.Location = new System.Drawing.Point(2, 2);
            this.SizableLayoutPanel.Name = "SizableLayoutPanel";
            this.SizableLayoutPanel.RowCount = 3;
            this.SizableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.SizableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.SizableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.SizableLayoutPanel.Size = new System.Drawing.Size(636, 396);
            this.SizableLayoutPanel.TabIndex = 0;
            // 
            // IconListView
            // 
            this.IconListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.IconListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.IconListView.Location = new System.Drawing.Point(0, 26);
            this.IconListView.Margin = new System.Windows.Forms.Padding(0);
            this.IconListView.Name = "IconListView";
            this.IconListView.Size = new System.Drawing.Size(636, 370);
            this.IconListView.TabIndex = 12;
            this.IconListView.Theme = Cube.Forms.WindowTheme.Explorer;
            this.IconListView.UseCompatibleStateImageBehavior = false;
            // 
            // TitleBar
            // 
            this.TitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.TitleBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.TitleBar.Controls.Add(this.CloseButton);
            this.TitleBar.Controls.Add(this.TitlePictureBox);
            this.TitleBar.Controls.Add(this.LogoPictureBox);
            this.TitleBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TitleBar.Location = new System.Drawing.Point(0, 0);
            this.TitleBar.Margin = new System.Windows.Forms.Padding(0);
            this.TitleBar.Name = "TitleBar";
            this.TitleBar.Size = new System.Drawing.Size(636, 25);
            this.TitleBar.TabIndex = 11;
            // 
            // CloseButton
            // 
            this.CloseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CloseButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.CloseButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CloseButton.FlatAppearance.BorderSize = 0;
            this.CloseButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CloseButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CloseButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CloseButton.Image = null;
            this.CloseButton.Location = new System.Drawing.Point(611, 0);
            this.CloseButton.MouseDownSurface.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.CloseButton.MouseDownSurface.BackgroundImage = null;
            this.CloseButton.MouseDownSurface.BorderColor = System.Drawing.Color.Empty;
            this.CloseButton.MouseDownSurface.Image = null;
            this.CloseButton.MouseDownSurface.ContentColor = System.Drawing.Color.Empty;
            this.CloseButton.MouseOverSurface.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.CloseButton.MouseOverSurface.BackgroundImage = null;
            this.CloseButton.MouseOverSurface.BorderColor = System.Drawing.Color.Empty;
            this.CloseButton.MouseOverSurface.Image = null;
            this.CloseButton.MouseOverSurface.ContentColor = System.Drawing.Color.Empty;
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(25, 25);
            this.CloseButton.Surface.BackColor = System.Drawing.Color.Empty;
            this.CloseButton.Surface.BackgroundImage = null;
            this.CloseButton.Surface.BorderColor = System.Drawing.Color.Empty;
            this.CloseButton.Surface.BorderSize = 0;
            this.CloseButton.Surface.Image = global::Cube.Forms.Demo.Properties.Resources.ButtonClose;
            this.CloseButton.Surface.ContentColor = System.Drawing.Color.Empty;
            this.CloseButton.TabIndex = 0;
            this.CloseButton.UseVisualStyleBackColor = false;
            // 
            // TitlePictureBox
            // 
            this.TitlePictureBox.BackColor = System.Drawing.Color.Transparent;
            this.TitlePictureBox.Image = global::Cube.Forms.Demo.Properties.Resources.LogoTitle;
            this.TitlePictureBox.Location = new System.Drawing.Point(32, 6);
            this.TitlePictureBox.Name = "TitlePictureBox";
            this.TitlePictureBox.Size = new System.Drawing.Size(41, 13);
            this.TitlePictureBox.TabIndex = 1;
            this.TitlePictureBox.TabStop = false;
            // 
            // LogoPictureBox
            // 
            this.LogoPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.LogoPictureBox.Image = global::Cube.Forms.Demo.Properties.Resources.LogoSmall;
            this.LogoPictureBox.Location = new System.Drawing.Point(6, 2);
            this.LogoPictureBox.Name = "LogoPictureBox";
            this.LogoPictureBox.Size = new System.Drawing.Size(20, 20);
            this.LogoPictureBox.TabIndex = 0;
            this.LogoPictureBox.TabStop = false;
            // 
            // Splitter
            // 
            this.Splitter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.Splitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Splitter.Location = new System.Drawing.Point(0, 25);
            this.Splitter.Margin = new System.Windows.Forms.Padding(0);
            this.Splitter.Name = "Splitter";
            this.Splitter.Size = new System.Drawing.Size(636, 1);
            this.Splitter.TabIndex = 13;
            this.Splitter.TabStop = false;
            // 
            // DemoStockIcons
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 400);
            this.Controls.Add(this.SizableLayoutPanel);
            this.Name = "DemoStockIcons";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.ShowInTaskbar = false;
            this.SizeGrip = 3;
            this.Text = "DemoStockIcons";
            this.SizableLayoutPanel.ResumeLayout(false);
            this.TitleBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TitlePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Splitter)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel SizableLayoutPanel;
        private ListView IconListView;
        private CaptionControl TitleBar;
        private FlatButton CloseButton;
        private PictureBox TitlePictureBox;
        private PictureBox LogoPictureBox;
        private PictureBox Splitter;
    }
}
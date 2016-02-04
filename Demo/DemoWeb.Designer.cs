namespace Cube.Forms.Demo
{
    partial class DemoWeb
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DemoWeb));
            this.SizableLayoutPanel = new Cube.Forms.TableLayoutPanel();
            this.ToolPanel = new Cube.Forms.TableLayoutPanel();
            this.UrlTextBox = new System.Windows.Forms.TextBox();
            this.UpdateButton = new Cube.Forms.FlatButton();
            this.WebBrowser = new Cube.Forms.WebBrowser();
            this.TitleBar = new Cube.Forms.UserControl();
            this.CloseButton = new Cube.Forms.FlatButton();
            this.TitlePictureBox = new Cube.Forms.PictureBox();
            this.LogoPictureBox = new Cube.Forms.PictureBox();
            this.Splitter1 = new Cube.Forms.PictureBox();
            this.Splitter2 = new Cube.Forms.PictureBox();
            this.SizableLayoutPanel.SuspendLayout();
            this.ToolPanel.SuspendLayout();
            this.TitleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TitlePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Splitter1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Splitter2)).BeginInit();
            this.SuspendLayout();
            // 
            // SizableLayoutPanel
            // 
            this.SizableLayoutPanel.ColumnCount = 1;
            this.SizableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.SizableLayoutPanel.Controls.Add(this.ToolPanel, 0, 2);
            this.SizableLayoutPanel.Controls.Add(this.WebBrowser, 0, 4);
            this.SizableLayoutPanel.Controls.Add(this.TitleBar, 0, 0);
            this.SizableLayoutPanel.Controls.Add(this.Splitter1, 0, 1);
            this.SizableLayoutPanel.Controls.Add(this.Splitter2, 0, 3);
            this.SizableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SizableLayoutPanel.Location = new System.Drawing.Point(2, 2);
            this.SizableLayoutPanel.Name = "SizableLayoutPanel";
            this.SizableLayoutPanel.RowCount = 5;
            this.SizableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.SizableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.SizableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.SizableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.SizableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.SizableLayoutPanel.Size = new System.Drawing.Size(596, 396);
            this.SizableLayoutPanel.TabIndex = 3;
            // 
            // ToolPanel
            // 
            this.ToolPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ToolPanel.ColumnCount = 2;
            this.ToolPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ToolPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.ToolPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.ToolPanel.Controls.Add(this.UrlTextBox, 0, 0);
            this.ToolPanel.Controls.Add(this.UpdateButton, 1, 0);
            this.ToolPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ToolPanel.Location = new System.Drawing.Point(0, 26);
            this.ToolPanel.Margin = new System.Windows.Forms.Padding(0);
            this.ToolPanel.Name = "ToolPanel";
            this.ToolPanel.RowCount = 1;
            this.ToolPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ToolPanel.Size = new System.Drawing.Size(596, 30);
            this.ToolPanel.TabIndex = 10;
            // 
            // UrlTextBox
            // 
            this.UrlTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UrlTextBox.Location = new System.Drawing.Point(3, 3);
            this.UrlTextBox.Name = "UrlTextBox";
            this.UrlTextBox.Size = new System.Drawing.Size(550, 23);
            this.UrlTextBox.TabIndex = 0;
            // 
            // UpdateButton
            // 
            this.UpdateButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.UpdateButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UpdateButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UpdateButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.UpdateButton.FlatAppearance.BorderSize = 0;
            this.UpdateButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.UpdateButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.UpdateButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.UpdateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UpdateButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.UpdateButton.Image = null;
            this.UpdateButton.Location = new System.Drawing.Point(559, 3);
            this.UpdateButton.MouseDownSurface.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.UpdateButton.MouseDownSurface.BackgroundImage = null;
            this.UpdateButton.MouseDownSurface.BorderColor = System.Drawing.Color.Empty;
            this.UpdateButton.MouseDownSurface.Image = null;
            this.UpdateButton.MouseDownSurface.TextColor = System.Drawing.Color.Empty;
            this.UpdateButton.MouseOverSurface.BackColor = System.Drawing.Color.Gainsboro;
            this.UpdateButton.MouseOverSurface.BackgroundImage = null;
            this.UpdateButton.MouseOverSurface.BorderColor = System.Drawing.Color.Gray;
            this.UpdateButton.MouseOverSurface.BorderSize = 1;
            this.UpdateButton.MouseOverSurface.Image = null;
            this.UpdateButton.MouseOverSurface.TextColor = System.Drawing.Color.Empty;
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(34, 24);
            this.UpdateButton.Surface.BackColor = System.Drawing.Color.Empty;
            this.UpdateButton.Surface.BackgroundImage = null;
            this.UpdateButton.Surface.BorderColor = System.Drawing.Color.Empty;
            this.UpdateButton.Surface.BorderSize = 0;
            this.UpdateButton.Surface.Image = global::Cube.Forms.Demo.Properties.Resources.ButtonUpdate;
            this.UpdateButton.Surface.TextColor = System.Drawing.Color.Empty;
            this.UpdateButton.TabIndex = 1;
            this.UpdateButton.UseVisualStyleBackColor = false;
            // 
            // WebBrowser
            // 
            this.WebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WebBrowser.Location = new System.Drawing.Point(0, 57);
            this.WebBrowser.Margin = new System.Windows.Forms.Padding(0);
            this.WebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.WebBrowser.Name = "WebBrowser";
            this.WebBrowser.Size = new System.Drawing.Size(596, 339);
            this.WebBrowser.TabIndex = 8;
            this.WebBrowser.Url = new System.Uri("about:blank", System.UriKind.Absolute);
            // 
            // TitleBar
            // 
            this.TitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.TitleBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.TitleBar.Controls.Add(this.CloseButton);
            this.TitleBar.Controls.Add(this.TitlePictureBox);
            this.TitleBar.Controls.Add(this.LogoPictureBox);
            this.TitleBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TitleBar.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.TitleBar.Location = new System.Drawing.Point(0, 0);
            this.TitleBar.Margin = new System.Windows.Forms.Padding(0);
            this.TitleBar.Name = "TitleBar";
            this.TitleBar.Size = new System.Drawing.Size(596, 25);
            this.TitleBar.TabIndex = 1;
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
            this.CloseButton.Location = new System.Drawing.Point(571, 0);
            this.CloseButton.MouseDownSurface.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.CloseButton.MouseDownSurface.BackgroundImage = null;
            this.CloseButton.MouseDownSurface.BorderColor = System.Drawing.Color.Empty;
            this.CloseButton.MouseDownSurface.Image = null;
            this.CloseButton.MouseDownSurface.TextColor = System.Drawing.Color.Empty;
            this.CloseButton.MouseOverSurface.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.CloseButton.MouseOverSurface.BackgroundImage = null;
            this.CloseButton.MouseOverSurface.BorderColor = System.Drawing.Color.Empty;
            this.CloseButton.MouseOverSurface.Image = null;
            this.CloseButton.MouseOverSurface.TextColor = System.Drawing.Color.Empty;
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(25, 25);
            this.CloseButton.Surface.BackColor = System.Drawing.Color.Empty;
            this.CloseButton.Surface.BackgroundImage = null;
            this.CloseButton.Surface.BorderColor = System.Drawing.Color.Empty;
            this.CloseButton.Surface.BorderSize = 0;
            this.CloseButton.Surface.Image = global::Cube.Forms.Demo.Properties.Resources.ButtonClose;
            this.CloseButton.Surface.TextColor = System.Drawing.Color.Empty;
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
            // Splitter1
            // 
            this.Splitter1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.Splitter1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Splitter1.Location = new System.Drawing.Point(0, 25);
            this.Splitter1.Margin = new System.Windows.Forms.Padding(0);
            this.Splitter1.Name = "Splitter1";
            this.Splitter1.Size = new System.Drawing.Size(596, 1);
            this.Splitter1.TabIndex = 9;
            this.Splitter1.TabStop = false;
            // 
            // Splitter2
            // 
            this.Splitter2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.Splitter2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Splitter2.Location = new System.Drawing.Point(0, 56);
            this.Splitter2.Margin = new System.Windows.Forms.Padding(0);
            this.Splitter2.Name = "Splitter2";
            this.Splitter2.Size = new System.Drawing.Size(596, 1);
            this.Splitter2.TabIndex = 11;
            this.Splitter2.TabStop = false;
            // 
            // DemoWeb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.Controls.Add(this.SizableLayoutPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "DemoWeb";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.ShowInTaskbar = false;
            this.Sizable = true;
            this.Text = "WebForm";
            this.SizableLayoutPanel.ResumeLayout(false);
            this.ToolPanel.ResumeLayout(false);
            this.ToolPanel.PerformLayout();
            this.TitleBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TitlePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Splitter1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Splitter2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel SizableLayoutPanel;
        private UserControl TitleBar;
        private FlatButton CloseButton;
        private PictureBox TitlePictureBox;
        private PictureBox LogoPictureBox;
        private TableLayoutPanel ToolPanel;
        private System.Windows.Forms.TextBox UrlTextBox;
        private FlatButton UpdateButton;
        private WebBrowser WebBrowser;
        private PictureBox Splitter1;
        private PictureBox Splitter2;
    }
}
namespace Cube.Forms.Demo
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.HeaderPanel = new System.Windows.Forms.Panel();
            this.CloseButton = new Cube.Forms.Button();
            this.TitlePictureBox = new System.Windows.Forms.PictureBox();
            this.LogoPictureBox = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ButtonsButton = new Cube.Forms.Button();
            this.WebBrowserButton = new Cube.Forms.Button();
            this.NotifyFormButton = new Cube.Forms.Button();
            this.HeaderPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TitlePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // HeaderPanel
            // 
            this.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.HeaderPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.HeaderPanel.Controls.Add(this.CloseButton);
            this.HeaderPanel.Controls.Add(this.TitlePictureBox);
            this.HeaderPanel.Controls.Add(this.LogoPictureBox);
            this.HeaderPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.HeaderPanel.Location = new System.Drawing.Point(0, 0);
            this.HeaderPanel.Margin = new System.Windows.Forms.Padding(0);
            this.HeaderPanel.Name = "HeaderPanel";
            this.HeaderPanel.Size = new System.Drawing.Size(250, 25);
            this.HeaderPanel.TabIndex = 0;
            // 
            // CloseButton
            // 
            this.CloseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.CloseButton.BorderColor = System.Drawing.Color.Transparent;
            this.CloseButton.BorderSize = 0;
            this.CloseButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.Image = global::Cube.Forms.Demo.Properties.Resources.ButtonClose;
            this.CloseButton.Location = new System.Drawing.Point(225, 0);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(25, 25);
            this.CloseButton.Surface.CheckedBackColor = System.Drawing.Color.Empty;
            this.CloseButton.Surface.CheckedBackgroundImage = null;
            this.CloseButton.Surface.CheckedBorderColor = System.Drawing.Color.Empty;
            this.CloseButton.Surface.CheckedForeColor = System.Drawing.Color.Empty;
            this.CloseButton.Surface.CheckedImage = null;
            this.CloseButton.Surface.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.CloseButton.Surface.MouseDownBackgroundImage = null;
            this.CloseButton.Surface.MouseDownBorderColor = System.Drawing.Color.Empty;
            this.CloseButton.Surface.MouseDownForeColor = System.Drawing.Color.Empty;
            this.CloseButton.Surface.MouseDownImage = null;
            this.CloseButton.Surface.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.CloseButton.Surface.MouseOverBackgroundImage = null;
            this.CloseButton.Surface.MouseOverBorderColor = System.Drawing.Color.Empty;
            this.CloseButton.Surface.MouseOverForeColor = System.Drawing.Color.Empty;
            this.CloseButton.Surface.MouseOverImage = null;
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
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Location = new System.Drawing.Point(0, 25);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(250, 1);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // ButtonsButton
            // 
            this.ButtonsButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.ButtonsButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(172)))), ((int)(((byte)(172)))));
            this.ButtonsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonsButton.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ButtonsButton.ForeColor = System.Drawing.Color.Black;
            this.ButtonsButton.Location = new System.Drawing.Point(12, 45);
            this.ButtonsButton.Name = "ButtonsButton";
            this.ButtonsButton.Size = new System.Drawing.Size(226, 23);
            this.ButtonsButton.Surface.CheckedBackColor = System.Drawing.Color.Empty;
            this.ButtonsButton.Surface.CheckedBackgroundImage = null;
            this.ButtonsButton.Surface.CheckedBorderColor = System.Drawing.Color.Empty;
            this.ButtonsButton.Surface.CheckedForeColor = System.Drawing.Color.Empty;
            this.ButtonsButton.Surface.CheckedImage = null;
            this.ButtonsButton.Surface.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.ButtonsButton.Surface.MouseDownBackgroundImage = null;
            this.ButtonsButton.Surface.MouseDownBorderColor = System.Drawing.Color.Empty;
            this.ButtonsButton.Surface.MouseDownForeColor = System.Drawing.Color.Empty;
            this.ButtonsButton.Surface.MouseDownImage = null;
            this.ButtonsButton.Surface.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.ButtonsButton.Surface.MouseOverBackgroundImage = null;
            this.ButtonsButton.Surface.MouseOverBorderColor = System.Drawing.Color.Empty;
            this.ButtonsButton.Surface.MouseOverForeColor = System.Drawing.Color.Empty;
            this.ButtonsButton.Surface.MouseOverImage = null;
            this.ButtonsButton.TabIndex = 1;
            this.ButtonsButton.Text = "Buttons";
            this.ButtonsButton.UseVisualStyleBackColor = false;
            this.ButtonsButton.Click += new System.EventHandler(this.ButtonsButton_Click);
            // 
            // WebBrowserButton
            // 
            this.WebBrowserButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.WebBrowserButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(172)))), ((int)(((byte)(172)))));
            this.WebBrowserButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.WebBrowserButton.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.WebBrowserButton.ForeColor = System.Drawing.Color.Black;
            this.WebBrowserButton.Location = new System.Drawing.Point(12, 74);
            this.WebBrowserButton.Name = "WebBrowserButton";
            this.WebBrowserButton.Size = new System.Drawing.Size(226, 23);
            this.WebBrowserButton.Surface.CheckedBackColor = System.Drawing.Color.Empty;
            this.WebBrowserButton.Surface.CheckedBackgroundImage = null;
            this.WebBrowserButton.Surface.CheckedBorderColor = System.Drawing.Color.Empty;
            this.WebBrowserButton.Surface.CheckedForeColor = System.Drawing.Color.Empty;
            this.WebBrowserButton.Surface.CheckedImage = null;
            this.WebBrowserButton.Surface.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.WebBrowserButton.Surface.MouseDownBackgroundImage = null;
            this.WebBrowserButton.Surface.MouseDownBorderColor = System.Drawing.Color.Empty;
            this.WebBrowserButton.Surface.MouseDownForeColor = System.Drawing.Color.Empty;
            this.WebBrowserButton.Surface.MouseDownImage = null;
            this.WebBrowserButton.Surface.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.WebBrowserButton.Surface.MouseOverBackgroundImage = null;
            this.WebBrowserButton.Surface.MouseOverBorderColor = System.Drawing.Color.Empty;
            this.WebBrowserButton.Surface.MouseOverForeColor = System.Drawing.Color.Empty;
            this.WebBrowserButton.Surface.MouseOverImage = null;
            this.WebBrowserButton.TabIndex = 2;
            this.WebBrowserButton.Text = "WebBrowser";
            this.WebBrowserButton.UseVisualStyleBackColor = false;
            // 
            // NotifyFormButton
            // 
            this.NotifyFormButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.NotifyFormButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(172)))), ((int)(((byte)(172)))));
            this.NotifyFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NotifyFormButton.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.NotifyFormButton.ForeColor = System.Drawing.Color.Black;
            this.NotifyFormButton.Location = new System.Drawing.Point(12, 103);
            this.NotifyFormButton.Name = "NotifyFormButton";
            this.NotifyFormButton.Size = new System.Drawing.Size(226, 23);
            this.NotifyFormButton.Surface.CheckedBackColor = System.Drawing.Color.Empty;
            this.NotifyFormButton.Surface.CheckedBackgroundImage = null;
            this.NotifyFormButton.Surface.CheckedBorderColor = System.Drawing.Color.Empty;
            this.NotifyFormButton.Surface.CheckedForeColor = System.Drawing.Color.Empty;
            this.NotifyFormButton.Surface.CheckedImage = null;
            this.NotifyFormButton.Surface.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.NotifyFormButton.Surface.MouseDownBackgroundImage = null;
            this.NotifyFormButton.Surface.MouseDownBorderColor = System.Drawing.Color.Empty;
            this.NotifyFormButton.Surface.MouseDownForeColor = System.Drawing.Color.Empty;
            this.NotifyFormButton.Surface.MouseDownImage = null;
            this.NotifyFormButton.Surface.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.NotifyFormButton.Surface.MouseOverBackgroundImage = null;
            this.NotifyFormButton.Surface.MouseOverBorderColor = System.Drawing.Color.Empty;
            this.NotifyFormButton.Surface.MouseOverForeColor = System.Drawing.Color.Empty;
            this.NotifyFormButton.Surface.MouseOverImage = null;
            this.NotifyFormButton.TabIndex = 3;
            this.NotifyFormButton.Text = "NotifyForm";
            this.NotifyFormButton.UseVisualStyleBackColor = false;
            // 
            // MainForm
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(250, 160);
            this.Controls.Add(this.NotifyFormButton);
            this.Controls.Add(this.WebBrowserButton);
            this.Controls.Add(this.ButtonsButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.HeaderPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.HeaderPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TitlePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel HeaderPanel;
        private Button CloseButton;
        private System.Windows.Forms.PictureBox TitlePictureBox;
        private System.Windows.Forms.PictureBox LogoPictureBox;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Button ButtonsButton;
        private Button WebBrowserButton;
        private Button NotifyFormButton;
    }
}
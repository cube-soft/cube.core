namespace Cube.Forms.Demo
{
    partial class FormBase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBase));
            this.TitleBar = new Cube.Forms.UserControl();
            this.CloseButton = new Cube.Forms.FlatButton();
            this.TitlePictureBox = new Cube.Forms.PictureBox();
            this.LogoPictureBox = new Cube.Forms.PictureBox();
            this.HeaderSplitter = new Cube.Forms.PictureBox();
            this.TitleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TitlePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeaderSplitter)).BeginInit();
            this.SuspendLayout();
            // 
            // TitleBar
            // 
            this.TitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.TitleBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.TitleBar.Controls.Add(this.CloseButton);
            this.TitleBar.Controls.Add(this.TitlePictureBox);
            this.TitleBar.Controls.Add(this.LogoPictureBox);
            this.TitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.TitleBar.Location = new System.Drawing.Point(0, 0);
            this.TitleBar.Margin = new System.Windows.Forms.Padding(0);
            this.TitleBar.Name = "TitleBar";
            this.TitleBar.Size = new System.Drawing.Size(300, 25);
            this.TitleBar.TabIndex = 0;
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
            this.CloseButton.Location = new System.Drawing.Point(275, 0);
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
            // HeaderSplitter
            // 
            this.HeaderSplitter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.HeaderSplitter.Dock = System.Windows.Forms.DockStyle.Top;
            this.HeaderSplitter.Location = new System.Drawing.Point(0, 25);
            this.HeaderSplitter.Name = "HeaderSplitter";
            this.HeaderSplitter.Size = new System.Drawing.Size(300, 1);
            this.HeaderSplitter.TabIndex = 8;
            this.HeaderSplitter.TabStop = false;
            // 
            // FormBase
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(300, 300);
            this.Controls.Add(this.HeaderSplitter);
            this.Controls.Add(this.TitleBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormBase";
            this.Text = "Cube.Forms.Demo";
            this.TitleBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TitlePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeaderSplitter)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UserControl TitleBar;
        private FlatButton CloseButton;
        private PictureBox TitlePictureBox;
        private PictureBox LogoPictureBox;
        private PictureBox HeaderSplitter;
    }
}
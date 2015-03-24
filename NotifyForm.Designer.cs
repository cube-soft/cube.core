namespace Cube.Forms
{
    partial class NotifyForm
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
            this.LayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.ImageButton = new Cube.Forms.Button();
            this.TitleButton = new Cube.Forms.Button();
            this.CloseButton = new Cube.Forms.Button();
            this.LayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // LayoutPanel
            // 
            this.LayoutPanel.ColumnCount = 3;
            this.LayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.LayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.LayoutPanel.Controls.Add(this.ImageButton, 0, 0);
            this.LayoutPanel.Controls.Add(this.TitleButton, 1, 0);
            this.LayoutPanel.Controls.Add(this.CloseButton, 2, 0);
            this.LayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.LayoutPanel.Name = "LayoutPanel";
            this.LayoutPanel.RowCount = 1;
            this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LayoutPanel.Size = new System.Drawing.Size(340, 60);
            this.LayoutPanel.TabIndex = 0;
            // 
            // ImageButton
            // 
            this.ImageButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ImageButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ImageButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ImageButton.FlatAppearance.BorderSize = 0;
            this.ImageButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ImageButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ImageButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ImageButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ImageButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ImageButton.Image = null;
            this.ImageButton.Location = new System.Drawing.Point(0, 0);
            this.ImageButton.Margin = new System.Windows.Forms.Padding(0);
            this.ImageButton.MouseDownSurface.BackColor = System.Drawing.Color.Empty;
            this.ImageButton.MouseDownSurface.BackgroundImage = null;
            this.ImageButton.MouseDownSurface.BorderColor = System.Drawing.Color.Empty;
            this.ImageButton.MouseDownSurface.Image = null;
            this.ImageButton.MouseDownSurface.TextColor = System.Drawing.Color.Empty;
            this.ImageButton.MouseOverSurface.BackColor = System.Drawing.Color.Empty;
            this.ImageButton.MouseOverSurface.BackgroundImage = null;
            this.ImageButton.MouseOverSurface.BorderColor = System.Drawing.Color.Empty;
            this.ImageButton.MouseOverSurface.Image = null;
            this.ImageButton.MouseOverSurface.TextColor = System.Drawing.Color.Empty;
            this.ImageButton.Name = "ImageButton";
            this.ImageButton.Size = new System.Drawing.Size(60, 60);
            this.ImageButton.Surface.BackColor = System.Drawing.Color.White;
            this.ImageButton.Surface.BackgroundImage = null;
            this.ImageButton.Surface.BorderColor = System.Drawing.Color.Empty;
            this.ImageButton.Surface.BorderSize = 0;
            this.ImageButton.Surface.Image = global::Cube.Forms.Properties.Resources.LogoLarge;
            this.ImageButton.Surface.TextColor = System.Drawing.Color.Empty;
            this.ImageButton.TabIndex = 0;
            this.ImageButton.UseVisualStyleBackColor = false;
            // 
            // TitleButton
            // 
            this.TitleButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TitleButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TitleButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TitleButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TitleButton.FlatAppearance.BorderSize = 0;
            this.TitleButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TitleButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TitleButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TitleButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TitleButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TitleButton.Image = null;
            this.TitleButton.Location = new System.Drawing.Point(60, 0);
            this.TitleButton.Margin = new System.Windows.Forms.Padding(0);
            this.TitleButton.MouseDownSurface.BackColor = System.Drawing.Color.Empty;
            this.TitleButton.MouseDownSurface.BackgroundImage = null;
            this.TitleButton.MouseDownSurface.BorderColor = System.Drawing.Color.Empty;
            this.TitleButton.MouseDownSurface.Image = null;
            this.TitleButton.MouseDownSurface.TextColor = System.Drawing.Color.Empty;
            this.TitleButton.MouseOverSurface.BackColor = System.Drawing.Color.Empty;
            this.TitleButton.MouseOverSurface.BackgroundImage = null;
            this.TitleButton.MouseOverSurface.BorderColor = System.Drawing.Color.Empty;
            this.TitleButton.MouseOverSurface.Image = null;
            this.TitleButton.MouseOverSurface.TextColor = System.Drawing.Color.Empty;
            this.TitleButton.Name = "TitleButton";
            this.TitleButton.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.TitleButton.Size = new System.Drawing.Size(255, 60);
            this.TitleButton.Surface.BackColor = System.Drawing.Color.Empty;
            this.TitleButton.Surface.BackgroundImage = null;
            this.TitleButton.Surface.BorderColor = System.Drawing.Color.Empty;
            this.TitleButton.Surface.BorderSize = 0;
            this.TitleButton.Surface.Image = null;
            this.TitleButton.Surface.TextColor = System.Drawing.Color.Black;
            this.TitleButton.TabIndex = 0;
            this.TitleButton.Text = "SampleText";
            this.TitleButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.TitleButton.UseVisualStyleBackColor = false;
            // 
            // CloseButton
            // 
            this.CloseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CloseButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.CloseButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CloseButton.FlatAppearance.BorderSize = 0;
            this.CloseButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CloseButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CloseButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CloseButton.Image = null;
            this.CloseButton.Location = new System.Drawing.Point(315, 0);
            this.CloseButton.Margin = new System.Windows.Forms.Padding(0);
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
            this.CloseButton.Surface.Image = global::Cube.Forms.Properties.Resources.ButtonClose;
            this.CloseButton.Surface.TextColor = System.Drawing.Color.Empty;
            this.CloseButton.TabIndex = 0;
            this.CloseButton.UseVisualStyleBackColor = false;
            // 
            // NotifyForm
            // 
            this.AllowDragMove = false;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(340, 60);
            this.Controls.Add(this.LayoutPanel);
            this.Name = "NotifyForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.LayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel LayoutPanel;
        private Button ImageButton;
        private Button TitleButton;
        private Button CloseButton;
    }
}
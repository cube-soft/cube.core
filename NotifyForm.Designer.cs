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
            this.TitleButton = new Cube.Forms.Button();
            this.CloseButton = new Cube.Forms.Button();
            this.IconButton = new Cube.Forms.Button();
            this.LayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // LayoutPanel
            // 
            this.LayoutPanel.ColumnCount = 3;
            this.LayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.LayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.LayoutPanel.Controls.Add(this.IconButton, 0, 0);
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
            // TitleButton
            // 
            this.TitleButton.BorderColor = System.Drawing.Color.Transparent;
            this.TitleButton.BorderSize = 0;
            this.TitleButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TitleButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TitleButton.FlatAppearance.BorderSize = 0;
            this.TitleButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TitleButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TitleButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TitleButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TitleButton.Location = new System.Drawing.Point(63, 3);
            this.TitleButton.Name = "TitleButton";
            this.TitleButton.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.TitleButton.Size = new System.Drawing.Size(249, 54);
            this.TitleButton.Surface.CheckedBackColor = System.Drawing.Color.Empty;
            this.TitleButton.Surface.CheckedBackgroundImage = null;
            this.TitleButton.Surface.CheckedBorderColor = System.Drawing.Color.Empty;
            this.TitleButton.Surface.CheckedForeColor = System.Drawing.Color.Empty;
            this.TitleButton.Surface.CheckedImage = null;
            this.TitleButton.Surface.MouseDownBackColor = System.Drawing.Color.Empty;
            this.TitleButton.Surface.MouseDownBackgroundImage = null;
            this.TitleButton.Surface.MouseDownBorderColor = System.Drawing.Color.Empty;
            this.TitleButton.Surface.MouseDownForeColor = System.Drawing.Color.Empty;
            this.TitleButton.Surface.MouseDownImage = null;
            this.TitleButton.Surface.MouseOverBackColor = System.Drawing.Color.Empty;
            this.TitleButton.Surface.MouseOverBackgroundImage = null;
            this.TitleButton.Surface.MouseOverBorderColor = System.Drawing.Color.Empty;
            this.TitleButton.Surface.MouseOverForeColor = System.Drawing.Color.Empty;
            this.TitleButton.Surface.MouseOverImage = null;
            this.TitleButton.TabIndex = 0;
            this.TitleButton.Text = "SampleText";
            this.TitleButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.TitleButton.UseVisualStyleBackColor = false;
            // 
            // CloseButton
            // 
            this.CloseButton.BorderColor = System.Drawing.Color.Transparent;
            this.CloseButton.BorderSize = 0;
            this.CloseButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.CloseButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CloseButton.FlatAppearance.BorderSize = 0;
            this.CloseButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CloseButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CloseButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.Image = global::Cube.Forms.Properties.Resources.ButtonClose;
            this.CloseButton.Location = new System.Drawing.Point(318, 3);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(19, 19);
            this.CloseButton.Surface.CheckedBackColor = System.Drawing.Color.Empty;
            this.CloseButton.Surface.CheckedBackgroundImage = null;
            this.CloseButton.Surface.CheckedBorderColor = System.Drawing.Color.Empty;
            this.CloseButton.Surface.CheckedForeColor = System.Drawing.Color.Empty;
            this.CloseButton.Surface.CheckedImage = null;
            this.CloseButton.Surface.MouseDownBackColor = System.Drawing.Color.Empty;
            this.CloseButton.Surface.MouseDownBackgroundImage = null;
            this.CloseButton.Surface.MouseDownBorderColor = System.Drawing.Color.Empty;
            this.CloseButton.Surface.MouseDownForeColor = System.Drawing.Color.Empty;
            this.CloseButton.Surface.MouseDownImage = null;
            this.CloseButton.Surface.MouseOverBackColor = System.Drawing.Color.Empty;
            this.CloseButton.Surface.MouseOverBackgroundImage = null;
            this.CloseButton.Surface.MouseOverBorderColor = System.Drawing.Color.Empty;
            this.CloseButton.Surface.MouseOverForeColor = System.Drawing.Color.Empty;
            this.CloseButton.Surface.MouseOverImage = null;
            this.CloseButton.TabIndex = 0;
            this.CloseButton.UseVisualStyleBackColor = false;
            // 
            // IconButton
            // 
            this.IconButton.BorderColor = System.Drawing.Color.Transparent;
            this.IconButton.BorderSize = 0;
            this.IconButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.IconButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.IconButton.FlatAppearance.BorderSize = 0;
            this.IconButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.IconButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.IconButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.IconButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IconButton.Image = global::Cube.Forms.Properties.Resources.LogoLarge;
            this.IconButton.Location = new System.Drawing.Point(3, 3);
            this.IconButton.Name = "IconButton";
            this.IconButton.Size = new System.Drawing.Size(54, 54);
            this.IconButton.Surface.CheckedBackColor = System.Drawing.Color.Empty;
            this.IconButton.Surface.CheckedBackgroundImage = null;
            this.IconButton.Surface.CheckedBorderColor = System.Drawing.Color.Empty;
            this.IconButton.Surface.CheckedForeColor = System.Drawing.Color.Empty;
            this.IconButton.Surface.CheckedImage = null;
            this.IconButton.Surface.MouseDownBackColor = System.Drawing.Color.Empty;
            this.IconButton.Surface.MouseDownBackgroundImage = null;
            this.IconButton.Surface.MouseDownBorderColor = System.Drawing.Color.Empty;
            this.IconButton.Surface.MouseDownForeColor = System.Drawing.Color.Empty;
            this.IconButton.Surface.MouseDownImage = null;
            this.IconButton.Surface.MouseOverBackColor = System.Drawing.Color.Empty;
            this.IconButton.Surface.MouseOverBackgroundImage = null;
            this.IconButton.Surface.MouseOverBorderColor = System.Drawing.Color.Empty;
            this.IconButton.Surface.MouseOverForeColor = System.Drawing.Color.Empty;
            this.IconButton.Surface.MouseOverImage = null;
            this.IconButton.TabIndex = 0;
            this.IconButton.UseVisualStyleBackColor = false;
            // 
            // NotifyForm
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(340, 60);
            this.Controls.Add(this.LayoutPanel);
            this.Name = "NotifyForm";
            this.Text = "NotifyForm";
            this.LayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel LayoutPanel;
        private Button IconButton;
        private Button TitleButton;
        private Button CloseButton;
    }
}
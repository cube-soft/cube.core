namespace Cube.Forms
{
    partial class VersionControl
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.LayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.LogoPanel = new System.Windows.Forms.PictureBox();
            this.ContentsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.ProductLabel = new System.Windows.Forms.Label();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.PlatformLabel = new System.Windows.Forms.Label();
            this.DescriptionLabel = new System.Windows.Forms.Label();
            this.CopyrightLinkLabel = new System.Windows.Forms.LinkLabel();
            this.LayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogoPanel)).BeginInit();
            this.ContentsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // LayoutPanel
            // 
            this.LayoutPanel.ColumnCount = 2;
            this.LayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.LayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LayoutPanel.Controls.Add(this.LogoPanel, 0, 0);
            this.LayoutPanel.Controls.Add(this.ContentsPanel, 1, 0);
            this.LayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.LayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.LayoutPanel.Name = "LayoutPanel";
            this.LayoutPanel.RowCount = 1;
            this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.LayoutPanel.Size = new System.Drawing.Size(350, 140);
            this.LayoutPanel.TabIndex = 0;
            // 
            // LogoPanel
            // 
            this.LogoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogoPanel.Image = global::Cube.Forms.Properties.Resources.LogoLarge;
            this.LogoPanel.Location = new System.Drawing.Point(0, 0);
            this.LogoPanel.Margin = new System.Windows.Forms.Padding(0);
            this.LogoPanel.Name = "LogoPanel";
            this.LogoPanel.Size = new System.Drawing.Size(48, 140);
            this.LogoPanel.TabIndex = 0;
            this.LogoPanel.TabStop = false;
            // 
            // ContentsPanel
            // 
            this.ContentsPanel.Controls.Add(this.ProductLabel);
            this.ContentsPanel.Controls.Add(this.VersionLabel);
            this.ContentsPanel.Controls.Add(this.PlatformLabel);
            this.ContentsPanel.Controls.Add(this.DescriptionLabel);
            this.ContentsPanel.Controls.Add(this.CopyrightLinkLabel);
            this.ContentsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContentsPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.ContentsPanel.Location = new System.Drawing.Point(48, 0);
            this.ContentsPanel.Margin = new System.Windows.Forms.Padding(0);
            this.ContentsPanel.Name = "ContentsPanel";
            this.ContentsPanel.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.ContentsPanel.Size = new System.Drawing.Size(302, 140);
            this.ContentsPanel.TabIndex = 1;
            // 
            // ProductLabel
            // 
            this.ProductLabel.AutoEllipsis = true;
            this.ProductLabel.AutoSize = true;
            this.ProductLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ProductLabel.Location = new System.Drawing.Point(12, 0);
            this.ProductLabel.Margin = new System.Windows.Forms.Padding(0);
            this.ProductLabel.Name = "ProductLabel";
            this.ProductLabel.Size = new System.Drawing.Size(219, 12);
            this.ProductLabel.TabIndex = 10;
            this.ProductLabel.Text = "CubeSoft";
            // 
            // VersionLabel
            // 
            this.VersionLabel.AutoEllipsis = true;
            this.VersionLabel.AutoSize = true;
            this.VersionLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.VersionLabel.Location = new System.Drawing.Point(12, 14);
            this.VersionLabel.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(219, 12);
            this.VersionLabel.TabIndex = 18;
            this.VersionLabel.Text = "Version 1.0.0 (x86)";
            // 
            // PlatformLabel
            // 
            this.PlatformLabel.AutoSize = true;
            this.PlatformLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.PlatformLabel.Location = new System.Drawing.Point(12, 42);
            this.PlatformLabel.Margin = new System.Windows.Forms.Padding(0, 16, 0, 0);
            this.PlatformLabel.Name = "PlatformLabel";
            this.PlatformLabel.Size = new System.Drawing.Size(219, 24);
            this.PlatformLabel.TabIndex = 17;
            this.PlatformLabel.Text = "Microsoft Windows NT 10.0.10240.0\r\nMicrosoft .NETFramework 4.0.30319.42000\r\n";
            // 
            // DescriptionLabel
            // 
            this.DescriptionLabel.AutoEllipsis = true;
            this.DescriptionLabel.AutoSize = true;
            this.DescriptionLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.DescriptionLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.DescriptionLabel.Location = new System.Drawing.Point(12, 82);
            this.DescriptionLabel.Margin = new System.Windows.Forms.Padding(0, 16, 0, 0);
            this.DescriptionLabel.Name = "DescriptionLabel";
            this.DescriptionLabel.Size = new System.Drawing.Size(219, 12);
            this.DescriptionLabel.TabIndex = 14;
            this.DescriptionLabel.Text = "ユーザーテキスト";
            this.DescriptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CopyrightLinkLabel
            // 
            this.CopyrightLinkLabel.AutoSize = true;
            this.CopyrightLinkLabel.BackColor = System.Drawing.Color.Transparent;
            this.CopyrightLinkLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.CopyrightLinkLabel.Location = new System.Drawing.Point(12, 110);
            this.CopyrightLinkLabel.Margin = new System.Windows.Forms.Padding(0, 16, 0, 0);
            this.CopyrightLinkLabel.Name = "CopyrightLinkLabel";
            this.CopyrightLinkLabel.Size = new System.Drawing.Size(219, 12);
            this.CopyrightLinkLabel.TabIndex = 16;
            this.CopyrightLinkLabel.TabStop = true;
            this.CopyrightLinkLabel.Text = "Copyright (c) 2014 CubeSoft, Inc.";
            this.CopyrightLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // VersionControl
            // 
            this.Controls.Add(this.LayoutPanel);
            this.Name = "VersionControl";
            this.Size = new System.Drawing.Size(350, 140);
            this.LayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LogoPanel)).EndInit();
            this.ContentsPanel.ResumeLayout(false);
            this.ContentsPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel LayoutPanel;
        private System.Windows.Forms.PictureBox LogoPanel;
        private System.Windows.Forms.FlowLayoutPanel ContentsPanel;
        private System.Windows.Forms.Label ProductLabel;
        private System.Windows.Forms.Label DescriptionLabel;
        private System.Windows.Forms.LinkLabel CopyrightLinkLabel;
        private System.Windows.Forms.Label PlatformLabel;
        private System.Windows.Forms.Label VersionLabel;
    }
}

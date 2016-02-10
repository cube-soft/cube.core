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
            this.VersionLabel = new System.Windows.Forms.Label();
            this.DescriptionLabel = new System.Windows.Forms.Label();
            this.CopyrightLabel = new System.Windows.Forms.Label();
            this.WebLinkLabel = new System.Windows.Forms.LinkLabel();
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
            this.LayoutPanel.Size = new System.Drawing.Size(350, 130);
            this.LayoutPanel.TabIndex = 0;
            // 
            // LogoPanel
            // 
            this.LogoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogoPanel.Image = global::Cube.Forms.Properties.Resources.LogoLarge;
            this.LogoPanel.Location = new System.Drawing.Point(0, 0);
            this.LogoPanel.Margin = new System.Windows.Forms.Padding(0);
            this.LogoPanel.Name = "LogoPanel";
            this.LogoPanel.Size = new System.Drawing.Size(48, 130);
            this.LogoPanel.TabIndex = 0;
            this.LogoPanel.TabStop = false;
            // 
            // ContentsPanel
            // 
            this.ContentsPanel.Controls.Add(this.VersionLabel);
            this.ContentsPanel.Controls.Add(this.DescriptionLabel);
            this.ContentsPanel.Controls.Add(this.CopyrightLabel);
            this.ContentsPanel.Controls.Add(this.WebLinkLabel);
            this.ContentsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContentsPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.ContentsPanel.Location = new System.Drawing.Point(48, 0);
            this.ContentsPanel.Margin = new System.Windows.Forms.Padding(0);
            this.ContentsPanel.Name = "ContentsPanel";
            this.ContentsPanel.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.ContentsPanel.Size = new System.Drawing.Size(302, 130);
            this.ContentsPanel.TabIndex = 1;
            // 
            // VersionLabel
            // 
            this.VersionLabel.AutoEllipsis = true;
            this.VersionLabel.AutoSize = true;
            this.VersionLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.VersionLabel.Location = new System.Drawing.Point(12, 0);
            this.VersionLabel.Margin = new System.Windows.Forms.Padding(0);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(258, 36);
            this.VersionLabel.TabIndex = 10;
            this.VersionLabel.Text = "CubeSoft 1.0.0 (x86)\r\nMicrosoft Windows NT 10.0.10240.0\r\nMicrosoft .NET Framework" +
    " 4.0.30319.42000";
            // 
            // DescriptionLabel
            // 
            this.DescriptionLabel.AutoEllipsis = true;
            this.DescriptionLabel.AutoSize = true;
            this.DescriptionLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.DescriptionLabel.Location = new System.Drawing.Point(12, 52);
            this.DescriptionLabel.Margin = new System.Windows.Forms.Padding(0, 16, 0, 0);
            this.DescriptionLabel.Name = "DescriptionLabel";
            this.DescriptionLabel.Size = new System.Drawing.Size(258, 12);
            this.DescriptionLabel.TabIndex = 14;
            this.DescriptionLabel.Text = "このライブラリは Apache 2.0 でライセンスされています。";
            this.DescriptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CopyrightLabel
            // 
            this.CopyrightLabel.AutoSize = true;
            this.CopyrightLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.CopyrightLabel.Location = new System.Drawing.Point(12, 80);
            this.CopyrightLabel.Margin = new System.Windows.Forms.Padding(0, 16, 0, 0);
            this.CopyrightLabel.Name = "CopyrightLabel";
            this.CopyrightLabel.Size = new System.Drawing.Size(258, 12);
            this.CopyrightLabel.TabIndex = 15;
            this.CopyrightLabel.Text = "Copyright (c) 2014 CubeSoft, Inc.";
            this.CopyrightLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // WebLinkLabel
            // 
            this.WebLinkLabel.AutoSize = true;
            this.WebLinkLabel.BackColor = System.Drawing.Color.Transparent;
            this.WebLinkLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.WebLinkLabel.Location = new System.Drawing.Point(14, 94);
            this.WebLinkLabel.Margin = new System.Windows.Forms.Padding(2, 2, 0, 0);
            this.WebLinkLabel.Name = "WebLinkLabel";
            this.WebLinkLabel.Size = new System.Drawing.Size(256, 12);
            this.WebLinkLabel.TabIndex = 16;
            this.WebLinkLabel.TabStop = true;
            this.WebLinkLabel.Text = "http://www.cube-soft.jp/";
            this.WebLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // VersionControl
            // 
            this.Controls.Add(this.LayoutPanel);
            this.Name = "VersionControl";
            this.Size = new System.Drawing.Size(350, 130);
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
        private System.Windows.Forms.Label VersionLabel;
        private System.Windows.Forms.Label DescriptionLabel;
        private System.Windows.Forms.Label CopyrightLabel;
        private System.Windows.Forms.LinkLabel WebLinkLabel;
    }
}

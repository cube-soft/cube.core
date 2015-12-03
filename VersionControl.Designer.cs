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
            this.VersionPanel = new System.Windows.Forms.Panel();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.ContentsPanel = new System.Windows.Forms.Panel();
            this.DescriptionLabel = new System.Windows.Forms.Label();
            this.FooterPanel = new System.Windows.Forms.Panel();
            this.WebLinkLabel = new System.Windows.Forms.LinkLabel();
            this.CopyrightLabel = new System.Windows.Forms.Label();
            this.LayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogoPanel)).BeginInit();
            this.VersionPanel.SuspendLayout();
            this.ContentsPanel.SuspendLayout();
            this.FooterPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // LayoutPanel
            // 
            this.LayoutPanel.ColumnCount = 1;
            this.LayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LayoutPanel.Controls.Add(this.LogoPanel, 0, 0);
            this.LayoutPanel.Controls.Add(this.VersionPanel, 0, 1);
            this.LayoutPanel.Controls.Add(this.ContentsPanel, 0, 2);
            this.LayoutPanel.Controls.Add(this.FooterPanel, 0, 3);
            this.LayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.LayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.LayoutPanel.Name = "LayoutPanel";
            this.LayoutPanel.RowCount = 4;
            this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.LayoutPanel.Size = new System.Drawing.Size(300, 250);
            this.LayoutPanel.TabIndex = 0;
            // 
            // LogoPanel
            // 
            this.LogoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogoPanel.Image = global::Cube.Forms.Properties.Resources.LogoLarge;
            this.LogoPanel.Location = new System.Drawing.Point(0, 0);
            this.LogoPanel.Margin = new System.Windows.Forms.Padding(0);
            this.LogoPanel.Name = "LogoPanel";
            this.LogoPanel.Size = new System.Drawing.Size(300, 48);
            this.LogoPanel.TabIndex = 0;
            this.LogoPanel.TabStop = false;
            // 
            // VersionPanel
            // 
            this.VersionPanel.Controls.Add(this.VersionLabel);
            this.VersionPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VersionPanel.Location = new System.Drawing.Point(0, 48);
            this.VersionPanel.Margin = new System.Windows.Forms.Padding(0);
            this.VersionPanel.Name = "VersionPanel";
            this.VersionPanel.Size = new System.Drawing.Size(300, 70);
            this.VersionPanel.TabIndex = 1;
            // 
            // VersionLabel
            // 
            this.VersionLabel.AutoEllipsis = true;
            this.VersionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VersionLabel.Location = new System.Drawing.Point(0, 0);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(300, 70);
            this.VersionLabel.TabIndex = 0;
            this.VersionLabel.Text = "CubeSoft 1.0.0 (x86)\r\nMicrosoft Windows NT 10.0.10240.0\r\nMicrosoft .NET Framework" +
    " 4.0.30319.42000";
            this.VersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ContentsPanel
            // 
            this.ContentsPanel.Controls.Add(this.DescriptionLabel);
            this.ContentsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContentsPanel.Location = new System.Drawing.Point(0, 118);
            this.ContentsPanel.Margin = new System.Windows.Forms.Padding(0);
            this.ContentsPanel.Name = "ContentsPanel";
            this.ContentsPanel.Size = new System.Drawing.Size(300, 96);
            this.ContentsPanel.TabIndex = 2;
            // 
            // DescriptionLabel
            // 
            this.DescriptionLabel.AutoEllipsis = true;
            this.DescriptionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DescriptionLabel.Location = new System.Drawing.Point(0, 0);
            this.DescriptionLabel.Name = "DescriptionLabel";
            this.DescriptionLabel.Size = new System.Drawing.Size(300, 96);
            this.DescriptionLabel.TabIndex = 0;
            this.DescriptionLabel.Text = "このライブラリは Apache 2.0 でライセンスされています。";
            this.DescriptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FooterPanel
            // 
            this.FooterPanel.Controls.Add(this.WebLinkLabel);
            this.FooterPanel.Controls.Add(this.CopyrightLabel);
            this.FooterPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FooterPanel.Location = new System.Drawing.Point(0, 214);
            this.FooterPanel.Margin = new System.Windows.Forms.Padding(0);
            this.FooterPanel.Name = "FooterPanel";
            this.FooterPanel.Size = new System.Drawing.Size(300, 36);
            this.FooterPanel.TabIndex = 3;
            // 
            // WebLinkLabel
            // 
            this.WebLinkLabel.BackColor = System.Drawing.Color.Transparent;
            this.WebLinkLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.WebLinkLabel.Location = new System.Drawing.Point(0, 18);
            this.WebLinkLabel.Margin = new System.Windows.Forms.Padding(0);
            this.WebLinkLabel.Name = "WebLinkLabel";
            this.WebLinkLabel.Size = new System.Drawing.Size(300, 18);
            this.WebLinkLabel.TabIndex = 1;
            this.WebLinkLabel.TabStop = true;
            this.WebLinkLabel.Text = "http://www.cube-soft.jp/";
            this.WebLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.WebLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel_LinkClicked);
            // 
            // CopyrightLabel
            // 
            this.CopyrightLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.CopyrightLabel.Location = new System.Drawing.Point(0, 0);
            this.CopyrightLabel.Margin = new System.Windows.Forms.Padding(0);
            this.CopyrightLabel.Name = "CopyrightLabel";
            this.CopyrightLabel.Size = new System.Drawing.Size(300, 18);
            this.CopyrightLabel.TabIndex = 0;
            this.CopyrightLabel.Text = "Copyright (c) 2014 CubeSoft, Inc.";
            this.CopyrightLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // VersionControl
            // 
            this.Controls.Add(this.LayoutPanel);
            this.Name = "VersionControl";
            this.Size = new System.Drawing.Size(300, 250);
            this.LayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LogoPanel)).EndInit();
            this.VersionPanel.ResumeLayout(false);
            this.ContentsPanel.ResumeLayout(false);
            this.FooterPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel LayoutPanel;
        private System.Windows.Forms.PictureBox LogoPanel;
        private System.Windows.Forms.Panel VersionPanel;
        private System.Windows.Forms.Panel ContentsPanel;
        private System.Windows.Forms.Panel FooterPanel;
        private System.Windows.Forms.Label VersionLabel;
        private System.Windows.Forms.Label DescriptionLabel;
        private System.Windows.Forms.Label CopyrightLabel;
        private System.Windows.Forms.LinkLabel WebLinkLabel;
    }
}

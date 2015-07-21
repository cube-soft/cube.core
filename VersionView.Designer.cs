namespace Cube.Forms
{
    partial class VersionView
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

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.LayoutContainer = new System.Windows.Forms.SplitContainer();
            this.HeaderPanel = new System.Windows.Forms.Panel();
            this.DotNetVersionLabel = new System.Windows.Forms.Label();
            this.OSVersionLabel = new System.Windows.Forms.Label();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.LogoPictureBox = new System.Windows.Forms.PictureBox();
            this.ProductLabel = new System.Windows.Forms.Label();
            this.FooterPanel = new System.Windows.Forms.Panel();
            this.WebLinkLabel = new System.Windows.Forms.LinkLabel();
            this.PublisherLabel = new System.Windows.Forms.Label();
            this.LayoutContainer.Panel1.SuspendLayout();
            this.LayoutContainer.Panel2.SuspendLayout();
            this.LayoutContainer.SuspendLayout();
            this.HeaderPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).BeginInit();
            this.FooterPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // LayoutContainer
            // 
            this.LayoutContainer.BackColor = System.Drawing.Color.Transparent;
            this.LayoutContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LayoutContainer.IsSplitterFixed = true;
            this.LayoutContainer.Location = new System.Drawing.Point(0, 0);
            this.LayoutContainer.Name = "LayoutContainer";
            this.LayoutContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // LayoutContainer.Panel1
            // 
            this.LayoutContainer.Panel1.Controls.Add(this.HeaderPanel);
            this.LayoutContainer.Panel1MinSize = 140;
            // 
            // LayoutContainer.Panel2
            // 
            this.LayoutContainer.Panel2.Controls.Add(this.FooterPanel);
            this.LayoutContainer.Panel2MinSize = 60;
            this.LayoutContainer.Size = new System.Drawing.Size(400, 350);
            this.LayoutContainer.SplitterDistance = 250;
            this.LayoutContainer.SplitterWidth = 1;
            this.LayoutContainer.TabIndex = 0;
            // 
            // HeaderPanel
            // 
            this.HeaderPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.HeaderPanel.Controls.Add(this.DotNetVersionLabel);
            this.HeaderPanel.Controls.Add(this.OSVersionLabel);
            this.HeaderPanel.Controls.Add(this.VersionLabel);
            this.HeaderPanel.Controls.Add(this.LogoPictureBox);
            this.HeaderPanel.Controls.Add(this.ProductLabel);
            this.HeaderPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HeaderPanel.Location = new System.Drawing.Point(0, 0);
            this.HeaderPanel.Name = "HeaderPanel";
            this.HeaderPanel.Size = new System.Drawing.Size(400, 250);
            this.HeaderPanel.TabIndex = 3;
            // 
            // DotNetVersionLabel
            // 
            this.DotNetVersionLabel.BackColor = System.Drawing.Color.Transparent;
            this.DotNetVersionLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.DotNetVersionLabel.Location = new System.Drawing.Point(0, 100);
            this.DotNetVersionLabel.Name = "DotNetVersionLabel";
            this.DotNetVersionLabel.Size = new System.Drawing.Size(400, 20);
            this.DotNetVersionLabel.TabIndex = 5;
            this.DotNetVersionLabel.Text = ".NET Framework 4.0.30319.18408";
            this.DotNetVersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OSVersionLabel
            // 
            this.OSVersionLabel.BackColor = System.Drawing.Color.Transparent;
            this.OSVersionLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.OSVersionLabel.Location = new System.Drawing.Point(0, 80);
            this.OSVersionLabel.Name = "OSVersionLabel";
            this.OSVersionLabel.Size = new System.Drawing.Size(400, 20);
            this.OSVersionLabel.TabIndex = 4;
            this.OSVersionLabel.Text = "Microsoft Windows NT 6.1.7601 Service Pack 1";
            this.OSVersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // VersionLabel
            // 
            this.VersionLabel.BackColor = System.Drawing.Color.Transparent;
            this.VersionLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.VersionLabel.Location = new System.Drawing.Point(0, 60);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(400, 20);
            this.VersionLabel.TabIndex = 3;
            this.VersionLabel.Text = "Version 1.0.0 (x86)";
            this.VersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LogoPictureBox
            // 
            this.LogoPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.LogoPictureBox.Image = global::Cube.Forms.Properties.Resources.LogoLarge;
            this.LogoPictureBox.Location = new System.Drawing.Point(80, 0);
            this.LogoPictureBox.Name = "LogoPictureBox";
            this.LogoPictureBox.Size = new System.Drawing.Size(60, 60);
            this.LogoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.LogoPictureBox.TabIndex = 1;
            this.LogoPictureBox.TabStop = false;
            // 
            // ProductLabel
            // 
            this.ProductLabel.BackColor = System.Drawing.Color.Transparent;
            this.ProductLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ProductLabel.Location = new System.Drawing.Point(0, 0);
            this.ProductLabel.Name = "ProductLabel";
            this.ProductLabel.Size = new System.Drawing.Size(400, 60);
            this.ProductLabel.TabIndex = 0;
            this.ProductLabel.Text = "CubeWidget";
            this.ProductLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FooterPanel
            // 
            this.FooterPanel.Controls.Add(this.WebLinkLabel);
            this.FooterPanel.Controls.Add(this.PublisherLabel);
            this.FooterPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FooterPanel.Location = new System.Drawing.Point(0, 0);
            this.FooterPanel.Name = "FooterPanel";
            this.FooterPanel.Size = new System.Drawing.Size(400, 99);
            this.FooterPanel.TabIndex = 5;
            // 
            // WebLinkLabel
            // 
            this.WebLinkLabel.BackColor = System.Drawing.Color.Transparent;
            this.WebLinkLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.WebLinkLabel.Location = new System.Drawing.Point(0, 20);
            this.WebLinkLabel.Name = "WebLinkLabel";
            this.WebLinkLabel.Size = new System.Drawing.Size(400, 20);
            this.WebLinkLabel.TabIndex = 1;
            this.WebLinkLabel.TabStop = true;
            this.WebLinkLabel.Text = "http://www.cube-soft.jp/";
            this.WebLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.WebLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.WebLinkLabel_LinkClicked);
            // 
            // PublisherLabel
            // 
            this.PublisherLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.PublisherLabel.Location = new System.Drawing.Point(0, 0);
            this.PublisherLabel.Name = "PublisherLabel";
            this.PublisherLabel.Size = new System.Drawing.Size(400, 20);
            this.PublisherLabel.TabIndex = 0;
            this.PublisherLabel.Text = "Copyright (c) 2014 CubeSoft, Inc.";
            this.PublisherLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // VersionView
            // 
            this.Controls.Add(this.LayoutContainer);
            this.Name = "VersionView";
            this.Size = new System.Drawing.Size(400, 350);
            this.LayoutContainer.Panel1.ResumeLayout(false);
            this.LayoutContainer.Panel2.ResumeLayout(false);
            this.LayoutContainer.ResumeLayout(false);
            this.HeaderPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).EndInit();
            this.FooterPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer LayoutContainer;
        private System.Windows.Forms.Panel HeaderPanel;
        private System.Windows.Forms.Label DotNetVersionLabel;
        private System.Windows.Forms.Label OSVersionLabel;
        private System.Windows.Forms.Label VersionLabel;
        private System.Windows.Forms.PictureBox LogoPictureBox;
        private System.Windows.Forms.Label ProductLabel;
        private System.Windows.Forms.Panel FooterPanel;
        private System.Windows.Forms.LinkLabel WebLinkLabel;
        private System.Windows.Forms.Label PublisherLabel;


    }
}

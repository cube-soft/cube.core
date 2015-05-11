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
            this.ToolPanel = new System.Windows.Forms.TableLayoutPanel();
            this.UrlTextBox = new System.Windows.Forms.TextBox();
            this.VersionComboBox = new System.Windows.Forms.ComboBox();
            this.UpdateButton = new Cube.Forms.Button();
            this.WebBrowser = new Cube.Forms.WebBrowser();
            this.ToolPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ToolPanel
            // 
            this.ToolPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.ToolPanel.ColumnCount = 3;
            this.ToolPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ToolPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.ToolPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.ToolPanel.Controls.Add(this.UrlTextBox, 0, 0);
            this.ToolPanel.Controls.Add(this.VersionComboBox, 1, 0);
            this.ToolPanel.Controls.Add(this.UpdateButton, 2, 0);
            this.ToolPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ToolPanel.Location = new System.Drawing.Point(0, 26);
            this.ToolPanel.Name = "ToolPanel";
            this.ToolPanel.RowCount = 1;
            this.ToolPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ToolPanel.Size = new System.Drawing.Size(600, 30);
            this.ToolPanel.TabIndex = 1;
            // 
            // UrlTextBox
            // 
            this.UrlTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UrlTextBox.Location = new System.Drawing.Point(3, 3);
            this.UrlTextBox.Name = "UrlTextBox";
            this.UrlTextBox.Size = new System.Drawing.Size(454, 23);
            this.UrlTextBox.TabIndex = 0;
            // 
            // VersionComboBox
            // 
            this.VersionComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VersionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.VersionComboBox.FormattingEnabled = true;
            this.VersionComboBox.Location = new System.Drawing.Point(463, 3);
            this.VersionComboBox.Name = "VersionComboBox";
            this.VersionComboBox.Size = new System.Drawing.Size(94, 23);
            this.VersionComboBox.TabIndex = 0;
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
            this.UpdateButton.Location = new System.Drawing.Point(563, 3);
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
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // WebBrowser
            // 
            this.WebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WebBrowser.Location = new System.Drawing.Point(0, 56);
            this.WebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.WebBrowser.Name = "WebBrowser";
            this.WebBrowser.Size = new System.Drawing.Size(600, 344);
            this.WebBrowser.TabIndex = 2;
            this.WebBrowser.Url = new System.Uri("about:blank", System.UriKind.Absolute);
            // 
            // DemoWeb
            // 
            this.AcceptButton = this.UpdateButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.Controls.Add(this.WebBrowser);
            this.Controls.Add(this.ToolPanel);
            this.Name = "DemoWeb";
            this.ShowInTaskbar = false;
            this.Text = "WebForm";
            this.Controls.SetChildIndex(this.ToolPanel, 0);
            this.Controls.SetChildIndex(this.WebBrowser, 0);
            this.ToolPanel.ResumeLayout(false);
            this.ToolPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel ToolPanel;
        private WebBrowser WebBrowser;
        private System.Windows.Forms.TextBox UrlTextBox;
        private System.Windows.Forms.ComboBox VersionComboBox;
        private Button UpdateButton;

    }
}
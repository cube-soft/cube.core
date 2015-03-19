namespace Cube.Forms.Demo
{
    partial class DemoNotify
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
            this.TitleLabel = new System.Windows.Forms.Label();
            this.DisplayLabel = new System.Windows.Forms.Label();
            this.TitleTextBox = new System.Windows.Forms.TextBox();
            this.ImageTextBox = new System.Windows.Forms.TextBox();
            this.ImageBrowseButton = new Cube.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.DisplayMilliseconds = new System.Windows.Forms.NumericUpDown();
            this.DelayMilliseconds = new System.Windows.Forms.NumericUpDown();
            this.DelayLabel = new System.Windows.Forms.Label();
            this.LogTextBox = new System.Windows.Forms.TextBox();
            this.ShowButton = new Cube.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DisplayMilliseconds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DelayMilliseconds)).BeginInit();
            this.SuspendLayout();
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Location = new System.Drawing.Point(12, 48);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(46, 17);
            this.TitleLabel.TabIndex = 999;
            this.TitleLabel.Text = "タイトル";
            // 
            // DisplayLabel
            // 
            this.DisplayLabel.AutoSize = true;
            this.DisplayLabel.Location = new System.Drawing.Point(12, 105);
            this.DisplayLabel.Name = "DisplayLabel";
            this.DisplayLabel.Size = new System.Drawing.Size(60, 17);
            this.DisplayLabel.TabIndex = 999;
            this.DisplayLabel.Text = "表示時間";
            // 
            // TitleTextBox
            // 
            this.TitleTextBox.Location = new System.Drawing.Point(80, 45);
            this.TitleTextBox.Name = "TitleTextBox";
            this.TitleTextBox.Size = new System.Drawing.Size(358, 23);
            this.TitleTextBox.TabIndex = 0;
            this.TitleTextBox.Text = "NotifyForm Demo";
            // 
            // ImageTextBox
            // 
            this.ImageTextBox.Location = new System.Drawing.Point(80, 74);
            this.ImageTextBox.Name = "ImageTextBox";
            this.ImageTextBox.Size = new System.Drawing.Size(302, 23);
            this.ImageTextBox.TabIndex = 1;
            // 
            // ImageBrowseButton
            // 
            this.ImageBrowseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.ImageBrowseButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(172)))), ((int)(((byte)(172)))));
            this.ImageBrowseButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ImageBrowseButton.FlatAppearance.BorderSize = 0;
            this.ImageBrowseButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ImageBrowseButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ImageBrowseButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ImageBrowseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ImageBrowseButton.Location = new System.Drawing.Point(388, 74);
            this.ImageBrowseButton.Name = "ImageBrowseButton";
            this.ImageBrowseButton.Size = new System.Drawing.Size(50, 23);
            this.ImageBrowseButton.Surface.CheckedBackColor = System.Drawing.Color.Empty;
            this.ImageBrowseButton.Surface.CheckedBackgroundImage = null;
            this.ImageBrowseButton.Surface.CheckedBorderColor = System.Drawing.Color.Empty;
            this.ImageBrowseButton.Surface.CheckedForeColor = System.Drawing.Color.Empty;
            this.ImageBrowseButton.Surface.CheckedImage = null;
            this.ImageBrowseButton.Surface.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.ImageBrowseButton.Surface.MouseDownBackgroundImage = null;
            this.ImageBrowseButton.Surface.MouseDownBorderColor = System.Drawing.Color.Empty;
            this.ImageBrowseButton.Surface.MouseDownForeColor = System.Drawing.Color.Empty;
            this.ImageBrowseButton.Surface.MouseDownImage = null;
            this.ImageBrowseButton.Surface.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.ImageBrowseButton.Surface.MouseOverBackgroundImage = null;
            this.ImageBrowseButton.Surface.MouseOverBorderColor = System.Drawing.Color.Empty;
            this.ImageBrowseButton.Surface.MouseOverForeColor = System.Drawing.Color.Empty;
            this.ImageBrowseButton.Surface.MouseOverImage = null;
            this.ImageBrowseButton.TabIndex = 2;
            this.ImageBrowseButton.Text = "...";
            this.ImageBrowseButton.UseVisualStyleBackColor = false;
            this.ImageBrowseButton.Click += new System.EventHandler(this.ImageBrowseButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 17);
            this.label2.TabIndex = 999;
            this.label2.Text = "イメージ";
            // 
            // DisplayMilliseconds
            // 
            this.DisplayMilliseconds.Location = new System.Drawing.Point(80, 103);
            this.DisplayMilliseconds.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.DisplayMilliseconds.Name = "DisplayMilliseconds";
            this.DisplayMilliseconds.Size = new System.Drawing.Size(358, 23);
            this.DisplayMilliseconds.TabIndex = 3;
            this.DisplayMilliseconds.ThousandsSeparator = true;
            this.DisplayMilliseconds.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // DelayMilliseconds
            // 
            this.DelayMilliseconds.Location = new System.Drawing.Point(80, 132);
            this.DelayMilliseconds.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.DelayMilliseconds.Name = "DelayMilliseconds";
            this.DelayMilliseconds.Size = new System.Drawing.Size(358, 23);
            this.DelayMilliseconds.TabIndex = 4;
            this.DelayMilliseconds.ThousandsSeparator = true;
            this.DelayMilliseconds.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // DelayLabel
            // 
            this.DelayLabel.AutoSize = true;
            this.DelayLabel.Location = new System.Drawing.Point(12, 134);
            this.DelayLabel.Name = "DelayLabel";
            this.DelayLabel.Size = new System.Drawing.Size(60, 17);
            this.DelayLabel.TabIndex = 999;
            this.DelayLabel.Text = "遅延時間";
            // 
            // LogTextBox
            // 
            this.LogTextBox.Location = new System.Drawing.Point(12, 210);
            this.LogTextBox.Multiline = true;
            this.LogTextBox.Name = "LogTextBox";
            this.LogTextBox.ReadOnly = true;
            this.LogTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LogTextBox.Size = new System.Drawing.Size(426, 170);
            this.LogTextBox.TabIndex = 999;
            this.LogTextBox.TabStop = false;
            // 
            // ShowButton
            // 
            this.ShowButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.ShowButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(172)))), ((int)(((byte)(172)))));
            this.ShowButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ShowButton.FlatAppearance.BorderSize = 0;
            this.ShowButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ShowButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ShowButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ShowButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ShowButton.Location = new System.Drawing.Point(363, 170);
            this.ShowButton.Name = "ShowButton";
            this.ShowButton.Size = new System.Drawing.Size(75, 23);
            this.ShowButton.Surface.CheckedBackColor = System.Drawing.Color.Empty;
            this.ShowButton.Surface.CheckedBackgroundImage = null;
            this.ShowButton.Surface.CheckedBorderColor = System.Drawing.Color.Empty;
            this.ShowButton.Surface.CheckedForeColor = System.Drawing.Color.Empty;
            this.ShowButton.Surface.CheckedImage = null;
            this.ShowButton.Surface.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.ShowButton.Surface.MouseDownBackgroundImage = null;
            this.ShowButton.Surface.MouseDownBorderColor = System.Drawing.Color.Empty;
            this.ShowButton.Surface.MouseDownForeColor = System.Drawing.Color.Empty;
            this.ShowButton.Surface.MouseDownImage = null;
            this.ShowButton.Surface.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.ShowButton.Surface.MouseOverBackgroundImage = null;
            this.ShowButton.Surface.MouseOverBorderColor = System.Drawing.Color.Empty;
            this.ShowButton.Surface.MouseOverForeColor = System.Drawing.Color.Empty;
            this.ShowButton.Surface.MouseOverImage = null;
            this.ShowButton.TabIndex = 19;
            this.ShowButton.Text = "Show";
            this.ShowButton.UseVisualStyleBackColor = false;
            this.ShowButton.Click += new System.EventHandler(this.ShowButton_Click);
            // 
            // DemoNotify
            // 
            this.ClientSize = new System.Drawing.Size(450, 400);
            this.Controls.Add(this.ShowButton);
            this.Controls.Add(this.LogTextBox);
            this.Controls.Add(this.DelayLabel);
            this.Controls.Add(this.DelayMilliseconds);
            this.Controls.Add(this.DisplayMilliseconds);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ImageBrowseButton);
            this.Controls.Add(this.ImageTextBox);
            this.Controls.Add(this.TitleTextBox);
            this.Controls.Add(this.DisplayLabel);
            this.Controls.Add(this.TitleLabel);
            this.Name = "DemoNotify";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Cube.Forms.NotifyForm";
            this.Controls.SetChildIndex(this.TitleLabel, 0);
            this.Controls.SetChildIndex(this.DisplayLabel, 0);
            this.Controls.SetChildIndex(this.TitleTextBox, 0);
            this.Controls.SetChildIndex(this.ImageTextBox, 0);
            this.Controls.SetChildIndex(this.ImageBrowseButton, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.DisplayMilliseconds, 0);
            this.Controls.SetChildIndex(this.DelayMilliseconds, 0);
            this.Controls.SetChildIndex(this.DelayLabel, 0);
            this.Controls.SetChildIndex(this.LogTextBox, 0);
            this.Controls.SetChildIndex(this.ShowButton, 0);
            ((System.ComponentModel.ISupportInitialize)(this.DisplayMilliseconds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DelayMilliseconds)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Label DisplayLabel;
        private System.Windows.Forms.TextBox TitleTextBox;
        private System.Windows.Forms.TextBox ImageTextBox;
        private Button ImageBrowseButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown DisplayMilliseconds;
        private System.Windows.Forms.NumericUpDown DelayMilliseconds;
        private System.Windows.Forms.Label DelayLabel;
        private System.Windows.Forms.TextBox LogTextBox;
        private Button ShowButton;

    }
}
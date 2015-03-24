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
            this.EnqueueButton = new Cube.Forms.Button();
            this.LevelLabel = new System.Windows.Forms.Label();
            this.LevelComboBox = new System.Windows.Forms.ComboBox();
            this.ClearButton = new Cube.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DisplayMilliseconds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DelayMilliseconds)).BeginInit();
            this.SuspendLayout();
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Location = new System.Drawing.Point(12, 69);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(42, 15);
            this.TitleLabel.TabIndex = 999;
            this.TitleLabel.Text = "タイトル";
            // 
            // DisplayLabel
            // 
            this.DisplayLabel.AutoSize = true;
            this.DisplayLabel.Location = new System.Drawing.Point(12, 126);
            this.DisplayLabel.Name = "DisplayLabel";
            this.DisplayLabel.Size = new System.Drawing.Size(55, 15);
            this.DisplayLabel.TabIndex = 999;
            this.DisplayLabel.Text = "表示時間";
            // 
            // TitleTextBox
            // 
            this.TitleTextBox.Location = new System.Drawing.Point(80, 66);
            this.TitleTextBox.Name = "TitleTextBox";
            this.TitleTextBox.Size = new System.Drawing.Size(358, 23);
            this.TitleTextBox.TabIndex = 1;
            this.TitleTextBox.Text = "NotifyForm Demo";
            // 
            // ImageTextBox
            // 
            this.ImageTextBox.Location = new System.Drawing.Point(80, 95);
            this.ImageTextBox.Name = "ImageTextBox";
            this.ImageTextBox.Size = new System.Drawing.Size(302, 23);
            this.ImageTextBox.TabIndex = 2;
            // 
            // ImageBrowseButton
            // 
            this.ImageBrowseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ImageBrowseButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ImageBrowseButton.FlatAppearance.BorderSize = 0;
            this.ImageBrowseButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ImageBrowseButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ImageBrowseButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ImageBrowseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ImageBrowseButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ImageBrowseButton.Image = null;
            this.ImageBrowseButton.Location = new System.Drawing.Point(388, 95);
            this.ImageBrowseButton.MouseDownSurface.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.ImageBrowseButton.MouseDownSurface.BackgroundImage = null;
            this.ImageBrowseButton.MouseDownSurface.BorderColor = System.Drawing.Color.Empty;
            this.ImageBrowseButton.MouseDownSurface.Image = null;
            this.ImageBrowseButton.MouseDownSurface.TextColor = System.Drawing.Color.Empty;
            this.ImageBrowseButton.MouseOverSurface.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.ImageBrowseButton.MouseOverSurface.BackgroundImage = null;
            this.ImageBrowseButton.MouseOverSurface.BorderColor = System.Drawing.Color.Empty;
            this.ImageBrowseButton.MouseOverSurface.Image = null;
            this.ImageBrowseButton.MouseOverSurface.TextColor = System.Drawing.Color.Empty;
            this.ImageBrowseButton.Name = "ImageBrowseButton";
            this.ImageBrowseButton.Size = new System.Drawing.Size(50, 23);
            this.ImageBrowseButton.Surface.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.ImageBrowseButton.Surface.BackgroundImage = null;
            this.ImageBrowseButton.Surface.BorderColor = System.Drawing.Color.Gray;
            this.ImageBrowseButton.Surface.BorderSize = 1;
            this.ImageBrowseButton.Surface.Image = null;
            this.ImageBrowseButton.Surface.TextColor = System.Drawing.Color.Black;
            this.ImageBrowseButton.TabIndex = 3;
            this.ImageBrowseButton.Text = "...";
            this.ImageBrowseButton.UseVisualStyleBackColor = false;
            this.ImageBrowseButton.Click += new System.EventHandler(this.ImageBrowseButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 15);
            this.label2.TabIndex = 999;
            this.label2.Text = "イメージ";
            // 
            // DisplayMilliseconds
            // 
            this.DisplayMilliseconds.Location = new System.Drawing.Point(80, 124);
            this.DisplayMilliseconds.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.DisplayMilliseconds.Name = "DisplayMilliseconds";
            this.DisplayMilliseconds.Size = new System.Drawing.Size(358, 23);
            this.DisplayMilliseconds.TabIndex = 4;
            this.DisplayMilliseconds.ThousandsSeparator = true;
            this.DisplayMilliseconds.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // DelayMilliseconds
            // 
            this.DelayMilliseconds.Location = new System.Drawing.Point(80, 153);
            this.DelayMilliseconds.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.DelayMilliseconds.Name = "DelayMilliseconds";
            this.DelayMilliseconds.Size = new System.Drawing.Size(358, 23);
            this.DelayMilliseconds.TabIndex = 5;
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
            this.DelayLabel.Location = new System.Drawing.Point(12, 155);
            this.DelayLabel.Name = "DelayLabel";
            this.DelayLabel.Size = new System.Drawing.Size(55, 15);
            this.DelayLabel.TabIndex = 999;
            this.DelayLabel.Text = "遅延時間";
            // 
            // LogTextBox
            // 
            this.LogTextBox.Location = new System.Drawing.Point(12, 230);
            this.LogTextBox.Multiline = true;
            this.LogTextBox.Name = "LogTextBox";
            this.LogTextBox.ReadOnly = true;
            this.LogTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LogTextBox.Size = new System.Drawing.Size(426, 150);
            this.LogTextBox.TabIndex = 999;
            this.LogTextBox.TabStop = false;
            // 
            // EnqueueButton
            // 
            this.EnqueueButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.EnqueueButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.EnqueueButton.FlatAppearance.BorderSize = 0;
            this.EnqueueButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.EnqueueButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.EnqueueButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.EnqueueButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EnqueueButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.EnqueueButton.Image = null;
            this.EnqueueButton.Location = new System.Drawing.Point(257, 190);
            this.EnqueueButton.MouseDownSurface.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.EnqueueButton.MouseDownSurface.BackgroundImage = null;
            this.EnqueueButton.MouseDownSurface.BorderColor = System.Drawing.Color.Empty;
            this.EnqueueButton.MouseDownSurface.Image = null;
            this.EnqueueButton.MouseDownSurface.TextColor = System.Drawing.Color.Empty;
            this.EnqueueButton.MouseOverSurface.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.EnqueueButton.MouseOverSurface.BackgroundImage = null;
            this.EnqueueButton.MouseOverSurface.BorderColor = System.Drawing.Color.Empty;
            this.EnqueueButton.MouseOverSurface.Image = null;
            this.EnqueueButton.MouseOverSurface.TextColor = System.Drawing.Color.Empty;
            this.EnqueueButton.Name = "EnqueueButton";
            this.EnqueueButton.Size = new System.Drawing.Size(100, 23);
            this.EnqueueButton.Surface.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.EnqueueButton.Surface.BackgroundImage = null;
            this.EnqueueButton.Surface.BorderColor = System.Drawing.Color.Gray;
            this.EnqueueButton.Surface.BorderSize = 1;
            this.EnqueueButton.Surface.Image = null;
            this.EnqueueButton.Surface.TextColor = System.Drawing.Color.Black;
            this.EnqueueButton.TabIndex = 6;
            this.EnqueueButton.Text = "Enqueue";
            this.EnqueueButton.UseVisualStyleBackColor = false;
            this.EnqueueButton.Click += new System.EventHandler(this.EnqueueButton_Click);
            // 
            // LevelLabel
            // 
            this.LevelLabel.AutoSize = true;
            this.LevelLabel.Location = new System.Drawing.Point(12, 40);
            this.LevelLabel.Name = "LevelLabel";
            this.LevelLabel.Size = new System.Drawing.Size(43, 15);
            this.LevelLabel.TabIndex = 999;
            this.LevelLabel.Text = "重要度";
            // 
            // LevelComboBox
            // 
            this.LevelComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LevelComboBox.FormattingEnabled = true;
            this.LevelComboBox.Location = new System.Drawing.Point(80, 37);
            this.LevelComboBox.Name = "LevelComboBox";
            this.LevelComboBox.Size = new System.Drawing.Size(358, 23);
            this.LevelComboBox.TabIndex = 0;
            // 
            // ClearButton
            // 
            this.ClearButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClearButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClearButton.FlatAppearance.BorderSize = 0;
            this.ClearButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClearButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClearButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClearButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ClearButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClearButton.Image = null;
            this.ClearButton.Location = new System.Drawing.Point(363, 190);
            this.ClearButton.MouseDownSurface.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ClearButton.MouseDownSurface.BackgroundImage = null;
            this.ClearButton.MouseDownSurface.BorderColor = System.Drawing.Color.Empty;
            this.ClearButton.MouseDownSurface.Image = null;
            this.ClearButton.MouseDownSurface.TextColor = System.Drawing.Color.Empty;
            this.ClearButton.MouseOverSurface.BackColor = System.Drawing.Color.MistyRose;
            this.ClearButton.MouseOverSurface.BackgroundImage = null;
            this.ClearButton.MouseOverSurface.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ClearButton.MouseOverSurface.Image = null;
            this.ClearButton.MouseOverSurface.TextColor = System.Drawing.Color.Empty;
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(75, 23);
            this.ClearButton.Surface.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.ClearButton.Surface.BackgroundImage = null;
            this.ClearButton.Surface.BorderColor = System.Drawing.Color.Gray;
            this.ClearButton.Surface.BorderSize = 1;
            this.ClearButton.Surface.Image = null;
            this.ClearButton.Surface.TextColor = System.Drawing.Color.Black;
            this.ClearButton.TabIndex = 7;
            this.ClearButton.Text = "Clear";
            this.ClearButton.UseVisualStyleBackColor = false;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // DemoNotify
            // 
            this.ClientSize = new System.Drawing.Size(450, 400);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.LevelComboBox);
            this.Controls.Add(this.LevelLabel);
            this.Controls.Add(this.EnqueueButton);
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
            this.Controls.SetChildIndex(this.EnqueueButton, 0);
            this.Controls.SetChildIndex(this.LevelLabel, 0);
            this.Controls.SetChildIndex(this.LevelComboBox, 0);
            this.Controls.SetChildIndex(this.ClearButton, 0);
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
        private Button EnqueueButton;
        private System.Windows.Forms.Label LevelLabel;
        private System.Windows.Forms.ComboBox LevelComboBox;
        private Button ClearButton;

    }
}
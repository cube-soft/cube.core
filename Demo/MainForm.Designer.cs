namespace Cube.Forms.Demo
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ImageButton = new Cube.Forms.Button();
            this.FlatButton = new Cube.Forms.Button();
            this.SuspendLayout();
            // 
            // ImageButton
            // 
            this.ImageButton.Appearance.CheckedBackColor = System.Drawing.Color.Empty;
            this.ImageButton.Appearance.CheckedBackgroundImage = null;
            this.ImageButton.Appearance.CheckedBorderColor = System.Drawing.Color.Empty;
            this.ImageButton.Appearance.CheckedImage = null;
            this.ImageButton.Appearance.MouseDownBackColor = System.Drawing.Color.Turquoise;
            this.ImageButton.Appearance.MouseDownBackgroundImage = null;
            this.ImageButton.Appearance.MouseDownBorderColor = System.Drawing.Color.Empty;
            this.ImageButton.Appearance.MouseDownImage = global::Cube.Forms.Demo.Properties.Resources.LogoCubeIce;
            this.ImageButton.Appearance.MouseOverBackColor = System.Drawing.Color.PaleTurquoise;
            this.ImageButton.Appearance.MouseOverBackgroundImage = null;
            this.ImageButton.Appearance.MouseOverBorderColor = System.Drawing.Color.Empty;
            this.ImageButton.Appearance.MouseOverImage = global::Cube.Forms.Demo.Properties.Resources.LogoCubeIce;
            this.ImageButton.BackColor = System.Drawing.Color.White;
            this.ImageButton.BorderColor = System.Drawing.Color.Black;
            this.ImageButton.BorderSize = 1;
            this.ImageButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ImageButton.Image = global::Cube.Forms.Demo.Properties.Resources.LogoCube;
            this.ImageButton.Location = new System.Drawing.Point(212, 12);
            this.ImageButton.Name = "ImageButton";
            this.ImageButton.Size = new System.Drawing.Size(60, 50);
            this.ImageButton.TabIndex = 0;
            this.ImageButton.UseVisualStyleBackColor = false;
            // 
            // FlatButton
            // 
            this.FlatButton.Appearance.CheckedBackColor = System.Drawing.Color.Empty;
            this.FlatButton.Appearance.CheckedBackgroundImage = null;
            this.FlatButton.Appearance.CheckedBorderColor = System.Drawing.Color.Empty;
            this.FlatButton.Appearance.CheckedImage = null;
            this.FlatButton.Appearance.MouseDownBackColor = System.Drawing.Color.Empty;
            this.FlatButton.Appearance.MouseDownBackgroundImage = null;
            this.FlatButton.Appearance.MouseDownBorderColor = System.Drawing.Color.Empty;
            this.FlatButton.Appearance.MouseDownImage = null;
            this.FlatButton.Appearance.MouseOverBackColor = System.Drawing.Color.Empty;
            this.FlatButton.Appearance.MouseOverBackgroundImage = null;
            this.FlatButton.Appearance.MouseOverBorderColor = System.Drawing.Color.Empty;
            this.FlatButton.Appearance.MouseOverImage = null;
            this.FlatButton.BackColor = System.Drawing.Color.White;
            this.FlatButton.BorderColor = System.Drawing.Color.Black;
            this.FlatButton.BorderSize = 1;
            this.FlatButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FlatButton.Location = new System.Drawing.Point(56, 12);
            this.FlatButton.Name = "FlatButton";
            this.FlatButton.Size = new System.Drawing.Size(150, 50);
            this.FlatButton.TabIndex = 1;
            this.FlatButton.Text = "SampleText";
            this.FlatButton.UseVisualStyleBackColor = false;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(284, 74);
            this.Controls.Add(this.FlatButton);
            this.Controls.Add(this.ImageButton);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(128)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Cube.Forms Demo";
            this.ResumeLayout(false);

        }

        #endregion

        private Button ImageButton;
        private Button FlatButton;





    }
}

